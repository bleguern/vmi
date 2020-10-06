/*
 * Created by SharpDevelop.
 * User: Benoit Le Guern
 * Date: 25/03/2007
 * Time: 18:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.IO;


namespace VMI
{
	/// <summary>
	/// Description of Option.
	/// </summary>
	public partial class Option : Form
	{
		private const string CONFIG_FILENAME = "config.xml";
		
		public Option()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			LoadDefault();
		}
		
		private void LoadDefault()
		{
			try
            {
                XmlDocument config = new XmlDocument();

                config.Load(Application.StartupPath + "\\" + CONFIG_FILENAME);

                tbxDsDsrpDir.Text = config.DocumentElement.SelectSingleNode("//config/directories/directory[@name='ds']/@value").Value;
                tbxCustomerDir.Text = config.DocumentElement.SelectSingleNode("//config/directories/directory[@name='customer']/@value").Value;
                tbxUser.Text = config.DocumentElement.SelectSingleNode("//config/user[@name='login']/@value").Value;
            }
            catch (Exception)
            {
            	
            }
		}
		
		private bool SaveDefault()
        {
            try
            {
                XmlDocument config = new XmlDocument();

                config.LoadXml("<?xml version=\"1.0\"?>" +
                               "<config>" +
                                "<directories>" +
                                    "<directory name=\"ds\" value=\"" + tbxDsDsrpDir.Text + "\" />" +
                                    "<directory name=\"customer\" value=\"" + tbxCustomerDir.Text + "\" />" +
                                "</directories>" + 
                                "<user name=\"login\" value=\"" + tbxUser.Text + "\" />" +
                               "</config>");

                config.Save(Application.StartupPath + "\\" + CONFIG_FILENAME);
            	
              	return true;
            }
            catch (Exception)
            {
            	return false;
            }
        }
		
		void BtnCancelClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
		
		void BtnValidateClick(object sender, EventArgs e)
		{
			if (SaveDefault())
			{
				this.DialogResult = DialogResult.OK;
			}
			else
			{
				this.DialogResult = DialogResult.None;
				this.Close();
			}
		}
		
		void BtnDsDsrpDirClick(object sender, EventArgs e)
		{
			fbdDsDsrpDir = new FolderBrowserDialog();
			fbdDsDsrpDir.SelectedPath = tbxDsDsrpDir.Text;
				
			if (fbdDsDsrpDir.ShowDialog() == DialogResult.OK) {
				string dsDsrpDir = fbdDsDsrpDir.SelectedPath;
	            
				if ((dsDsrpDir != null) && (Directory.Exists(dsDsrpDir)))
	            {
	            	if ((!File.Exists(dsDsrpDir + "\\" + "dsw.exe")) ||
	            	    (!File.Exists(dsDsrpDir + "\\" + "dsrp.exe")))
	            	{
						MessageBox.Show(this, "DS/DSRP introuvable!");
					}
					else
					{
						tbxDsDsrpDir.Text = dsDsrpDir;
					}
	            } else {
					MessageBox.Show(this, "Répertoire DS/DSRP incorrect : " + dsDsrpDir);
	            }
			}
		}
		
		void BtnCustomerDirClick(object sender, EventArgs e)
		{
			fbdCustomerDir = new FolderBrowserDialog();
			fbdCustomerDir.SelectedPath = tbxCustomerDir.Text;
			
			if (fbdCustomerDir.ShowDialog() == DialogResult.OK) {
				
				string customerDir = fbdCustomerDir.SelectedPath;
	            
	            if ((customerDir != null) && (Directory.Exists(customerDir)))
	            {
	            	bool forehistFound = false;
            	
	            	foreach (string dir in Directory.GetDirectories(customerDir))
	            	{
	            		if (File.Exists(dir + "\\" + "FOREHIST.TXT"))
	            		{
	            			forehistFound = true;
	            			break;
	            		}
	            	}
	            	
	            	if (!forehistFound)
	            	{
	            		MessageBox.Show(this, "Aucune base client trouvée dans : " + customerDir);
	            	}
	            	else
	            	{
	            		tbxCustomerDir.Text = customerDir;
	            	}
	            } else {
	            	MessageBox.Show(this, "Répertoire client incorrect : " + customerDir);
	            }
			}
		}
	}
}
