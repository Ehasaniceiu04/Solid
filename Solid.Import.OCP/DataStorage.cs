
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.OCP
{
    public class DataStorage:IDisposable
    {
        private readonly Mapper _mapper;
        private readonly DbConnection _dbConnection;
        private readonly DbCommand _dbCommand;

        public DataStorage()
        {
            _mapper = new Mapper();
            _dbConnection = new OleDbConnection();
            _dbCommand = new OleDbCommand();

        }
     public DbDataReader GetExcelData(string connectionString, string sql)
        {
           // OleDbConnection conn = new OleDbConnection(connectionString);
            _dbConnection.ConnectionString = connectionString;
            _dbConnection.Open();
            _dbCommand.Connection = _dbConnection;
            _dbCommand.CommandText = sql;
            return _dbCommand.ExecuteReader();
         
            //var cmd = new OleDbCommand(sql);
            //cmd.Connection = _dbConnection;
            //return cmd.ExecuteReader();
        }
      public void WriteToServer(string destinationConnectionString, string tableName, DbDataReader reader, string[] mappingColumNames)
        {
            using (var destinationConn = new SqlConnection(destinationConnectionString))
            {
                destinationConn.Open();
                using (var bulkCopy = new SqlBulkCopy(destinationConn, SqlBulkCopyOptions.TableLock, null))
                {
                    _mapper.AddColumnMapping(bulkCopy, mappingColumNames);

                    bulkCopy.DestinationTableName = tableName;
                    bulkCopy.BulkCopyTimeout = 0;
                    bulkCopy.WriteToServer(reader);
                }
                destinationConn.Close();
            }
        }





      void IDisposable.Dispose()
      {
          if (_dbConnection != null)
              _dbConnection.Dispose();
          if (_dbCommand != null)
              _dbCommand.Dispose();
      }
    }
}
