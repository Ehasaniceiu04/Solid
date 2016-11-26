using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Solid.Import.DI
{
    public interface IImport
    {
        void DoImport(string tableName, List<ColumnMapping> mappingColumns);
        IDataReader TestSqlReader();
    }
}
