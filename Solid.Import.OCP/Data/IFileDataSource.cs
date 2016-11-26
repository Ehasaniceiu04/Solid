using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.OCP.Data
{
    public interface IFileDataSource
    {
        string FileName { get; set; }
        string sheetName { get; set; }

        string GetConnectionString(string fileName);
        string  GetSql(string sheetName);
        
    }
}
