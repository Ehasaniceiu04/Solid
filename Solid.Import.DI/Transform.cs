using Ninject;
using Ninject.Parameters;
using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI
{
     [ExcludeFromCodeCoverage]
    public sealed class Transform
    {
        
        //Import _import;
        public Transform()
        {
            
        }
        public void Execute(IData source,IData target,string fulliQuailifiedTableName,List<ColumnMapping> columnMappings)
        {
            IKernel kernel = new StandardKernel(new MyModule(source.IsFileDataSource));
            IParameter sourceParameter = new ConstructorArgument("source", source);
            IParameter targetParameter = new ConstructorArgument("target", target);
            var _import = kernel.Get<IImport>(sourceParameter, targetParameter);
            _import.DoImport(fulliQuailifiedTableName, columnMappings);
        }
    }
}
