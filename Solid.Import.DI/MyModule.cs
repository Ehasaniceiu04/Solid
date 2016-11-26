using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Solid.Import.DI.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Solid.Import.DI
{
     [ExcludeFromCodeCoverage]
   public sealed class MyModule:NinjectModule
    {
       bool _isFileDataSource = false;
       internal MyModule( bool isFileDataSource)
       {
           _isFileDataSource = isFileDataSource;
       }
       public override void Load()
       {
           Bind<IImport>().To<Import>();
           Bind<IReader>().To<Reader>();
           Bind<IWriter>().To<SqlBulkCopyWriter>();
           Bind<Imapper>().To<Mapper>();
           Bind<IErrorHandler>().To<SqlBulkCopyErrorHandler>();
           Bind<ICacheTable>().To<CacheTable>();
           if (_isFileDataSource)
           {
               Bind<DbConnection>().To<OleDbConnection>();
               Bind<DbCommand>().To<OleDbCommand>();
           }
           else
           {
               Bind<DbConnection>().To<SqlConnection>();
               Bind<DbCommand>().To<SqlCommand>();
           }
           Bind<ICustomDataReader>().To<CustomDataReader>();
           Bind<IExpression>().To<VbScriptExpression>();
           //Bind<DbConnection>().To<OleDbConnection>().WhenInjectedInto(typeof(IFileDataSource));
           //Bind<DbCommand>().To<OleDbCommand>().WhenInjectedInto(typeof(IFileDataSource));
           //Bind<DbConnection>().To<SqlConnection>().WhenInjectedInto(typeof(IDatabaseSource));
           //Bind<DbCommand>().To<SqlCommand>().WhenInjectedInto(typeof(IDatabaseSource));
           //Bind<DbConnection>().To<SqlConnectio

           //Bind<DbConnection>().To<OleDbConnection>().Named("oledb");
           //Bind<DbCommand>().To<OleDbCommand>().Named("oledb");
           //Bind<DbConnection>().To<SqlConnection>().Named("sql");
           //Bind<DbCommand>().To<SqlCommand>().Named("sql");
       }
    }
}
