using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI.Data
{
    public class XLSXData:IFileData
    {

       public string FileName
        {
            get;
            set;
        }

       public string SheetName
        {
            get;
            set;
        }
       bool IData.IsFileDataSource { get { return true; } }
      

        string IData.GetConnectionString()
        {
            string connectionString = string.Empty;
            if (!string.IsNullOrEmpty(FileName))
            {
                //connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Excel 12.0;Database=" + FileName + ";";
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + FileName + ";Extended Properties=Excel 8.0;";
            }
            return connectionString;
        }

        string IData.GetSql()
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(SheetName))
            {
                sql = @"select * from " + SheetName;
            }
            return sql;
        }

        
    }
}
