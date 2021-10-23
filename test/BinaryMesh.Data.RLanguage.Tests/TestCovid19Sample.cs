using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryMesh.Data.RLanguage.Tests
{
    [TestClass]
    public sealed class TestDataSamples
    {
        [TestMethod]
        public void TestCovid19Sample()
        {
            Assembly assembly = typeof(TestDataFrame).GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("BinaryMesh.Data.RLanguage.Tests.SampleData.covid_19_uncompressed.rds"))
            {
                DataFrame dataFrame = DataFrame.ReadFromStream(stream);

                Assert.AreEqual(8, dataFrame.Columns.Count);
                Assert.AreEqual(10089883, dataFrame.RowCount);

                Assert.IsTrue(dataFrame.Columns.ContainsKey("ID"));
                Assert.IsTrue(dataFrame.Columns.ContainsKey("Date"));
                Assert.IsTrue(dataFrame.Columns.ContainsKey("Cases"));
                Assert.IsTrue(dataFrame.Columns.ContainsKey("Cases_New"));
                Assert.IsTrue(dataFrame.Columns.ContainsKey("Type"));
                Assert.IsTrue(dataFrame.Columns.ContainsKey("Age"));
                Assert.IsTrue(dataFrame.Columns.ContainsKey("Sex"));
                Assert.IsTrue(dataFrame.Columns.ContainsKey("Source"));
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
