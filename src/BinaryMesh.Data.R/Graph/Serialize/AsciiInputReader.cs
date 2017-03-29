// -----------------------------------------------------------------------
// <copyright file="AsciiInputReader.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace BinaryMesh.Data.R.Graph
{
    internal sealed class AsciiInputReader : InputReader
    {
        private CultureInfo _culture;

        private StringBuilder _wordBuilder;

        public AsciiInputReader(BinaryReader reader)
            : base(reader)
        {
            _culture = CultureInfo.InvariantCulture;
            _wordBuilder = new StringBuilder();
        }

        public override int ReadInt32()
        {
            string word = ReadWord();

            if (word == "NA")
            {
                return int.MinValue;
            }

            if (int.TryParse(word, NumberStyles.AllowLeadingSign, _culture, out int i))
            {
                return i;
            }

            throw new InvalidDataException();
        }

        public double ReadDouble()
        {
            string word = ReadWord();
            if (word == "NA")
            {
                // TODO is there a difference between NA an NaN?
                return double.NaN;
            }
            else if (word == "NaN")
            {
                return double.NaN;
            }
            else if (word == "Inf")
            {
                return double.PositiveInfinity;
            }
            else if (word == "-Inf")
            {
                return double.NegativeInfinity;
            }
            else
            {
                if (double.TryParse(word, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, _culture, out double d))
                {
                    return d;
                }

                throw new InvalidDataException();
            }
        }

        public override IRString ReadString(int length, RString.CharEncoding encoding)
        {
            if (encoding != RString.CharEncoding.Native)
            {
                throw new ArgumentOutOfRangeException();
            }

            char[] buffer = new char[length];
            char last = (char)Reader.ReadByte();

            do
            {
                last = (char)Reader.ReadByte();
            }
            while (char.IsWhiteSpace(last));

            for (int i = 0; i < length; i++)
            {
                if (last == '\\')
                {
                    last = (char)Reader.ReadByte();
                    switch (last)
                    {
                        case 'n':
                            buffer[i] = '\n';
                            last = (char)Reader.ReadByte();
                            break;
                        case 't':
                            buffer[i] = '\t';
                            last = (char)Reader.ReadByte();
                            break;
                        case 'v':
                            buffer[i] = '\v';
                            last = (char)Reader.ReadByte();
                            break;
                        case 'b':
                            buffer[i] = '\b';
                            last = (char)Reader.ReadByte();
                            break;
                        case 'r':
                            buffer[i] = '\r';
                            last = (char)Reader.ReadByte();
                            break;
                        case 'f':
                            buffer[i] = '\f';
                            last = (char)Reader.ReadByte();
                            break;
                        case 'a':
                            buffer[i] = '\a';
                            last = (char)Reader.ReadByte();
                            break;
                        case '\\':
                            buffer[i] = '\\';
                            last = (char)Reader.ReadByte();
                            break;
                        case '?':
                            buffer[i] = '?';
                            last = (char)Reader.ReadByte();
                            break;
                        case '\'':
                            buffer[i] = '\'';
                            last = (char)Reader.ReadByte();
                            break;
                        case '\"': /* closing " for emacs */
                            buffer[i] = '\"';
                            last = (char)Reader.ReadByte();
                            break;
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                            int d = 0;
                            for (int j = 0; last >= '0' && last < '8' && j < 3; j++)
                            {
                                d = (d * 8) + (last - '0');
                                last = (char)Reader.ReadByte();
                            }

                            buffer[i] = (char)d;
                            break;
                    }
                }
                else
                {
                    buffer[i] = last;
                    last = (char)Reader.ReadByte();
                }
            }

            return new RString(new string(buffer));
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

        private string ReadWord()
        {
            _wordBuilder.Clear();

            char c;

            do
            {
                c = (char)Reader.ReadByte();
            }
            while (char.IsWhiteSpace(c));

            try
            {
                while (!char.IsWhiteSpace(c))
                {
                    _wordBuilder.Append(c);
                    c = (char)Reader.ReadByte();
                }
            }
            catch (EndOfStreamException)
            {
            }

            return _wordBuilder.ToString();
        }
    }
}
