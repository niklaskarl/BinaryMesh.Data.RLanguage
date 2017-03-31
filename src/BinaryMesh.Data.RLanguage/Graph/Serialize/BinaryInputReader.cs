// -----------------------------------------------------------------------
// <copyright file="BinaryInputReader.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;

namespace BinaryMesh.Data.RLanguage.Graph
{
    internal sealed class BinaryInputReader : InputReader
    {
        public BinaryInputReader(BinaryReader reader)
            : base(reader)
        {
        }

        public override int ReadInt32()
        {
            return Reader.ReadInt32();
        }

        public override IRString ReadString(int length, RString.CharEncoding encoding)
        {
            byte[] buffer = Reader.ReadBytes(length);
            return new RString(buffer, encoding);
        }

        public override void ReadIntegerVector(IRIntegerVector vector)
        {
            for (long i = 0; i < vector.Count; i++)
            {
                vector[i] = Reader.ReadInt32();
            }
        }

        public override void ReadRealVector(IRRealVector vector)
        {
            for (long i = 0; i < vector.Count; i++)
            {
                vector[i] = Reader.ReadDouble();
            }
        }
    }
}
