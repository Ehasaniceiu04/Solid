using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI.Data
{
    interface IDelimitedData:IData
    {
         string DirectoryName { get; set; }
        string FileName { get; set; }
        bool FirstRowHasColumnName { get; set; }
        string RowDelimeter { get; set; }
        string ColumnDelimeter { get; set; }
    }
}
