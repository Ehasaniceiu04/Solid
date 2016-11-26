using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"D:\tmp\Book2.xlsx";
            string tableName = "LarData";
            string sheetName = "sheet1$";
            
            string conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Excel 12.0;Database=" + fileName + ";";
            string sql = "SELECT * FROM ["+sheetName+"]";
          

            using (OleDbConnection conn = new OleDbConnection(conStr))
            {
                conn.Open();
                using (var cmd = new OleDbCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        using (var destinationConn = new SqlConnection("Data Source=(local);Integrated Security=true;Initial Catalog=ImportTest;"))
                        {
                            destinationConn.Open();
                            using (var bulkCopy = new SqlBulkCopy(destinationConn, SqlBulkCopyOptions.TableLock, null))
                            {
                                string[] colNames = new string[] { "ent", "rec_no", "msa", "perc_min", "trct_incm", "app_incm", "ltv", "purpose", "fed_grn", "race", "co_race", "sex", "co_sex", "no_unit", "aff_catg" };
                                for (int i = 0; i < colNames.Length; i++)
                                    bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(colNames[i], colNames[i]));

                                bulkCopy.DestinationTableName = tableName;
                                bulkCopy.BulkCopyTimeout = 0;
                                bulkCopy.WriteToServer(reader);
                            }
                            destinationConn.Close();
                        }
                    }
                }

                conn.Close();
            }

        }
    }
}
