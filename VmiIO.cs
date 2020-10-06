/*
 * Created by SharpDevelop.
 * User: Benoit Le Guern
 * Date: 05/04/2007
 * Time: 10:50
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
	/// Description of VmiIO.
	/// </summary>
	public class VmiIO
	{
		public VmiIO()
		{
			
		}
		
		public static bool LoadFile(string fileName, ref DataTable table)
        {
            try
            {
                StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding(1252));

                string line;
                int lineCount = 1;

                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        DataRow row = table.NewRow();
                        int lineRow = 0;

                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            string strValue = line.Substring(lineRow, table.Columns[j].MaxLength).Trim();

                            try
                            {
                                if (table.Columns[j].Caption == "Number")
                                {
                                    row[j] = Convert.ToInt32(strValue).ToString();
                                }
                                else if (table.Columns[j].Caption == "Decimal")
                                {
                                    row[j] = Convert.ToDecimal(strValue).ToString();
                                }
                                else
                                {
                                    row[j] = strValue;
                                }
                            }
                            catch (FormatException)
                            {
                                
                            }
                            catch (OverflowException)
                            {
                                
                            }

                            lineRow += table.Columns[j].MaxLength;
                        }

                        table.Rows.Add(row);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        
                    }

                    lineCount++;
                }

                sr.Close();

                return true;
            }
            catch (Exception)
            {
               	
            }
            
            return false;
        }
		
		public static bool WriteFile(string fileName, DataTable table)
        {
            try
            {
                StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding(1252));

                foreach (DataRow row in table.Rows)
                {
                    string line = "";

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        string value = "";
                        
                        try
                        {
                            if (table.Columns[i].Caption == "Number")
                            {
                                int number = Convert.ToInt32(row[i].ToString());
                                value = number.ToString().PadLeft(table.Columns[i].MaxLength, ' ');

                            }
                            else if (table.Columns[i].Caption == "Decimal")
                            {
                                decimal number = Convert.ToDecimal(row[i].ToString());
                                value = number.ToString("F").PadLeft(table.Columns[i].MaxLength, ' ');
                            }
                            else
                            {
                                value = row[i].ToString().PadRight(table.Columns[i].MaxLength, ' ');
                            }
                        }
                        catch (FormatException)
                        {
                            value = row[i].ToString().PadRight(table.Columns[i].MaxLength, ' ');
                        }
                        catch (OverflowException)
                        {
                            value = " ".PadRight(table.Columns[i].MaxLength, ' ');
                        }

                        line += value;
                    }

                    sw.WriteLine(line);
                }

                sw.Close();

                return true;
            }
            catch (Exception)
            {
                
            }
            
            return false;
        }
		
		
	}
}
