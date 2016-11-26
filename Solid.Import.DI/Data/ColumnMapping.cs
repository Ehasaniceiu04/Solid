using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI.Data
{
   public class ColumnMapping
    {
       public string SourceColumn { get; set; }
       public string DestinationColumn { get; set; }
       public string Expression { get; set; }
    }
}
