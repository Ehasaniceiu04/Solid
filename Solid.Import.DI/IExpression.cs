using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI
{
    interface IExpression
    {
        void PrepareScript(List<ColumnMapping> columnMappings);
        object Evaluate(string code);
        void Execute(string code);
    }
}
