using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI
{
 

     [ExcludeFromCodeCoverage]
   public sealed class SqlBulkCopyWriter : IWriter
   {
      private readonly Imapper _mapper;
      private readonly IErrorHandler _errorHandler;
      private readonly ICustomDataReader _customDataReader;

      public SqlBulkCopyWriter(Imapper mapper, ICustomDataReader customDataReader, IErrorHandler errorHandler)
       {
           _mapper = mapper;
           _errorHandler = errorHandler;
           _customDataReader = customDataReader;
       }
       void IWriter.WriteToServer(string connectionString, string tableName, DbDataReader reader, List<ColumnMapping> mappingColumNames)
       {
           bool isFinished = false;
           using (var destinationConn = new SqlConnection(connectionString))
           {
               destinationConn.Open();

               using (var bulkCopy = new SqlBulkCopy(destinationConn, SqlBulkCopyOptions.TableLock, null))
               {
                  
                   _mapper.AddColumnMapping(bulkCopy, mappingColumNames);

                   _customDataReader.DataReader = reader ;
                   _customDataReader.ColumnMappings = mappingColumNames;
                   _customDataReader.BatchSize = 500;
                   _customDataReader.CreateCacheTable();
                   bulkCopy.DestinationTableName = tableName;
                   bulkCopy.BulkCopyTimeout = 0;
                   while (!isFinished)
                   {
                       try
                       {
                           bulkCopy.WriteToServer(_customDataReader);
                           isFinished = true;
                       }
                       catch (Exception exp)
                       {
                           var dtCacheData = _customDataReader.GetCachedData();
                           _errorHandler.AddRow(bulkCopy, dtCacheData, 200);
                           _customDataReader.ClearCacheTable();
                       }
                   }
               }
               destinationConn.Close();
           }
       }
   }
}
