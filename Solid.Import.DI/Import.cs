
using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Solid.Import.DI
{
    public sealed class Import:IImport
    {
        private readonly IData _source;
        private readonly IData _target;
        private readonly IReader _reader;
        private readonly IWriter _writer;
        public Import(IData source, IData target, IReader reader, IWriter writer)
        {
            _source = source;
            _reader = reader;
            _writer = writer;
            _target = target;
        }
        void IImport.DoImport(string tableName, List<ColumnMapping> mappingColumns)
        {
            string sourceConnectionString = _source.GetConnectionString();
            if (string.IsNullOrEmpty(sourceConnectionString)) return;
            string sql = _source.GetSql();
            if (string.IsNullOrEmpty(sql)) return;
            DbDataReader dataReader = _reader.GetData(sourceConnectionString, sql);
            string destinationConnectionString = _target.GetConnectionString();
            if (string.IsNullOrEmpty(destinationConnectionString)) return;
            _writer.WriteToServer(destinationConnectionString, tableName, dataReader, mappingColumns);
        }
        IDataReader IImport.TestSqlReader()
        {
            SqlDataReader reader;
           var connectionString = string.Format("Server={0};Database={1};Trusted_Connection=True;","(local)", "Test");
           var con = new SqlConnection(connectionString);
           
                con.Open();
                using(var cmd=new SqlCommand("SELECT TOP 1000 [BlogId],[Name] FROM [Test].[dbo].[Blogs]",con))
                {
                    reader = cmd.ExecuteReader();
                }
            
            return reader;
        }
    }
}
