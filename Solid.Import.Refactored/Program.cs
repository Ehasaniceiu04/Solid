using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.Refactored
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"D:\tmp\Book2.xlsx";
            string tableName = "LarData";
            string sheetName = "sheet1$";
            string[] mappingColumnNames = new string[] { "ent", "rec_no", "msa", "perc_min", "trct_incm", "app_incm", "ltv", "purpose", "fed_grn", "race", "co_race", "sex", "co_sex", "no_unit", "aff_catg" };
            string sourceConnectionString = GetConnectionString(fileName);
            string destinationConnectionString = GetDestinationConnectionString();
            string sql = GetSql(sheetName);
            OleDbDataReader reader = GetExcelData(sourceConnectionString, sql);
            WriteToServer(destinationConnectionString, tableName, reader, mappingColumnNames);
        }

        static string GetConnectionString(string fileName)
        {
            return @"Provider=Microsoft.ACE.OLEDB.12.0;Excel 12.0;Database=" + fileName + ";";
        }
        static string GetDestinationConnectionString()
        {
            return @"Data Source=(local);Integrated Security=true;Initial Catalog=ImportTest;";
        }
        static string GetSql(string sheetName)
        {
            return @"SELECT * FROM ["+sheetName+"]";
        }
        static OleDbDataReader GetExcelData(string connectionString, string sql)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            conn.Open();
            var cmd = new OleDbCommand(sql, conn);
            return cmd.ExecuteReader();
        }
        static void WriteToServer(string destinationConnectionString, string tableName, OleDbDataReader reader,string [] mappingColumNames)
        {
            using (var destinationConn = new SqlConnection(destinationConnectionString))
            {
                destinationConn.Open();
                using (var bulkCopy = new SqlBulkCopy(destinationConn, SqlBulkCopyOptions.TableLock, null))
                {
                    AddColumnMapping(bulkCopy, mappingColumNames);

                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.BulkCopyTimeout = 0;
                    bulkCopy.WriteToServer(reader);
                }
                destinationConn.Close();
            }
        }

        private static void AddColumnMapping(SqlBulkCopy bulkCopy, string[] mappingColumNames)
        {

            for (int i = 0; i < mappingColumNames.Length; i++)
                bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(mappingColumNames[i], mappingColumNames[i]));
        }
    }
}
