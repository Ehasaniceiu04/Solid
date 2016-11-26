using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI
{
  public interface IReader
    {
       DbDataReader GetData(string connectionString, string sql);
    }

     [ExcludeFromCodeCoverage]
   public sealed class Reader : IReader
    {
        private readonly DbConnection _dbConnection;
        private readonly DbCommand _dbCommand;

        public Reader(DbConnection dbConnection, DbCommand dbCommand)
        {
            _dbConnection = dbConnection;
            _dbCommand = dbCommand;
        }
         DbDataReader IReader.GetData(string connectionString, string sql)
        {
            _dbConnection.ConnectionString = connectionString;
            _dbConnection.Open();
            _dbCommand.Connection = _dbConnection;
            _dbCommand.CommandText = sql;
            return _dbCommand.ExecuteReader();
        }
    }

}
