using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.SRP
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"D:\tmp\Book2.xlsx";
            string tableName = "LarData";
            string sheetName = "sheet1$";
            string[] mappingColumnNames = new string[] { "ent", "rec_no", "msa", "perc_min", "trct_incm", "app_incm", "ltv", "purpose", "fed_grn", "race", "co_race", "sex", "co_sex", "no_unit", "aff_catg" };
            using(var import=new Import())
            {
                import.DoImport(fileName,tableName,sheetName,mappingColumnNames);
            }
        }
    }
}
