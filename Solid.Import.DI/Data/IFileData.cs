using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI.Data
{
    public interface IFileData:IData
    {
        string FileName { get; set; }
        string SheetName { get; set; } 
    }
}
