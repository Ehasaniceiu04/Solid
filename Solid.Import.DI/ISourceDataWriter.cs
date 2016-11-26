using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI
{
   public interface ISourceDataWriter
    {
        void WriteToServer(string connectionString, string tableName, DbDataReader reader, List<ColumnMapping> mappingColumNames);
    }
}
