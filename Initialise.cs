/*
 * Created by SharpDevelop.
 * User: benoit le guern
 * Date: 30/03/2007
 * Time: 09:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.IO;
using System.Data;
using System.Text;

namespace VMI
{
	/// <summary>
	/// Description of Initialise.
	/// </summary>
	public class Initialise
	{
		/// <summary>
		/// DS configuration file
		/// </summary>
		private const string DS_CONFIG_FILE_NAME            = "C:\\WINDOWS\\DSW.INI";
		
		/// <summary>
		/// Import folder
		/// </summary>
		public const string IMPORT_FOLDER_NAME             = "IMPORT";
		
		/// <summary>
		/// Customer files
		/// </summary>
		private const string PO_FILENAME                    = "po.txt";
		private const string FO_FILENAME                    = "fo.txt";
        private const string DOWNLOAD_FILENAME              = "Download.txt";
        
        /// <summary>
        /// DS / DSRP files
        /// </summary>
        private const string RECEIPTS_FILENAME              = "Receipts.Dat";
        private const string CUSTORDS_FILENAME              = "CustOrds.Dat";
        private const string ONHAND_FILENAME                = "OnHand.Dat";

        private string dsDsrpFolder                         = null;
        private string customerBaseFolder                   = null;
        private string customerFileFolder                   = null;
        private string customerTrigram                      = null;
     
        private string userName                             = null;
        
        
        private string error = null;
        
        
		public Initialise(string dsDsrp, string customer, string user, string customerName, string trigram)
		{
			if (dsDsrp.EndsWith("\\"))
			{
				dsDsrp = dsDsrp.Substring(0, dsDsrp.Length-1);
			}
			
			if (customer.EndsWith("\\"))
			{
				customer = customer.Substring(0, customer.Length-1);
			}
			
			this.dsDsrpFolder = dsDsrp;
			this.customerFileFolder = customer + "\\" + IMPORT_FOLDER_NAME;
			this.customerBaseFolder = customer;
			this.userName = user;
			this.customerTrigram = trigram;
		}
		
		private void AddError(string message)
        {
            error += message + "#";
        }

        public string[] GetErrors()
        {
            if (error != null)
            {
                return error.Split("#".ToCharArray());
            }
            else
            {
                return null;
            }
        }
		
		
		public bool Launch()
		{
			bool success = false;

			BackupFiles();
			
			success = LoadConfig();
            success &= Import();
                
            return success;
		}
		
		private bool LoadConfig()
		{
			if (File.Exists(DS_CONFIG_FILE_NAME))
			{
				File.Copy(DS_CONFIG_FILE_NAME, DS_CONFIG_FILE_NAME + ".bak", true);
				File.Delete(DS_CONFIG_FILE_NAME);
			}
			
			try
			{
				StreamWriter sw = new StreamWriter(DS_CONFIG_FILE_NAME);
				
				sw.WriteLine("[Common]");
				sw.WriteLine("DSUserInfo=" + userName);
				sw.WriteLine("DSWindowsProgramDirectory=" + dsDsrpFolder);
				sw.WriteLine("DSDataDirectory=" + customerBaseFolder);
				sw.WriteLine("[Interactive]");
				sw.WriteLine("ShowStatusBar=1");
				sw.WriteLine("[Recent File List]");
				sw.WriteLine("File1=" + customerBaseFolder);
				
				sw.WriteLine("[DSRP Common]");
				sw.WriteLine("DSRPDataDirectory=" + customerBaseFolder);
				sw.WriteLine("DSRPUserInfo=" + userName);
				sw.WriteLine("[DSRP Recent File List]");
				sw.WriteLine("File1=" + customerBaseFolder);
				sw.WriteLine("[DSRP Interactive]");
				sw.WriteLine("ShowStatusBar=Y");

				sw.WriteLine("[License]");
				sw.WriteLine("DSUserName=" + userName);
				sw.WriteLine("CompanyName=Modling");
				sw.WriteLine("Location=Moult");
				
				sw.Flush();
				sw.Close();
				
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			
			return false;
		}
		
		private bool Import()
		{
			bool success = false;
			
			success = ImportReceipts();
			success &= ImportCustords();
		    success &= ImportOnHand();
		    
		    return success;
		}
		
		private void BackupFiles()
        {
            string strCustordsFileName    = customerBaseFolder + "\\" + CUSTORDS_FILENAME;
            string strOnhandFileName      = customerBaseFolder + "\\" + ONHAND_FILENAME;
            string strReceiptsFileName    = customerBaseFolder + "\\" + RECEIPTS_FILENAME;

            try
            {
                if (File.Exists(strCustordsFileName))
                {
                    File.Copy(strCustordsFileName, strCustordsFileName + ".bak", true);
                }

                if (File.Exists(strOnhandFileName))
                {
                    File.Copy(strOnhandFileName, strOnhandFileName + ".bak", true);
                }

                if (File.Exists(strReceiptsFileName))
                {
                    File.Copy(strReceiptsFileName, strReceiptsFileName + ".bak", true);
                }
            }
            catch (Exception)
            {
               	
            }
        }

		private bool ImportReceipts()
		{
			bool success = false;
			
			string inFileName = customerFileFolder + "\\" + PO_FILENAME;
			string outFileName = customerBaseFolder + "\\" + RECEIPTS_FILENAME;
			
			if (File.Exists(inFileName))
			{
				DataTable receiptsInTable = new DataTable();

	            receiptsInTable.Columns.Add("Reference", System.Type.GetType("System.String"));
	            receiptsInTable.Columns[0].MaxLength = 18;
		        receiptsInTable.Columns[0].Caption = "Number";
		        receiptsInTable.Columns.Add("Filler", System.Type.GetType("System.String"));
	            receiptsInTable.Columns[1].MaxLength = 1;
	            receiptsInTable.Columns.Add("PO_Number", System.Type.GetType("System.String"));
	            receiptsInTable.Columns[2].MaxLength = 10;
	            receiptsInTable.Columns[2].Caption = "Number";
	            receiptsInTable.Columns.Add("Filler2", System.Type.GetType("System.String"));
	            receiptsInTable.Columns[3].MaxLength = 1;
	            receiptsInTable.Columns.Add("Quantity", System.Type.GetType("System.String"));
	            receiptsInTable.Columns[4].MaxLength = 10;
	            receiptsInTable.Columns[4].Caption = "Number";
	            receiptsInTable.Columns.Add("Filler3", System.Type.GetType("System.String"));
	            receiptsInTable.Columns[5].MaxLength = 1;
	            receiptsInTable.Columns.Add("Date", System.Type.GetType("System.String"));
	            receiptsInTable.Columns[6].MaxLength = 8;
				
	            success = VmiIO.LoadFile(inFileName, ref receiptsInTable);
	            
				if (success)
	            {
					DataTable receiptsOutTable = new DataTable();

					receiptsOutTable.Columns.Add("Reference", System.Type.GetType("System.String"));
		            receiptsOutTable.Columns[0].MaxLength = 17;
		            receiptsOutTable.Columns.Add("Location", System.Type.GetType("System.String"));
		            receiptsOutTable.Columns[1].MaxLength = 3;
		            receiptsOutTable.Columns.Add("Quantity", System.Type.GetType("System.String"));
		            receiptsOutTable.Columns[2].MaxLength = 8;
		            receiptsOutTable.Columns[2].Caption = "Number";
		            receiptsOutTable.Columns.Add("Filler", System.Type.GetType("System.String"));
		            receiptsOutTable.Columns[3].MaxLength = 1;
		            receiptsOutTable.Columns.Add("PO_Number", System.Type.GetType("System.String"));
		            receiptsOutTable.Columns[4].MaxLength = 9;
		            receiptsOutTable.Columns.Add("Description", System.Type.GetType("System.String"));
		            receiptsOutTable.Columns[5].MaxLength = 51;
		            receiptsOutTable.Columns.Add("Date", System.Type.GetType("System.String"));
		            receiptsOutTable.Columns[6].MaxLength = 6;
		            
					foreach (DataRow row in receiptsInTable.Rows)
					{
						if (row[0].ToString() != "")
						{
							try
							{
								DataRow newRow = receiptsOutTable.NewRow();
								
								DateTime poDate = Convert.ToDateTime(row[6]);
								
								newRow[0] = row[0].ToString();
								newRow[1] = customerTrigram;
								newRow[2] = row[4].ToString();
								newRow[3] = "";
								newRow[4] = row[2].ToString().PadLeft(9, '0');
								newRow[5] = "";
								newRow[6] = poDate.ToString("yyMMdd");
								
								receiptsOutTable.Rows.Add(newRow);
							}
							catch(Exception)
							{

							}
						}
					}
					
	                success &= VmiIO.WriteFile(outFileName, receiptsOutTable);
	            }
			}
			
			return success;
		}
		
		private bool ImportCustords()
		{
			bool success = false;
			
			string inFileName = customerFileFolder + "\\" + FO_FILENAME;
			string outFileName = customerBaseFolder + "\\" + CUSTORDS_FILENAME;
			
			if (File.Exists(inFileName))
			{
				DataTable custordsInTable = new DataTable();

	            custordsInTable.Columns.Add("Reference", System.Type.GetType("System.String"));
	            custordsInTable.Columns[0].MaxLength = 18;
		        custordsInTable.Columns[0].Caption = "Number";
		        custordsInTable.Columns.Add("Filler", System.Type.GetType("System.String"));
	            custordsInTable.Columns[1].MaxLength = 1;
	            custordsInTable.Columns.Add("FO_Number", System.Type.GetType("System.String"));
	            custordsInTable.Columns[2].MaxLength = 10;
	            custordsInTable.Columns.Add("Filler2", System.Type.GetType("System.String"));
	            custordsInTable.Columns[3].MaxLength = 1;
	            custordsInTable.Columns.Add("Quantity", System.Type.GetType("System.String"));
	            custordsInTable.Columns[4].MaxLength = 10;
	            custordsInTable.Columns[4].Caption = "Number";
	            custordsInTable.Columns.Add("Filler3", System.Type.GetType("System.String"));
	            custordsInTable.Columns[5].MaxLength = 1;
	            custordsInTable.Columns.Add("Date", System.Type.GetType("System.String"));
	            custordsInTable.Columns[6].MaxLength = 8;
				
	            success = VmiIO.LoadFile(inFileName, ref custordsInTable);
	            
				if (success)
	            {
					DataTable custordsOutTable = new DataTable();

					custordsOutTable.Columns.Add("Reference", System.Type.GetType("System.String"));
		            custordsOutTable.Columns[0].MaxLength = 17;
		            custordsOutTable.Columns.Add("Location", System.Type.GetType("System.String"));
		            custordsOutTable.Columns[1].MaxLength = 3;
		            custordsOutTable.Columns.Add("Quantity", System.Type.GetType("System.String"));
		            custordsOutTable.Columns[2].MaxLength = 8;
		            custordsOutTable.Columns[2].Caption = "Number";
		            custordsOutTable.Columns.Add("Filler", System.Type.GetType("System.String"));
		            custordsOutTable.Columns[3].MaxLength = 1;
		            custordsOutTable.Columns.Add("Order_Number", System.Type.GetType("System.String"));
		            custordsOutTable.Columns[4].MaxLength = 13;
		            custordsOutTable.Columns.Add("Customer_Number", System.Type.GetType("System.String"));
		            custordsOutTable.Columns[5].MaxLength = 8;
		            custordsOutTable.Columns.Add("Direct", System.Type.GetType("System.String"));
		            custordsOutTable.Columns[6].MaxLength = 1;
		            custordsOutTable.Columns.Add("Customer_Name", System.Type.GetType("System.String"));
		            custordsOutTable.Columns[7].MaxLength = 29;
		            custordsOutTable.Columns.Add("Status", System.Type.GetType("System.String"));
		            custordsOutTable.Columns[8].MaxLength = 4;
		            custordsOutTable.Columns.Add("Date", System.Type.GetType("System.String"));
		            custordsOutTable.Columns[9].MaxLength = 6;
		            
					foreach (DataRow row in custordsInTable.Rows)
					{
						if (row[0].ToString() != "")
						{
							try
							{
								DataRow newRow = custordsOutTable.NewRow();
								
								DateTime foDate = Convert.ToDateTime(row[6]);
								
								newRow[0] = row[0].ToString();
								newRow[1] = customerTrigram;
								newRow[2] = row[4].ToString();
								newRow[3] = "";
								newRow[4] = row[2].ToString().PadRight(13, ' ');
								newRow[5] = "";
								newRow[6] = "";
								newRow[7] = "";
								newRow[8] = "";
								newRow[9] = foDate.ToString("yyMMdd");
								
								custordsOutTable.Rows.Add(newRow);
							}
							catch(Exception)
							{
								
							}
						}
					}
	            
	                success &= VmiIO.WriteFile(outFileName, custordsOutTable);
	            }
			}
			
			return success;
		}
		
		private bool ImportOnHand()
		{
			bool success = false;
			
			string inFileName = customerFileFolder + "\\" + DOWNLOAD_FILENAME;
			string outFileName = customerBaseFolder + "\\" + ONHAND_FILENAME;
			
			if (File.Exists(inFileName))
			{
				DataTable onHandInTable = new DataTable();

	            onHandInTable.Columns.Add("Date", System.Type.GetType("System.String"));
	            onHandInTable.Columns[0].MaxLength = 8;
		        onHandInTable.Columns.Add("Filler1", System.Type.GetType("System.String"));
	            onHandInTable.Columns[1].MaxLength = 1;
	            onHandInTable.Columns.Add("Reference", System.Type.GetType("System.String"));
	            onHandInTable.Columns[2].MaxLength = 18;
	            onHandInTable.Columns[2].Caption = "Number";
	            onHandInTable.Columns.Add("Filler2", System.Type.GetType("System.String"));
	            onHandInTable.Columns[3].MaxLength = 1;
	            onHandInTable.Columns.Add("Last_Conso", System.Type.GetType("System.String"));
	            onHandInTable.Columns[4].MaxLength = 14;
	            onHandInTable.Columns.Add("Filler3", System.Type.GetType("System.String"));
	            onHandInTable.Columns[5].MaxLength = 1;
	            onHandInTable.Columns.Add("Last_Total", System.Type.GetType("System.String"));
	            onHandInTable.Columns[6].MaxLength = 14;
	            onHandInTable.Columns.Add("Filler4", System.Type.GetType("System.String"));
	            onHandInTable.Columns[7].MaxLength = 1;
	            onHandInTable.Columns.Add("MTD", System.Type.GetType("System.String"));
	            onHandInTable.Columns[8].MaxLength = 14;
	            onHandInTable.Columns[8].Caption = "Number";
	            onHandInTable.Columns.Add("Filler5", System.Type.GetType("System.String"));
	            onHandInTable.Columns[9].MaxLength = 1;
	            onHandInTable.Columns.Add("Total", System.Type.GetType("System.String"));
	            onHandInTable.Columns[10].MaxLength = 14;
	            onHandInTable.Columns.Add("Filler6", System.Type.GetType("System.String"));
	            onHandInTable.Columns[11].MaxLength = 1;
	            onHandInTable.Columns.Add("Stock", System.Type.GetType("System.String"));
	            onHandInTable.Columns[12].MaxLength = 14;
	            onHandInTable.Columns.Add("Filler7", System.Type.GetType("System.String"));
	            onHandInTable.Columns[13].MaxLength = 1;
	            onHandInTable.Columns.Add("Supplier_ral", System.Type.GetType("System.String"));
	            onHandInTable.Columns[14].MaxLength = 14;
	            onHandInTable.Columns.Add("Filler8", System.Type.GetType("System.String"));
	            onHandInTable.Columns[15].MaxLength = 1;
	            onHandInTable.Columns.Add("Customer_ral", System.Type.GetType("System.String"));
	            onHandInTable.Columns[16].MaxLength = 14;
	            onHandInTable.Columns.Add("Filler9", System.Type.GetType("System.String"));
	            onHandInTable.Columns[17].MaxLength = 1;
	            onHandInTable.Columns.Add("Mini_stock", System.Type.GetType("System.String"));
	            onHandInTable.Columns[18].MaxLength = 14;
	            onHandInTable.Columns.Add("Filler10", System.Type.GetType("System.String"));
	            onHandInTable.Columns[19].MaxLength = 1;
	            onHandInTable.Columns.Add("Classe", System.Type.GetType("System.String"));
	            onHandInTable.Columns[20].MaxLength = 1;
	            onHandInTable.Columns.Add("Filler11", System.Type.GetType("System.String"));
	            onHandInTable.Columns[21].MaxLength = 1;
	            onHandInTable.Columns.Add("Mini_stock_entered", System.Type.GetType("System.String"));
	            onHandInTable.Columns[22].MaxLength = 14;
	            onHandInTable.Columns.Add("Filler12", System.Type.GetType("System.String"));
	            onHandInTable.Columns[23].MaxLength = 1;
	            onHandInTable.Columns.Add("Multiple", System.Type.GetType("System.String"));
	            onHandInTable.Columns[24].MaxLength = 14;
				
	            success = VmiIO.LoadFile(inFileName, ref onHandInTable);
	            
				if (success)
	            {
					DataTable onHandOutTable = new DataTable();

					onHandOutTable.Columns.Add("Reference", System.Type.GetType("System.String"));
		            onHandOutTable.Columns[0].MaxLength = 17;
		            onHandOutTable.Columns.Add("Location", System.Type.GetType("System.String"));
		            onHandOutTable.Columns[1].MaxLength = 3;
		            onHandOutTable.Columns.Add("Stock_Quantity", System.Type.GetType("System.String"));
		            onHandOutTable.Columns[2].MaxLength = 8;
		            onHandOutTable.Columns[2].Caption = "Number";
		            onHandOutTable.Columns.Add("Weight", System.Type.GetType("System.String"));
		            onHandOutTable.Columns[3].MaxLength = 12;
		            onHandOutTable.Columns.Add("Cube", System.Type.GetType("System.String"));
		            onHandOutTable.Columns[4].MaxLength = 12;
		            onHandOutTable.Columns.Add("MTD", System.Type.GetType("System.String"));
		            onHandOutTable.Columns[5].MaxLength = 8;
		            onHandOutTable.Columns[5].Caption = "Number";
		            onHandOutTable.Columns.Add("Minimum_Order", System.Type.GetType("System.String"));
		            onHandOutTable.Columns[6].MaxLength = 8;
		            onHandOutTable.Columns[6].Caption = "Number";
		            onHandOutTable.Columns.Add("Multiple", System.Type.GetType("System.String"));
		            onHandOutTable.Columns[7].MaxLength = 8;
		            onHandOutTable.Columns[7].Caption = "Number";
		            
					foreach (DataRow row in onHandInTable.Rows)
					{
						if (row[2].ToString() != "")
						{
							try
							{
								DataRow newRow = onHandOutTable.NewRow();
								
								newRow[0] = row[2].ToString();
								newRow[1] = customerTrigram;
								newRow[2] = row[12].ToString();
								newRow[3] = "";
								newRow[4] = "";
								newRow[5] = row[8].ToString();
								newRow[6] = "";
								newRow[7] = "";
								
								onHandOutTable.Rows.Add(newRow);
							}
							catch(Exception)
							{
								
							}
						}
					}
	            
	                success &= VmiIO.WriteFile(outFileName, onHandOutTable);
	            }            
			}
			
			return success;
		}
	}
}
