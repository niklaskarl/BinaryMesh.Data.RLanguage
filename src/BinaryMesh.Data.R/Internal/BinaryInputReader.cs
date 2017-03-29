// -----------------------------------------------------------------------
// <copyright file="BinaryInputReader.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;

namespace BinaryMesh.Data.R.Internal
{
    internal sealed class BinaryInputReader : InputReader
    {
        public BinaryInputReader(BinaryReader reader)
            : base(reader)
        {
        }

        public override int ReadInt32()
        {
            return _reader.ReadInt32();
        }

        public override IRString ReadString(int length, RString.CharEncoding encoding)
        {
            byte[] buffer = _reader.ReadBytes(length);
            return new RString(buffer, encoding);
        }

        public override void ReadIntegerVector(IRIntegerVector vector)
        {
            for (long i = 0; i < vector.Count; i++)
            {
                vector[i] = _reader.ReadInt32();
            }
        }

        public override void ReadRealVector(IRRealVector vector)
        {
            for (long i = 0; i < vector.Count; i++)
            {
                vector[i] = _reader.ReadDouble();
            }
        }
    }
}
