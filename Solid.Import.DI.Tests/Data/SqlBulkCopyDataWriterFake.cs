using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI.Tests
{
    internal sealed class SqlBulkCopyDataWriterFake : IWriter
    {
        public bool IsDataSaved = false;
        void IWriter.WriteToServer(string connectionString, string tableName, System.Data.Common.DbDataReader reader, List<ColumnMapping> mappingColumNames)
        {
            IsDataSaved=true;
        }
    }
}
