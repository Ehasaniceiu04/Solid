using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.Refactored.SRP.Data
{
   public class Source
    {
       public string GetConnectionString(string fileName)
       {
           return @"Provider=Microsoft.ACE.OLEDB.12.0;Excel 12.0;Database=" + fileName + ";";
       }
      public string GetSql(string sheetName)
       {
           return @"SELECT * FROM [" + sheetName + "]";
       }
    }
}
