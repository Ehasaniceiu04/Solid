using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI
{
   public interface ICustomDataReader:IDataReader
    {
        IDataReader DataReader { get; set; }
        List<ColumnMapping> ColumnMappings { get; set; }
        int BatchSize { get; set; }
        void CreateCacheTable();
        DataTable GetCachedData();
        void ClearCacheTable();
    }
}
