using NSubstitute;
using NUnit.Framework;
using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI.Tests
{
    [TestFixture]
   public  class ImportTests
    {
        private  IData _source;
        private  IData _target;
        private IReader _reader;
        private SqlBulkCopyDataWriterFake _writer;
        IImport sut;

        [SetUp]
        public void Setup()
        {
            _source=Substitute.For<IData>();
            _target = Substitute.For<IData>();
            _reader = Substitute.For<IReader>();
            _writer = new SqlBulkCopyDataWriterFake();
            sut = new Import(_source,_target,_reader,_writer);
        }
        [Test]
        public void TestReadee()
        {
            var reader=sut.TestSqlReader();
            while(reader.Read())
            {
                var data = reader[0];
            }
        }
        [Test]
        public void do_import_does_not_write_data_into_server_if_source_connectionstring_and_source_sql_both_are_empty()
        {
            _source.GetConnectionString().Returns(string.Empty);
            _source.GetSql().Returns(string.Empty);
            sut = new Import(_source, _target, _reader, _writer);
            sut.DoImport("ehasan", null);

            Assert.AreEqual(false, _writer.IsDataSaved);

        }
        [Test]
        public void do_import_does_not_write_data_into_server_if_sourceConnectionString_empty()
        {
            _source.GetConnectionString().Returns(string.Empty);
            sut = new Import(_source, _target, _reader, _writer);
            sut.DoImport("ehasan", null);
            Assert.AreEqual(false, _writer.IsDataSaved);
        
        }
        [Test]
        public void do_import_does_not_write_data_into_server_if_sourcesql_empty()
        {
            _source.GetConnectionString().Returns("ConnectiontionString");
            _source.GetSql().Returns(string.Empty);
            sut.DoImport("ehasan", null);
            Assert.AreEqual(false, _writer.IsDataSaved);

        }
        //[Test]
        //public void do_import_does_not_write_data_into_server_if_reader_null()
        //{
        //    OleDbDataReader reader=null;
        //    _reader.GetData("Ehasan", "sqlformat").Returns(reader);
        //    sut.DoImport("ehasan", null);
        //    Assert.AreEqual(false, _writer.IsDataSaved);

        //}

        [Test]
        public void do_import_does_not_write_data_into_server_if_destinationConnectionstring_is_empty()
        {
            _source.GetConnectionString().Returns("ConnectiontionString");
            _source.GetSql().Returns("Sql");
            _target.GetConnectionString().Returns(string.Empty);
        
            sut = new Import(_source, _target, _reader, _writer);
            sut.DoImport("ehasan", null);
            Assert.AreEqual(false, _writer.IsDataSaved);

        }
        [Test]
        public void do_import_does__write_data_into_server_if_all_above_conditions_met()
        {
            _source.GetConnectionString().Returns("ConnectiontionString");
            _source.GetSql().Returns("Sql");
            _target.GetConnectionString().Returns("ConnectiontionString");

            sut = new Import(_source, _target, _reader, _writer);
            sut.DoImport("ehasan", null);
            Assert.AreEqual(true, _writer.IsDataSaved);

        }
       
    }
    
}
