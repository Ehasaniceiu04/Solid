using Solid.Import.OCP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.OCP
{
    class Program
    {
        static void Main(string[] args)
        {
          
            string tableName = "LarData";
            string[] mappingColumnNames = new string[] { "ent", "rec_no", "msa", "perc_min", "trct_incm", "app_incm", "ltv", "purpose", "fed_grn", "race", "co_race", "sex", "co_sex", "no_unit", "aff_catg" };
            IFileDataSource dataSource = new XLSXDataSource();
            dataSource.FileName=@"D:\tmp\Book2.xlsx";
            dataSource.sheetName = "sheet1$";
            using (var import = new Import(dataSource))
            {
                import.DoImport(tableName, mappingColumnNames);
            }
        }
    }
}
