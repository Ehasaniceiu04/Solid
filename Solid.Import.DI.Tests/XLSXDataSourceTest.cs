using NSubstitute;
using NUnit.Framework;
using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI.Tests
{
    [TestFixture]
    public class XLSXDataSourceTest
    {
        IFileData sut;
        [SetUp]
        public void Setup()
        {
            sut = new XLSXData();
        }
        [Test]
        public void connectionstring_should_return_empty()
        {
            sut.FileName = string.Empty;
            Assert.AreEqual("", sut.GetConnectionString());
        }
        [Test]
        public void connectionstring_should_return_value()
        {
            sut.FileName = "ehasan";
            //string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Excel 12.0;Database=" + sut.FileName + ";";
            Assert.AreEqual(true, sut.GetConnectionString().Length>0);
        }
        [Test]
        public void getsql_should__return_empty()
        {
            sut.SheetName = string.Empty;
            Assert.AreEqual("", sut.GetSql());
        }
        [Test]
        public void getsql_should_return_value()
        {
            sut.SheetName = "ehasan";
            string sql = @"select * from " + sut.SheetName;
            Assert.AreEqual(sql, sut.GetSql());
        }
       
        
    }
}
