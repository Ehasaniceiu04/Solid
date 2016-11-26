using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.Refactored.SRP.Data
{
    public class Mapper
    {
        public void AddColumnMapping(SqlBulkCopy bulkCopy, string[] mappingColumNames)
        {

            for (int i = 0; i < mappingColumNames.Length; i++)
                bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(mappingColumNames[i], mappingColumNames[i]));
        }
    }
}
