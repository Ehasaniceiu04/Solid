using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI.Data
{
   public class DelimitedData:IDelimitedData
    {
        public string DirectoryName { get; set; }
        public string FileName { get; set; }
        public bool FirstRowHasColumnName { get; set; }
        public string RowDelimeter { get; set; }
        public string ColumnDelimeter { get; set; }

        bool IData.IsFileDataSource { get { return true; } }

        string IData.GetConnectionString()
        {
            OleDbConnectionStringBuilder sbConnection = new OleDbConnectionStringBuilder();

            Debug.Assert(!string.IsNullOrEmpty(DirectoryName));

            String strExtendedProperties = String.Empty;
            string hdr_property = (FirstRowHasColumnName) ? "Yes" : "No";
            string fileExtension = Path.GetExtension(FileName).ToLower();
            if (fileExtension == ".csv" || fileExtension == ".txt")
            {
                sbConnection.DataSource = string.Format("{0}", DirectoryName);
                sbConnection.Provider = "Microsoft.Jet.OLEDB.4.0";
                strExtendedProperties = string.Format("text;HDR={0}", hdr_property);
                sbConnection.Add("Extended Properties", strExtendedProperties);
            }
            else
                throw new Exception("Source file is not valid!!");

            return sbConnection.ToString();
        }

        string IData.GetSql()
        {
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(FileName))
            {
                sql = string.Format("SELECT * FROM {0}", FileName);
            }
            return sql;
        }
    }
}
