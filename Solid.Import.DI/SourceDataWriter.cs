using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI
{
 

     [ExcludeFromCodeCoverage]
   public sealed class SqlBulkCopyDataWriter : ISourceDataWriter
   {
      private readonly Imapper _mapper;
 
       public SqlBulkCopyDataWriter(Imapper mapper,SqlConnection connectionProvider)
       {
           _mapper = mapper;
       }
       void ISourceDataWriter.WriteToServer(string connectionString, string tableName, DbDataReader reader, List<ColumnMapping> mappingColumNames)
       {
           using (var destinationConn = new SqlConnection(connectionString))
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
   }
}
