using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryMesh.Data.RLanguage.Tests
{
    [TestClass]
    public sealed class TestDataFrame
    {
        [TestMethod]
        public void TestSimpleFile()
        {
            Assembly assembly = typeof(TestDataFrame).GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("BinaryMesh.Data.RLanguage.Tests.SampleData.simple_dataframe.rds"))
            {
                DataFrame dataFrame = DataFrame.ReadFromStream(stream);

                Assert.AreEqual(2, dataFrame.Columns.Count);
                Assert.AreEqual(10, dataFrame.RowCount);

                Assert.IsTrue(dataFrame.Columns.ContainsKey("time"));
                Assert.IsTrue(dataFrame.Columns.ContainsKey("val"));
            }
        }

        [TestMethod]
        public void TestSimpleSerializeFile()
        {
            Assembly assembly = typeof(TestDataFrame).GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("BinaryMesh.Data.RLanguage.Tests.SampleData.simple_dataframe_serialize.rds"))
            {
                DataFrame dataFrame = DataFrame.ReadFromStream(stream);

                Assert.AreEqual(2, dataFrame.Columns.Count);
                Assert.AreEqual(10, dataFrame.RowCount);

                Assert.IsTrue(dataFrame.Columns.ContainsKey("time"));
                Assert.IsTrue(dataFrame.Columns.ContainsKey("val"));
            }
        }
    }
}
