using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI.Data
{
    public class SqlServerData : IDatabaseData
    {
        public string ServerName
        {
            get;
            set;
        }

       public string DatabaseName
        {
            get;
            set;
        }

       public string UserName
        {
            get;
            set;
        }

       public string Password
        {
            get;
            set;
        }

       public string FulliQualifiedTableName
        {
            get;
            set;
        }

       bool IData.IsFileDataSource { get { return false; } }

        string IData.GetConnectionString()
        {
            string connectionString = string.Empty;
            if (!string.IsNullOrEmpty(ServerName) && !string.IsNullOrEmpty(DatabaseName) && !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                connectionString = string.Format("Server={0};Database={1};Trusted_Connection=True;",ServerName,DatabaseName);
            }
            return connectionString;
        }

        string IData.GetSql()
        {
            string sql = string.Empty;
            if(!string.IsNullOrEmpty(FulliQualifiedTableName))
            {
                sql=string.Format("SELECT * FROM {0}",FulliQualifiedTableName);
            }
            return sql;
        }

    }
}
