using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI
{
    public interface ICacheTable
    {
        void Create(List<ColumnMapping> columnMappings);
        void Clear( int batchSize);
        void Clear();
        DataRow CreateNewRow();
        void AddNewRow( DataRow newRow);
        DataTable GetCachedData();
    }
}
