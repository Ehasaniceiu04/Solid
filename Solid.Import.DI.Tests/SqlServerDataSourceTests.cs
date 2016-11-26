using NUnit.Framework;
using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI.Tests
{
    [TestFixture]
   public class SqlServerDataSourceTests
    {
        IDatabaseData sut;
        [SetUp]
        public void Setup()
        {
            sut = new SqlServerData();
        }
        [Test]
        public void connectionstring_should_return_empty_if_servername_username_password_databasename_empty()
        {
            sut.DatabaseName = string.Empty;
            sut.Password = string.Empty;
            sut.ServerName = string.Empty;
            sut.UserName = string.Empty;
            Assert.AreEqual("", sut.GetConnectionString());
        }
        [Test]
        public void connectionstring_should_return_value_if_all_property_has_value()
        {
            sut.DatabaseName = "ImportTest";
            sut.Password = "123456";
            sut.ServerName = "(local)";
            sut.UserName = "sa";
            string connectionString = string.Format("Server={0};Database={1};Trusted_Connection=True;", sut.ServerName, sut.DatabaseName);
            Assert.AreEqual(connectionString, sut.GetConnectionString());
        }
        [Test]
        public void connectionstring_should_return_empty_if_Server_name_is_empty()
        {
            sut.DatabaseName = string.Empty;
            sut.Password = "123456";
            sut.ServerName = "(local)";
            sut.UserName = "sa";
            string connectionString = string.Empty;
            Assert.AreEqual(connectionString, sut.GetConnectionString());
        }
        [Test]
        public void getsql_should__return_empty_table_name_empty()
        {
            sut.FulliQualifiedTableName = string.Empty;
            Assert.AreEqual(string.Empty, sut.GetSql());
        }
        [Test]
        public void getsql_should_return_value_if_tablename_has_value()
        {
            sut.FulliQualifiedTableName = "ehasan";
            string sql = string.Format("SELECT * FROM {0}", sut.FulliQualifiedTableName);
            Assert.AreEqual(sql, sut.GetSql());
        }
      
        
    }
}
