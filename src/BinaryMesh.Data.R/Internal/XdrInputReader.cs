// -----------------------------------------------------------------------
// <copyright file="XdrInputReader.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;

namespace BinaryMesh.Data.R.Internal
{
    internal sealed class XdrInputReader : InputReader
    {
        public XdrInputReader(BinaryReader reader)
            : base(reader)
        {
        }

        public override int ReadInt32()
        {
            byte[] buffer = new byte[4];
            _reader.Read(buffer, 0, 4);

            Array.Reverse(buffer);

            return BitConverter.ToInt32(buffer, 0);
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
                vector[i] = ReadInt32();
            }
        }

        public override void ReadRealVector(IRRealVector vector)
        {
            for (long i = 0; i < vector.Count; i++)
            {
                vector[i] = ReadDouble();
            }
        }

        private double ReadDouble()
        {
            byte[] buffer = new byte[8];
            _reader.Read(buffer, 0, 8);

            Array.Reverse(buffer);

            return BitConverter.ToDouble(buffer, 0);
        }
    }
}
