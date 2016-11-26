using NUnit.Framework;
using Solid.Import.DI.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Import.DI.Tests
{
    [TestFixture]
   public class MapperTests
    {
        Imapper sut;
        List<ColumnMapping> columnList = null;
        SqlBulkCopy bulkCopy = null;
        [SetUp]
        public void SetUp()
        {
            columnList = new List<ColumnMapping> {
                new ColumnMapping{SourceColumn="Race",DestinationColumn="Race"}
            };
            sut = new Mapper();
            //bulkCopy = NSubstitute.Substitute.For<SqlBulkCopy>();
             bulkCopy = new SqlBulkCopy("Data Source=(loca);Integrated Security=true;Initial Catalog=ImportTes;", SqlBulkCopyOptions.TableLock);
        }
        [Test]
        public void addcolumnmapping_returns_mapping_column_if_coumn_name_has_value()
        {
            sut.AddColumnMapping(bulkCopy, columnList);
            Assert.AreEqual(true, bulkCopy.ColumnMappings.Count > 0);
        }
        [Test]
        public void addcolumnmapping_returns_nothing_if_coumn_name_has_no_value()
        {
            columnList = new List<ColumnMapping>();
            sut.AddColumnMapping(bulkCopy, columnList);
            Assert.AreEqual(true, bulkCopy.ColumnMappings.Count == 0);
        }
    }
}
