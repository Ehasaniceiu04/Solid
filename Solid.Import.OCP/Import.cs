
using Solid.Import.OCP.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.OCP
{
    public class Import:IDisposable
    {
        private readonly DataStorage _dataStorage;
        private readonly IFileDataSource _source;
        private readonly Destination _destination;
        public Import(IFileDataSource source)
        {
            _source = source;
            _dataStorage = new DataStorage();
            _destination = new Destination();

        }
        public void DoImport(string tableName,string [] mappingColumns)
        {
            string sourceConnectionString = _source.GetConnectionString(_source.FileName);
            string sql = _source.GetSql(_source.sheetName);
            DbDataReader dataReader = _dataStorage.GetExcelData(sourceConnectionString, sql);
            string destinationConnectionString = _destination.GetDestinationConnectionString();
            _dataStorage.WriteToServer(destinationConnectionString, tableName, dataReader, mappingColumns);
        }

        void IDisposable.Dispose()
        {
            if (_source != null)
                ((IDisposable)_source).Dispose();
            if (_source != null)
                ((IDisposable)_dataStorage).Dispose();
            if (_source != null)
                ((IDisposable)_destination).Dispose();
        }
    }
}
