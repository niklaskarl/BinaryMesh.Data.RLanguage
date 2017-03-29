using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryMesh.Data.R.Tests
{
    [TestClass]
    public sealed class TestDataFrame
    {
        [TestMethod]
        public void TestSimpleFile()
        {
            Assembly assembly = typeof(TestDataFrame).GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("BinaryMesh.Data.R.Tests.SampleData.simple.rds"))
            {
                DataFrame dataFrame = DataFrame.ReadFromStream(stream);

                Assert.AreEqual(dataFrame.Count, 2);
                Assert.AreEqual(dataFrame.RowCount, 10);

                Assert.IsTrue(dataFrame.ContainsKey("time"));
                Assert.IsTrue(dataFrame.ContainsKey("val"));
            }
        }
    }
}
