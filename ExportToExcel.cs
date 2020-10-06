/*
 * Created by SharpDevelop.
 * User: Benoit Le Guern
 * Date: 04/04/2007
 * Time: 21:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Globalization;

namespace VMI
{
	/// <summary>
	/// Description of ExportToExcel.
	/// </summary>
	public class ExportToExcel
	{
		/// <summary>
        /// DS / DSRP files
        /// </summary>
        private const string RECEIPTS_FILENAME              = "Receipts.Dat";
        private const string CUSTORDS_FILENAME              = "CustOrds.Dat";
        private const string ONHAND_FILENAME                = "OnHand.Dat";
        private const string ORDPLAN_FILENAME               = "OrdPlan.Txt";
        private const string FOREHIST_FILENAME              = "FOREHIST.TXT";
        private const string STOCK_FILENAME                 = "STOCK.EXP";
        
		private string dsDsrpFolder                         = null;
        private string customerDatabaseFolder               = null;
        
        private System.Data.DataTable receiptsTable = new System.Data.DataTable();
        private System.Data.DataTable custordsTable = new System.Data.DataTable();
        private System.Data.DataTable forehistTable = new System.Data.DataTable();
        private System.Data.DataTable stockTable = new System.Data.DataTable();
        private System.Data.DataTable ordPlanTable = new System.Data.DataTable();
        
		public ExportToExcel(string dsDsrp, string customer)
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
			this.customerDatabaseFolder = customer;
		}
		
		public bool Load()
		{
			return LoadOrdPlan() &&
				   LoadStock() &&
				   LoadForehist();
		}
		
		private bool LoadStock()
		{
			stockTable.Columns.Add("Reference", System.Type.GetType("System.String"));
            stockTable.Columns[0].MaxLength = 17;
            stockTable.Columns.Add("Skip_Field", System.Type.GetType("System.String"));
            stockTable.Columns[1].MaxLength = 3;               //  20
            
            for (int i = 0; i < 61; i++)
            {
            	stockTable.Columns.Add("Stock_" + i.ToString().PadLeft(2, '0'), System.Type.GetType("System.String"));
            	stockTable.Columns[2 + i].MaxLength = 8;
            	stockTable.Columns[2 + i].Caption = "Number";
            }
			
			return VmiIO.LoadFile(customerDatabaseFolder + "\\EXPORT\\" + STOCK_FILENAME, ref stockTable);
		}
		
		private bool LoadCustords()
		{
			custordsTable.Columns.Add("Reference", System.Type.GetType("System.String"));
            custordsTable.Columns[0].MaxLength = 17;
            custordsTable.Columns.Add("Location", System.Type.GetType("System.String"));
            custordsTable.Columns[1].MaxLength = 3;
            custordsTable.Columns.Add("Quantity", System.Type.GetType("System.String"));
            custordsTable.Columns[2].MaxLength = 8;
            custordsTable.Columns[2].Caption = "Number";
            custordsTable.Columns.Add("Filler", System.Type.GetType("System.String"));
            custordsTable.Columns[3].MaxLength = 1;
            custordsTable.Columns.Add("CO_Number", System.Type.GetType("System.String"));
            custordsTable.Columns[4].MaxLength = 13;
            custordsTable.Columns.Add("Customer_Number", System.Type.GetType("System.String"));
            custordsTable.Columns[4].MaxLength = 8;
            custordsTable.Columns.Add("Description", System.Type.GetType("System.String"));
            custordsTable.Columns[5].MaxLength = 34;
            custordsTable.Columns.Add("Date", System.Type.GetType("System.String"));
            custordsTable.Columns[6].MaxLength = 6;
			
			return VmiIO.LoadFile(customerDatabaseFolder + "\\" + CUSTORDS_FILENAME, ref receiptsTable);
		}
		
		private bool LoadReceipts()
		{
			receiptsTable.Columns.Add("Reference", System.Type.GetType("System.String"));
            receiptsTable.Columns[0].MaxLength = 17;
            receiptsTable.Columns.Add("Location", System.Type.GetType("System.String"));
            receiptsTable.Columns[1].MaxLength = 3;
            receiptsTable.Columns.Add("Quantity", System.Type.GetType("System.String"));
            receiptsTable.Columns[2].MaxLength = 8;
            receiptsTable.Columns[2].Caption = "Number";
            receiptsTable.Columns.Add("Filler", System.Type.GetType("System.String"));
            receiptsTable.Columns[3].MaxLength = 1;
            receiptsTable.Columns.Add("PO_Number", System.Type.GetType("System.String"));
            receiptsTable.Columns[4].MaxLength = 9;
            receiptsTable.Columns.Add("Description", System.Type.GetType("System.String"));
            receiptsTable.Columns[5].MaxLength = 51;
            receiptsTable.Columns.Add("Date", System.Type.GetType("System.String"));
            receiptsTable.Columns[6].MaxLength = 6;
			
			return VmiIO.LoadFile(customerDatabaseFolder + "\\" + RECEIPTS_FILENAME, ref receiptsTable);
		}
		
		private bool LoadForehist()
		{
			forehistTable.Columns.Add("Reference", System.Type.GetType("System.String"));
            forehistTable.Columns[0].MaxLength = 17;                //  1
            forehistTable.Columns.Add("Location", System.Type.GetType("System.String"));
            forehistTable.Columns[1].MaxLength = 3;                 //  18
            forehistTable.Columns.Add("Reference_Modling", System.Type.GetType("System.String"));
            forehistTable.Columns[2].MaxLength = 25;                //  21
            forehistTable.Columns.Add("Fournisseur", System.Type.GetType("System.String"));
            forehistTable.Columns[3].MaxLength = 8;                 //  46
            forehistTable.Columns.Add("Skip_Field3", System.Type.GetType("System.String"));
            forehistTable.Columns[4].MaxLength = 963;               //  54
            forehistTable.Columns.Add("Vendor", System.Type.GetType("System.String"));
            forehistTable.Columns[5].MaxLength = 8;                 //  1017
            forehistTable.Columns.Add("Planner", System.Type.GetType("System.String"));
            forehistTable.Columns[6].MaxLength = 8;                 //  1025
            forehistTable.Columns.Add("Multiple", System.Type.GetType("System.String"));
            forehistTable.Columns[7].MaxLength = 12;                //  1033
            forehistTable.Columns.Add("Skip_Field4", System.Type.GetType("System.String"));
            forehistTable.Columns[8].MaxLength = 18;                //  1045
            forehistTable.Columns.Add("ABC_Code", System.Type.GetType("System.String"));
            forehistTable.Columns[9].MaxLength = 1;                 //  1063
            forehistTable.Columns.Add("Skip_Field5", System.Type.GetType("System.String"));
            forehistTable.Columns[10].MaxLength = 57;               //  1064
            forehistTable.Columns.Add("On_Hand", System.Type.GetType("System.String"));
            forehistTable.Columns[11].MaxLength = 8;                //  1121
			
            return VmiIO.LoadFile(customerDatabaseFolder + "\\" + FOREHIST_FILENAME, ref forehistTable);
		}
		
		private bool LoadOrdPlan()
		{
			ordPlanTable.Columns.Add("Skip_field1", System.Type.GetType("System.String"));
            ordPlanTable.Columns[0].MaxLength = 11;                //  1
            ordPlanTable.Columns.Add("Period", System.Type.GetType("System.String"));
            ordPlanTable.Columns[1].MaxLength = 2;                 //  2
            ordPlanTable.Columns[1].Caption = "Number";
            ordPlanTable.Columns.Add("Skip_field2", System.Type.GetType("System.String"));
            ordPlanTable.Columns[2].MaxLength = 1;                 //  1
            ordPlanTable.Columns.Add("Reference", System.Type.GetType("System.String"));
            ordPlanTable.Columns[3].MaxLength = 17;                //  1
            ordPlanTable.Columns.Add("Priority", System.Type.GetType("System.String"));
            ordPlanTable.Columns[4].MaxLength = 1;                 //  1
            ordPlanTable.Columns[4].Caption = "Number";
            ordPlanTable.Columns.Add("Quantity", System.Type.GetType("System.String"));
            ordPlanTable.Columns[5].MaxLength = 8;                 //  1
            ordPlanTable.Columns[5].Caption = "Number";
            ordPlanTable.Columns.Add("Skip_field3", System.Type.GetType("System.String"));
            ordPlanTable.Columns[6].MaxLength = 104;               //  1
            ordPlanTable.Columns.Add("Date", System.Type.GetType("System.String"));
            ordPlanTable.Columns[7].MaxLength = 7;                 //  1
			
            return VmiIO.LoadFile(customerDatabaseFolder + "\\" + ORDPLAN_FILENAME, ref ordPlanTable);
		}
		
		private void Export(System.Data.DataTable dataTable)
		{
			Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = (Workbook)application.Workbooks.Add(Missing.Value);
            Worksheet worksheet = (Worksheet)application.ActiveSheet;
           	worksheet.Cells.NumberFormat = "@";
           	
	        if (dataTable != null)
	        {
		        for (int i = 0; i < dataTable.Columns.Count; i++)
		        {
		        	worksheet.Cells[1, i + 1] = dataTable.Columns[i].ColumnName;
		        }
		        
		        for (int i = 0; i < dataTable.Rows.Count; i++)
		        {
		        	for (int j = 0; j < dataTable.Columns.Count; j++)
		        	{
		        		worksheet.Cells[i + 2, j + 1] = dataTable.Rows[i][j].ToString();
		        	}
		        }
	        }
	        
	        application.Visible = true;
		}
		
		public void ExportStockOut()
		{
			System.Data.DataTable stockOutTable = new System.Data.DataTable();

            stockOutTable.Columns.Add("Code Client", System.Type.GetType("System.String"));
	        stockOutTable.Columns.Add("Code Modling", System.Type.GetType("System.String"));
	        stockOutTable.Columns.Add("Emplacement", System.Type.GetType("System.String"));
	        stockOutTable.Columns.Add("ABC", System.Type.GetType("System.String"));
	        stockOutTable.Columns.Add("Qté stock", System.Type.GetType("System.String"));
	        stockOutTable.Columns.Add("Date rupture", System.Type.GetType("System.String"));
	        
	        foreach (DataRow forehistRow in forehistTable.Rows)
	        {
	        	if (!forehistRow[9].ToString().Equals("F"))
	        	{
	        		try
		        	{
	        			int onHand = Convert.ToInt32(forehistRow[11]);
			        	DateTime date = DateTime.Now;
			        	bool dateIsSet = false;
			        	
			        	if (onHand == 0)
			        	{
			        		dateIsSet = true;
			        	}
			        	else
			        	{
				        	foreach (DataRow stockRow in stockTable.Rows)
				        	{
				        		if (stockRow[0].ToString().Equals(forehistRow[0].ToString()))
				        		{
				        			for (int i = 0; i < 61; i++)
				        			{
				        				if (Convert.ToInt32(stockRow[i + 2]) <= 0)
				        				{
				        					dateIsSet = true;
				        					break;
				        				} else {
				        					date = date.AddDays(i);
				        				}
				        			}
				        			
				        			break;
				        		}
				        	}
			        	}
			        	
			        	if (dateIsSet)
			        	{
			        		IFormatProvider culture = new CultureInfo("en-US", true);
			        		DataRow newRow = stockOutTable.NewRow();
			        		
			        		newRow[0] = forehistRow[0].ToString();
			        		newRow[1] = forehistRow[2].ToString();
			        		newRow[2] = forehistRow[3].ToString();
			        		newRow[3] = forehistRow[9].ToString();
			        		newRow[4] = onHand.ToString();
			        		newRow[5] = date.ToShortDateString();
			        		
			        		stockOutTable.Rows.Add(newRow);
			        	}
		        	}
		        	catch(Exception)
	        		{
	        		
	        		}
	        	}
	        }
	            
			Export(stockOutTable);
		 	
		 	stockOutTable = null;
		}
		
		/// <summary>
		/// TO BE DELETED...
		/// </summary>
		public void ExportStockOutOld()
		{
			System.Data.DataTable stockOutTable = new System.Data.DataTable();

            stockOutTable.Columns.Add("Code Client", System.Type.GetType("System.String"));
	        stockOutTable.Columns.Add("Code Modling", System.Type.GetType("System.String"));
	        stockOutTable.Columns.Add("Emplacement", System.Type.GetType("System.String"));
	        stockOutTable.Columns.Add("ABC", System.Type.GetType("System.String"));
	        stockOutTable.Columns.Add("Qté stock", System.Type.GetType("System.String"));
	        stockOutTable.Columns.Add("Qté commandée", System.Type.GetType("System.String"));
	        stockOutTable.Columns.Add("Date rupture", System.Type.GetType("System.String"));
	        
	        foreach (DataRow forehistRow in forehistTable.Rows)
	        {
	        	if (!forehistRow[9].ToString().Equals("F"))
	        	{
	        		try
		        	{
			        	int custordsQuantity = 0;
			        	int onHand = Convert.ToInt32(forehistRow[11]);
			        	DateTime date = DateTime.Now;
			        	bool dateIsSet = false;
			        	
			        	if (onHand == 0)
			        	{
			        		dateIsSet = true;
			        	}
			        	
			        	foreach (DataRow custordsRow in custordsTable.Rows)
			        	{
			        		if (custordsRow[0].ToString().Equals(forehistRow[0].ToString()))
			        		{
			        			custordsQuantity += Convert.ToInt32(custordsRow[2]);
			        			
			        			if (!dateIsSet && ((onHand - custordsQuantity) <= 0))
			        			{
			        				date = DateTime.ParseExact(custordsRow[6].ToString(), "yyMMdd", null);
			        			}
			        		}
			        	}
			        	
			        	if ((onHand - custordsQuantity) <= 0)
			        	{
			        		IFormatProvider culture = new CultureInfo("en-US", true);
			        		DataRow newRow = stockOutTable.NewRow();
			        		
			        		newRow[0] = forehistRow[0].ToString();
			        		newRow[1] = forehistRow[2].ToString();
			        		newRow[2] = forehistRow[3].ToString();
			        		newRow[3] = forehistRow[9].ToString();
			        		newRow[4] = onHand.ToString();
			        		newRow[5] = custordsQuantity.ToString();
			        		newRow[6] = date.ToShortDateString();
			        		
			        		stockOutTable.Rows.Add(newRow);
			        	}
		        	}
		        	catch(Exception)
	        		{
	        		
	        		}
	        	}
	        }
	            
			Export(stockOutTable);
		 	
		 	stockOutTable = null;
		}
		
		public void ExportRushOrders()
		{
			System.Data.DataTable rushOrdersOutTable = new System.Data.DataTable();

	        rushOrdersOutTable.Columns.Add("Commande urgente", System.Type.GetType("System.String"));
	        rushOrdersOutTable.Columns.Add("Code Client", System.Type.GetType("System.String"));
	        rushOrdersOutTable.Columns.Add("Code Modling", System.Type.GetType("System.String"));
	        rushOrdersOutTable.Columns.Add("Qté client", System.Type.GetType("System.String"));
	        rushOrdersOutTable.Columns.Add("Qté Modling", System.Type.GetType("System.String"));
	        rushOrdersOutTable.Columns.Add("Date livraison", System.Type.GetType("System.String"));
	        rushOrdersOutTable.Columns.Add("Fournisseur", System.Type.GetType("System.String"));
	        rushOrdersOutTable.Columns.Add("Planner", System.Type.GetType("System.String"));
	        rushOrdersOutTable.Columns.Add("Multiple", System.Type.GetType("System.String"));
	        
	        foreach (DataRow ordPlanRow in ordPlanTable.Rows)
	        {
	        	if (ordPlanRow[1].ToString().Equals("0"))
	        	{
	        		try
	        		{
	        			IFormatProvider culture = new CultureInfo("en-US", true);
		        		DataRow newRow = rushOrdersOutTable.NewRow();
		        		
		        		newRow[0] = "";
		        		newRow[1] = ordPlanRow[3];
		        		newRow[2] = "?";
		        		newRow[3] = ordPlanRow[5];
		        		newRow[4] = "?";
		        		newRow[5] = DateTime.ParseExact(ordPlanRow[7].ToString().PadLeft(7, '0'), "ddMMMyy", culture).ToShortDateString();
		        		newRow[6] = "?";
		        		newRow[7] = "?";
		        		newRow[8] = 1;
		        		
		        		foreach (DataRow forehistRow in forehistTable.Rows)
		        		{
		        			if (forehistRow[0].Equals(ordPlanRow[3].ToString()))
		        			{
		        				string multiple = forehistRow[7].ToString();
		        				
		        				if (multiple.IndexOf('.') != -1)
		        				{
		        					multiple = multiple.Substring(0, multiple.IndexOf('.'));
		        				}
		        				
		        				newRow[2] = forehistRow[2];
		        				newRow[4] = (Convert.ToInt32(newRow[3]) * Convert.ToInt32(multiple));
		        				newRow[6] = forehistRow[3];
		        				newRow[7] = forehistRow[6];
		        				newRow[8] = multiple;
		        				
		        				break;
		        			}
		        		}
		        		
		        		rushOrdersOutTable.Rows.Add(newRow);
	        		}
	        		catch(Exception)
	        		{
	        		
	        		}
	        	}
	        }
	        
	        Export(rushOrdersOutTable);
	        
	        rushOrdersOutTable = null;
		}
		
		public void ExportStockOrders()
		{
			System.Data.DataTable stockOrdersTable = new System.Data.DataTable();

	        stockOrdersTable.Columns.Add("Commande", System.Type.GetType("System.String"));
	        stockOrdersTable.Columns.Add("Code Client", System.Type.GetType("System.String"));
	        stockOrdersTable.Columns.Add("Code Modling", System.Type.GetType("System.String"));
	        stockOrdersTable.Columns.Add("Qté client", System.Type.GetType("System.String"));
	        stockOrdersTable.Columns.Add("Qté Modling", System.Type.GetType("System.String"));
	        stockOrdersTable.Columns.Add("Date livraison", System.Type.GetType("System.String"));
	        stockOrdersTable.Columns.Add("Fournisseur", System.Type.GetType("System.String"));
	        stockOrdersTable.Columns.Add("Planner", System.Type.GetType("System.String"));
	        stockOrdersTable.Columns.Add("Multiple", System.Type.GetType("System.String"));
	        
	        foreach (DataRow ordPlanRow in ordPlanTable.Rows)
	        {
	        	
	        	if (ordPlanRow[1].ToString().Equals("1"))
	        	{
	        		try
	        		{
	        			IFormatProvider culture = new CultureInfo("en-US", true);
		        		DataRow newRow = stockOrdersTable.NewRow();
		        		
		        		newRow[0] = "";
		        		newRow[1] = ordPlanRow[3];
		        		newRow[2] = "?";
		        		newRow[3] = ordPlanRow[5];
		        		newRow[4] = "?";
		        		newRow[5] = DateTime.ParseExact(ordPlanRow[7].ToString().PadLeft(7, '0'), "ddMMMyy", culture).ToShortDateString();
		        		newRow[6] = "?";
		        		newRow[7] = "?";
		        		newRow[8] = 1;
		        		
		        		foreach (DataRow forehistRow in forehistTable.Rows)
		        		{
		        			if (forehistRow[0].Equals(ordPlanRow[3].ToString()))
		        			{
		        				string multiple = forehistRow[7].ToString();
		        				
		        				if (multiple.IndexOf('.') != -1)
		        				{
		        					multiple = multiple.Substring(0, multiple.IndexOf('.'));
		        				}
		        				
		        				newRow[2] = forehistRow[2];
		        				newRow[4] = (Convert.ToInt32(newRow[3]) * Convert.ToInt32(multiple));
		        				newRow[6] = forehistRow[3];
		        				newRow[7] = forehistRow[6];
		        				newRow[8] = multiple;
		        				
		        				break;
		        			}
		        		}
		        		
		        		stockOrdersTable.Rows.Add(newRow);
	        		}
	        		catch(Exception)
	        		{
	        		
	        		}
	        	}
	        }
	            
			Export(stockOrdersTable);
			
			stockOrdersTable = null;
		}
	}
}
