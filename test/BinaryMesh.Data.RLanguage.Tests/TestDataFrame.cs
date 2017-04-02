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
            using (Stream stream = assembly.GetManifestResourceStream("BinaryMesh.Data.RLanguage.Tests.SampleData.simple.rds"))
            {
                DataFrame dataFrame = DataFrame.ReadFromStream(stream);

                Assert.AreEqual(dataFrame.Columns.Count, 2);
                Assert.AreEqual(dataFrame.RowCount, 10);

                Assert.IsTrue(dataFrame.Columns.ContainsKey("time"));
                Assert.IsTrue(dataFrame.Columns.ContainsKey("val"));
            }
        }

        [TestMethod]
        public void TestSimpleSerializeFile()
        {
            Assembly assembly = typeof(TestDataFrame).GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("BinaryMesh.Data.RLanguage.Tests.SampleData.simple_serialize.rds"))
            {
                DataFrame dataFrame = DataFrame.ReadFromStream(stream);

                Assert.AreEqual(dataFrame.Columns.Count, 2);
                Assert.AreEqual(dataFrame.RowCount, 10);

                Assert.IsTrue(dataFrame.Columns.ContainsKey("time"));
                Assert.IsTrue(dataFrame.Columns.ContainsKey("val"));
            }
        }
    }
}
