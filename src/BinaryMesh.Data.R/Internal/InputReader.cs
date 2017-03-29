// -----------------------------------------------------------------------
// <copyright file="InputReader.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;

namespace BinaryMesh.Data.R.Internal
{
    internal abstract class InputReader
    {
        protected readonly BinaryReader _reader;

        protected InputReader(BinaryReader reader)
        {
            _reader = reader;
        }

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

    internal enum InputReaderType
    {
        Ascii,
        Binary,
        Xdr
    }
}
