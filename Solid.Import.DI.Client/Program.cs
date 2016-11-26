using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new Transform();
            var source = new DelimitedData();
            source.FileName = "CSV.csv";
            source.DirectoryName = @"C:\Users\Ehasanul\Desktop\CSV";
            source.FirstRowHasColumnName = true;
            var target = new SqlServerData();
            target.ServerName = "(local)";
            target.DatabaseName = "ImportTest";
            target.UserName = "sa";
            target.Password = "Password_1";

            WriteSchemaIniFile(source.DirectoryName, source.FileName);

            List<ColumnMapping> columnMappings = GetMappingColumn();
            t.Execute(source, target, "CSVTest", columnMappings);
        }
       static void WriteSchemaIniFile(string directoryName,string fileName)
        {
            string schema = Path.Combine(directoryName, "Schema.ini");

            if (!File.Exists(schema))
            {
                using (StreamWriter writer = new StreamWriter(schema))
                {
                    writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "[{0}]", fileName));
                    writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "Format={0}", "CsvDelimited"));
                    //writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "TextDelimiter={0}", "\""));
                    writer.WriteLine(string.Format(CultureInfo.InvariantCulture, "TextDelimiter={0}", "'"));
                }
            }
        }
        static List<ColumnMapping> GetMappingColumn()
       {
           return new List<ColumnMapping>(){
                new ColumnMapping{ DestinationColumn="Name", SourceColumn="Name"},
                new ColumnMapping{ DestinationColumn="Title", SourceColumn="Title"},
                new ColumnMapping{ DestinationColumn="Description", SourceColumn="Description"},
            };
       }
        public static List<ColumnMapping> GetMappingColumn(string fileName, string sheetName)
        {
            List<ColumnMapping> coulmns = new List<ColumnMapping>();
            DataTable dt = null;
            String connString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                "Data Source=" + fileName + ";Extended Properties=Excel 8.0;";

            try
            {
                string sql = "SELECT * FROM [" + sheetName + "$]";
                using (OleDbConnection objConn2 = new OleDbConnection(connString))
                {
                    objConn2.Open();
                    using (var cmd = new OleDbCommand(sql, objConn2))
                    {
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            dt = reader.GetSchemaTable();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            ColumnMapping column;
            foreach (DataRow row in dt.Rows)
            {
                column = new ColumnMapping();
                column.SourceColumn = row["ColumnName"].ToString();
                column.DestinationColumn = row["ColumnName"].ToString();
                column.Expression = GetExpression(column.SourceColumn);
                coulmns.Add(column);
            }
            return coulmns;
        }
        private static string GetExpression(string columnName)
        {
            string expScript = "";
            switch (columnName.ToUpper())
            {
                case "RACE":
                    return @"If (Race = ""1"") Then
                                Race = ""X""
                            Else
                                 Race = ""Y""
                            End If";
                //                case "Sex":
                //                //    return @"Sex = UCase(sex)";  
                //                    return @"If (Sex = ""male"") Then
                //                                Sex = ""M""
                //                            ElseIf (Sex = ""female"") Then
                //                                 Sex = ""F""
                //                            End If";
            }
            return expScript;
        }
    }
}
