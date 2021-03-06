﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryMesh.Data.RLanguage.Tests
{
    [TestClass]
    public sealed class TestVector
    {
        [TestMethod]
        public void TestSimpleFile()
        {
            Assembly assembly = typeof(TestDataFrame).GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("BinaryMesh.Data.RLanguage.Tests.SampleData.simple_vector.rds"))
            {
                Vector vector = Vector.ReadFromStream(stream);

                Assert.AreEqual(6, vector.Count);
                Assert.AreEqual(VectorType.Real, vector.Type);
            }
        }
    }
}
