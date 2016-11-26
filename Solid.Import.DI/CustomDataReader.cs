using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI
{
    public sealed class CustomDataReader : ICustomDataReader
    {


        private readonly ICacheTable _chacheTable;
        private readonly ICustomDataReader _this;
        public CustomDataReader(ICacheTable chacheTable)
        {
            _chacheTable = chacheTable;
            _this = this;
        }

        List<ColumnMapping> ICustomDataReader.ColumnMappings { get; set; }
        int ICustomDataReader.BatchSize { get; set; }
        IDataReader ICustomDataReader.DataReader { get; set; }

        void ICustomDataReader.CreateCacheTable()
        {
            _chacheTable.Create(_this.ColumnMappings);
        }
        public void Close()
        {
            throw new NotImplementedException();
        }

        public int Depth
        {
            get { throw new NotImplementedException(); }
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public bool IsClosed
        {
            get { throw new NotImplementedException(); }
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {

            bool isRead = _this.DataReader.Read();
            if (isRead)
            {
                _chacheTable.Clear(_this.BatchSize);
                DataRow newRow = _chacheTable.CreateNewRow();
                for (int i = 0; i < _this.DataReader.FieldCount; i++)
                {
                    newRow[i] = _this.DataReader.GetValue(i);

                }
                _chacheTable.AddNewRow(newRow);
            }

            return isRead;
        }
        DataTable ICustomDataReader.GetCachedData()
        {
            return _chacheTable.GetCachedData();
        }
        void ICustomDataReader.ClearCacheTable()
        {
            _chacheTable.Clear();
        }

        public int RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int FieldCount
        {
            get { return _this.DataReader.FieldCount; }
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            return _this.DataReader.GetOrdinal(name);
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int i)
        {
            return _this.DataReader.GetValue(i);
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public object this[string name]
        {
            get { throw new NotImplementedException(); }
        }

        public object this[int i]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
