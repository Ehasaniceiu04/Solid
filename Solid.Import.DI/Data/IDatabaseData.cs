using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI.Data
{
    public interface IDatabaseData : IData
    {
        string ServerName { get; set; }
        string DatabaseName { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string FulliQualifiedTableName { get; set; }
    }
}
