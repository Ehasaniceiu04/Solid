using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.OCP.Data
{
    internal class XLSXDataSource:IFileDataSource
    {

        string IFileDataSource.FileName
        {
            get;
            set;
        }

        string IFileDataSource.sheetName
        {
            get;
            set;
        }

        string IFileDataSource.GetConnectionString(string fileName)
        {
            return @"Provider=Microsoft.ACE.OLEDB.12.0;Excel 12.0;Database=" + fileName + ";";
        }
        string IFileDataSource.GetSql(string sheetName)
        {
            return @"Provider=Microsoft.ACE.OLEDB.12.0;Excel 12.0;Database=" + sheetName + ";";
        }
    }
}
