using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI.Data
{
    public interface IData
    {

        string GetConnectionString();
        string GetSql();
        bool IsFileDataSource { get; }

       
    }
}
