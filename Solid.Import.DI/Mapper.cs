using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI
{
    public sealed class Mapper:Imapper
    {
      
        void Imapper.AddColumnMapping(SqlBulkCopy bulkCopy, List<ColumnMapping> mappingColumNames)
        {

            foreach (var column in mappingColumNames)
                bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(column.SourceColumn, column.DestinationColumn));
        }
    }
}
