// -----------------------------------------------------------------------
// <copyright file="InputReader.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;

namespace BinaryMesh.Data.R.Internal
{
    internal enum InputReaderType
    {
        Ascii,
        Binary,
        Xdr
    }

    internal abstract class InputReader
    {
        private readonly BinaryReader _reader;

        protected InputReader(BinaryReader reader)
        {
            _reader = reader;
        }

        protected BinaryReader Reader => _reader;

        public static InputReader FromType(BinaryReader reader, InputReaderType type)
        {
            switch (type)
            {
                case InputReaderType.Ascii:
                    return new AsciiInputReader(reader);
                case InputReaderType.Binary:
                    return new BinaryInputReader(reader);
                case InputReaderType.Xdr:
                    return new XdrInputReader(reader);
                default:
                    throw new NotSupportedException();
            }
        }

        public abstract int ReadInt32();

        public abstract IRString ReadString(int length, RString.CharEncoding encoding);

        public abstract void ReadIntegerVector(IRIntegerVector vector);

        public abstract void ReadRealVector(IRRealVector vector);
    }
}
