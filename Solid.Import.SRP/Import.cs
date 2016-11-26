using Solid.Import.Refactored.SRP.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.SRP
{
    public class Import:IDisposable
    {
        private readonly DataStorage _dataStorage;
        private readonly Source _source;
        private readonly Destination _destination;
        public Import()
        {
            _source = new Source();
            _dataStorage = new DataStorage();
            _destination = new Destination();

        }
        public void DoImport(string fileName,string tableName,string sheetName,string [] mappingColumns)
        {
            string sourceConnectionString = _source.GetConnectionString(fileName);
            string sql = _source.GetSql(sheetName);
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
