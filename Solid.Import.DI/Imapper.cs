using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI
{
   public interface Imapper
    {
        void AddColumnMapping(SqlBulkCopy bulkCopy, List<ColumnMapping> mappingColumNames);
    }
}
