using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI
{
   public interface IErrorHandler
    {
        void AddRow(SqlBulkCopy sqlBulkCopy, DataTable dtCacheData, int errorThreshold);
    }
}
