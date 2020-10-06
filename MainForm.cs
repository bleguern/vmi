/*
 * Created by SharpDevelop.
 * User: Benoit Le Guern
 * Date: 25/03/2007
 * Time: 18:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace VMI
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private const string CONFIG_FILENAME = "config.xml";
		private const string LOCK_FILENAME = "DSRPLock.Txt";
		
		private string dsDsrpDir = null;
		private string customerDir = null;
		private string userName = null;
		private string trigram = null;
		
		private const string export = "STOCKOUTS;JUST RUSH ORDERS;JUST STOCK ORDERS";
		
		private void LoadDefault()
		{
			lblMessage.Text = "";
			string message = null;
			
			lbxExport.Items.Clear();
			
			foreach (string name in export.Split(";".ToCharArray()))
			{
				lbxExport.Items.Add(name);
			}
			
			cbxCustomer.Items.Clear();
			
			gbxCustomer.Enabled = false;
			gbxDsDsrp.Enabled = false;
			gbxExport.Enabled = false;
			
			btnDsStart.Enabled = false;
			btnDsrpStart.Enabled = false;
			
			
			try
            {
                XmlDocument config = new XmlDocument();

                config.Load(Application.StartupPath + "\\" + CONFIG_FILENAME);

                dsDsrpDir = config.DocumentElement.SelectSingleNode("//config/directories/directory[@name='ds']/@value").Value;
                customerDir = config.DocumentElement.SelectSingleNode("//config/directories/directory[@name='customer']/@value").Value;
                userName = config.DocumentElement.SelectSingleNode("//config/user[@name='login']/@value").Value;
                trigram = config.DocumentElement.SelectSingleNode("//config/base[@name='trigram']/@value").Value;
			}
            catch (Exception)
            {
            	
            }
           	
            if ((dsDsrpDir != null) && (Directory.Exists(dsDsrpDir)))
            {
            	if ((!File.Exists(dsDsrpDir + "\\" + "dsw.exe")) ||
            	    (!File.Exists(dsDsrpDir + "\\" + "dsrp.exe")))
            	{
            		message += "DS/DSRP introuvable!\n";
            	}
            }
            else
            {
            	message += "Répertoire DS/DSRP incorrect : " + dsDsrpDir + "\n";
            }
            
            if ((customerDir != null) && (Directory.Exists(customerDir)))
            {
            	bool forehistFound = false;
            	
            	foreach (string dir in Directory.GetDirectories(customerDir))
            	{
            		if (File.Exists(dir + "\\" + "FOREHIST.TXT"))
            		{
            			string dirValue = dir.Replace(customerDir, "");
            			
            			if (dirValue.StartsWith("\\"))
            			{
            				dirValue = dirValue.Substring(1);
            			}
            			
            			cbxCustomer.Items.Add(dirValue);
            			forehistFound = true;
            		}
            	}
            	
            	if (!forehistFound)
            	{
            		message += "Aucune base client trouvée dans : " + customerDir + "\n";
            	}
            	else
            	{
            		gbxCustomer.Enabled = true;
            		gbxDsDsrp.Enabled = true;
            		gbxImport.Enabled = true;
            	}
            }
            else
            {
            	message += "Répertoire client incorrect : " + customerDir + "\n";
            }
            
            lblMessage.Text = message;
            
            if (cbxCustomer.Items.Count > 0)
            {
            	cbxCustomer.SelectedIndex = 0;
            }
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
		public MainForm()
		{
			InitializeComponent();
			LoadDefault();
		}
		
		void BtnQuitClick(object sender, EventArgs e)
		{
			this.Close();
			Application.Exit();
		}
		
		void BtnOptionClick(object sender, EventArgs e)
		{
			Option optionForm = new Option();
			
			if (optionForm.ShowDialog(this) == DialogResult.OK)
			{
				LoadDefault();
			}
		}
		
		void BtnDsStartClick(object sender, EventArgs e)
		{
			Process ds = new Process();
			
			try
			{
				ds.StartInfo.WorkingDirectory = string.Format(dsDsrpDir);
				ds.StartInfo.FileName = dsDsrpDir + "\\dsw.exe";
				ds.StartInfo.CreateNoWindow = false;
				ds.Start();
			}
			catch (Exception)
			{

			}
		}
		
		void BtnDsrpStartClick(object sender, EventArgs e)
		{
			Process dsrp = new Process();
			
			try
			{
				dsrp.StartInfo.WorkingDirectory = string.Format(dsDsrpDir);
				dsrp.StartInfo.FileName = dsDsrpDir + "\\dsrp.exe";
				dsrp.StartInfo.CreateNoWindow = false;
				dsrp.Start();
			}
			catch (Exception)
			{

			}
		}
		
		void BtnDsrpInitClick(object sender, EventArgs e)
		{
			if (!customerDir.EndsWith("\\"))
			{
				customerDir += "\\";
			}
			
			string lockFileName = customerDir + cbxCustomer.Items[cbxCustomer.SelectedIndex].ToString() + "\\" + LOCK_FILENAME;
			
			if (File.Exists(lockFileName))
		    {
				if (MessageBox.Show("Fichier de verrouillage présent. Voulez-vous le supprimer et continuer?", 
				                    "Base verrouillée", 
				                    MessageBoxButtons.YesNo, 
				                    MessageBoxIcon.Warning) == DialogResult.Yes)
				{
					File.Delete(lockFileName);
				}
				else
				{
					return;
				}
		    	
		    }
			
			btnDsrpInit.Enabled = false;
			
			Initialise init = new Initialise(dsDsrpDir, 
			                                 customerDir + cbxCustomer.Items[cbxCustomer.SelectedIndex].ToString(),
			                                 userName,
			                                 cbxCustomer.Items[cbxCustomer.SelectedIndex].ToString(),
			                                 trigram);
			
			if (init.Launch())
			{
				Process dsrpInit = new Process();
				
				try
				{
					dsrpInit.StartInfo.WorkingDirectory = string.Format(dsDsrpDir);
					dsrpInit.StartInfo.FileName = "dsrpld.exe";
					dsrpInit.StartInfo.CreateNoWindow = false;
					dsrpInit.Start();
					dsrpInit.WaitForExit();
					
					btnDsStart.Enabled = true;
					btnDsrpStart.Enabled = true;
					gbxExport.Enabled = true;
					btnExport.Enabled = false;
				}
				catch (Exception)
				{
	
				}
			}
			
			btnDsrpInit.Enabled = true;
		}
		
		void CbxCustomerSelectedIndexChanged(object sender, EventArgs e)
		{
			btnDsStart.Enabled = false;
			btnDsrpStart.Enabled = false;
			gbxExport.Enabled = false;
		}
		
		void LbxExportSelectedIndexChanged(object sender, EventArgs e)
		{
			if (lbxExport.SelectedIndex != -1)
			{
				btnExport.Enabled = true;
			}
			else
			{
				btnExport.Enabled = false;
			}
		}
		
		void BtnExportClick(object sender, EventArgs e)
		{
			ExportToExcel exportToExcel = new ExportToExcel(dsDsrpDir, customerDir + cbxCustomer.Items[cbxCustomer.SelectedIndex].ToString());
			
			if (exportToExcel.Load())
			{
				switch (lbxExport.Items[lbxExport.SelectedIndex].ToString())
				{
					case "STOCKOUTS":
					{
						btnExport.Enabled = false;
						exportToExcel.ExportStockOut();
						btnExport.Enabled = true;
						break;
					}
					case "JUST RUSH ORDERS":
					{
						btnExport.Enabled = false;
						exportToExcel.ExportRushOrders();
						btnExport.Enabled = true;
						break;
					}
					case "JUST STOCK ORDERS":
					{
						btnExport.Enabled = false;
						exportToExcel.ExportStockOrders();
						btnExport.Enabled = true;
						break;
					}
					default:
					{
						break;
					}
				}
			}
		}
		
		void BtnImportOpenClick(object sender, EventArgs e)
		{
			string importDir = customerDir;
			
			if (!importDir.EndsWith("\\"))
			{
				importDir += "\\";
			}
			
			importDir += cbxCustomer.Items[cbxCustomer.SelectedIndex].ToString() + "\\" + Initialise.IMPORT_FOLDER_NAME;
			
			if (!Directory.Exists(importDir))
			{
				if (MessageBox.Show("Le répertoire \"" + importDir + "\" n'existe pas, voulez-vous le créer?", "Répertoire d'importation client inexistant", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					Directory.CreateDirectory(importDir);
				}
				else
				{
					return;
				}
			}
			
			Process explorer = new Process();
			explorer.StartInfo.WorkingDirectory = string.Format(customerDir);
			explorer.StartInfo.FileName = "explorer.exe";
			explorer.StartInfo.CreateNoWindow = false;
			explorer.StartInfo.Arguments = importDir;
			explorer.Start();
		}
	}
}
