using Microsoft.ClearScript;
using Microsoft.ClearScript.Windows;
using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI
{
    internal sealed class VbScriptExpression : IExpression
    {
        ScriptEngine _engine = new VBScriptEngine();
        string _script = string.Empty;
        void IExpression.PrepareScript(List<ColumnMapping> columnMappings)
        {
            var transformableColumns = from c in columnMappings
                                       where !string.IsNullOrWhiteSpace(c.Expression)
                                       select c;
            foreach (ColumnMapping columnMapping in transformableColumns)
            {
                _script = _script + "\r\n" + columnMapping.Expression;
            }

        }
        object IExpression.Evaluate(string code)
        {
            return _engine.Evaluate(code);
        }
        void IExpression.Execute(string code)
        {
            _engine.Execute(code + "\r\n" + _script);
        }
    }
}
