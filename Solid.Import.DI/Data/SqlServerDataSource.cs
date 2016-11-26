using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI.Data
{
    public class SqlServerDataSource : IDatabaseSource
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

      

        string IDataSource.GetConnectionString()
        {
            string connectionString = string.Empty;
            if (!string.IsNullOrEmpty(ServerName) && !string.IsNullOrEmpty(DatabaseName) && !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                connectionString = string.Format("Server={0};Database={1};Trusted_Connection=True;",ServerName,DatabaseName);
            }
            return connectionString;
        }

        string IDataSource.GetSql()
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
