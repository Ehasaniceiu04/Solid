using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI
{
  public interface ISourceDataReader
    {
       DbDataReader GetData(string connectionString, string sql);
    }

     [ExcludeFromCodeCoverage]
   public sealed class SourceDataReader : ISourceDataReader
    {
        private readonly DbConnection _dbConnection;
        private readonly DbCommand _dbCommand;

        public SourceDataReader(DbConnection dbConnection, DbCommand dbCommand)
        {
            _dbConnection = dbConnection;
            _dbCommand = dbCommand;
        }
         DbDataReader ISourceDataReader.GetData(string connectionString, string sql)
        {
            _dbConnection.ConnectionString = connectionString;
            _dbConnection.Open();
            _dbCommand.Connection = _dbConnection;
            _dbCommand.CommandText = sql;
            return _dbCommand.ExecuteReader();
        }
    }

}
