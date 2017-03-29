using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace BinaryMesh.Data.R.Internal
{
    public static class Serializer
    {
        public static IRObject ReadRds(Stream stream)
        {
            using (stream = new GZipStream(stream, CompressionMode.Decompress, true))
            {
                return Unserialize(stream);
            }
        }

        public static IRObject Unserialize(Stream stream)
        {
            using (BinaryReader binaryReader = new BinaryReader(stream, Encoding.ASCII, true))
            {
                InputReader reader = InFormat(binaryReader);

                /* Read the version numbers */
                int version = reader.ReadInt32();
                int writer_version = reader.ReadInt32();
                int release_version = reader.ReadInt32();

                if (version != 2)
                {
                    DecodeVersion(writer_version, out int vw, out int pw, out int sw);
                    if (release_version < 0)
                    {
                        throw new NotSupportedException($"Cannot read unreleased workspace version {version} written by experimental R {vw}.{pw}.{sw}.");
                    }
                    else
                    {
                        DecodeVersion(release_version, out int vm, out int pm, out int sm);
                        throw new NotSupportedException($"Cannot read workspace version {version} written by R {vw}.{pw}.{sw}. Need R {vm}.{pm}.{sm} or newer.");
                    }
                }

                /* Read the actual object back */
                return ReadItem(reader);
            }
        }

        private static InputReader InFormat(BinaryReader reader)
        {
            byte[] buffer;
            InputReaderType type;

            buffer = reader.ReadBytes(2);
            switch (buffer[0])
            {
                case (byte)'A':
                    type = InputReaderType.Ascii;
                    break;
                case (byte)'B':
                    type = InputReaderType.Binary;
                    break;
                case (byte)'X':
                    type = InputReaderType.Xdr;
                    break;
                case (byte)'\n':
                    /* GROSS HACK: ASCII unserialize may leave a trailing newline
                       in the stream.  If the stream contains a second
                       serialization, then a second unserialize will fail if such
                       a newline is present.  The right fix is to make sure
                       unserialize consumes exactly what serialize produces.  But
                       this seems hard because of the current use of whitespace
                       skipping in unserialize.  So a temporary hack to cure the
                       symptom is to deal with a possible leading newline.  I
                       don't think more than one is possible, but I'm not sure.
                       LT */
                    if (buffer[1] == 'A')
                    {
                        type = InputReaderType.Ascii;
                        reader.ReadByte();
                        break;
                    }

                    throw new InvalidDataException();
                default:
                    throw new InvalidDataException();
            }

            return InputReader.FromType(reader, type);
        }

        private static void DecodeVersion(int packed, out int v, out int p, out int s)
        {
            v = packed / 65536;
            packed = packed % 65536;

            p = packed / 256;
            packed = packed % 256;

            s = packed;
        }

        private static IRObject ReadItem(InputReader reader)
        {
            int flags = reader.ReadInt32();

            UnpackFlags(flags, out SEXPTYPE type, out int levels, out bool isObject, out bool hasAttribute, out bool hasTag);

            switch (type)
            {
                case SEXPTYPE.NILVALUE_SXP:
                    return null;
                case SEXPTYPE.EMPTYENV_SXP:
                    return RObject.EmptyEnvironment;
                case SEXPTYPE.BASEENV_SXP:
                    return RObject.BaseEnvironment;
                case SEXPTYPE.GLOBALENV_SXP:
                    return RObject.GlobalEnvironment;
                case SEXPTYPE.UNBOUNDVALUE_SXP:
                    return RObject.UnboundValue;
                case SEXPTYPE.MISSINGARG_SXP:
                    return RObject.MissingArg;
                case SEXPTYPE.BASENAMESPACE_SXP:
                    return RObject.BaseNamespace;
                case SEXPTYPE.REFSXP:
                    // return GetReadRef(ref_table, InRefIndex(stream, flags));
                    ThrowTypeNotSupported("REFSXSP");
                    break;
                case SEXPTYPE.PERSISTSXP:
                    // return PersistentRestore(reader, InStringVec(reader));
                    ThrowTypeNotSupported("PERSISTSXP");
                    break;
                case SEXPTYPE.SYMSXP:
                    return ReadItem(reader);
                    // return installChar(ReadItem(reader));
                case SEXPTYPE.PACKAGESXP:
                    // return R_FindPackageEnv(InStringVec(reader));
                    ThrowTypeNotSupported("PACKAGESXP");
                    break;
                case SEXPTYPE.NAMESPACESXP:
                    // return R_FindNamespace1(InStringVec(reader));
                    ThrowTypeNotSupported("NAMESPACESXP");
                    break;
                case SEXPTYPE.ENVSXP:
                    {
                        /*int locked = reader.ReadInt32();
                        RObject s = allocSExp(SEXPTYPE.ENVSXP);
                        SET_ENCLOS(s, ReadItem(reader));
                        SET_FRAME(s, ReadItem(reader));
                        SET_HASHTAB(s, ReadItem(reader));
                        SET_ATTRIB(s, ReadItem(reader));

                        if (Attribute(s) != R_NilValue && getAttrib(s, R_ClassSymbol) != R_NilValue)
                        {
                            SET_OBJECTS(s, 1);
                        }

                        if (locked != null)
                        {
                            R_LockEnvironment(s, false);
                        }

                        if (ENCLOS(s) == R_NilValue)
                        {
                            SET_ENCLOS(s, R_BaseEnv);
                        }

                        return s;*/
                        ThrowTypeNotSupported("ENVSXP");
                        break;
                    }
                case SEXPTYPE.LISTSXP:
                case SEXPTYPE.LANGSXP:
                case SEXPTYPE.CLOSXP:
                case SEXPTYPE.PROMSXP:
                case SEXPTYPE.DOTSXP:
                    IRList s = new RList((RObjectType)type)
                    {
                        Levels = levels,
                        IsObject = isObject,
                        Attribute = hasAttribute ? ReadItem(reader) : null,
                        Tag = hasTag ? ReadItem(reader) : null,
                        Head = ReadItem(reader),
                        Tail = (IRList)ReadItem(reader),
                    };
                    
                    /*if (type == CLOSXP && CLOENV(s) == R_NilValue) SET_CLOENV(s, R_BaseEnv);
                    else if (type == PROMSXP && PRENV(s) == R_NilValue) SET_PRENV(s, R_BaseEnv);*/

                    return s;
                default:
                    IRObject result;
                    switch (type)
                    {
                        case SEXPTYPE.CHARSXP:
                            {
                                int length = reader.ReadInt32();
                                if (length == -1)
                                {
                                    return RString.NotAvailable;
                                }
                                else
                                {
                                    RString.CharEncoding encoding = RString.CharEncoding.Native;
                                    if ((levels & (1 << 3)) != 0)
                                    {
                                        encoding = RString.CharEncoding.Utf8;
                                    }
                                    else if ((levels & (1 << 2)) != 0)
                                    {
                                        encoding = RString.CharEncoding.Latin1;
                                    }
                                    else if ((levels & (1 << 1)) != 0)
                                    {
                                        encoding = RString.CharEncoding.Bytes;
                                    }
                                    
                                    return reader.ReadString(length, encoding);
                                }
                            }
                        case SEXPTYPE.REALSXP:
                            {
                                long length = ReadLength(reader);
                                IRRealVector vector = new RRealVector(length);
                                reader.ReadRealVector(vector);

                                result = vector;
                                break;
                            }
                        case SEXPTYPE.INTSXP:
                            {
                                long length = ReadLength(reader);
                                IRIntegerVector vector = new RIntegerVector(length);
                                reader.ReadIntegerVector(vector);

                                result = vector;
                                break;
                            }
                        case SEXPTYPE.STRSXP:
                            {
                                long length = ReadLength(reader);
                                IRVector vector = RVector.AllocateVector((RObjectType)type, length);
                                for (long i = 0; i < length; i++)
                                {
                                    vector[i] = ReadItem(reader);
                                }

                                result = vector;

                                break;
                            }
                        case SEXPTYPE.VECSXP:
                        case SEXPTYPE.EXPRSXP:
                            {
                                long length = ReadLength(reader);
                                IRVector vector = RVector.AllocateVector((RObjectType)type, length);
                                for (long i = 0; i < length; i++)
                                {
                                    vector[i] = ReadItem(reader);
                                }

                                result = vector;
                                break;
                            }
                        default:
                            throw new NotSupportedException();
                    }

                    if (result.ObjectType != RObjectType.Char)
                    {
                        result.Levels = levels;
                    }

                    result.IsObject = isObject;

                    if (result.ObjectType == RObjectType.Char)
                    {
                        if (hasAttribute)
                        {
                            ReadItem(reader);
                        }
                    }
                    else
                    {
                        result.Attribute = hasAttribute ? ReadItem(reader) : null;
                    }

                    return result;
            }

            throw new NotSupportedException();
        }

        private static long ReadLength(InputReader reader)
        {
            int len = reader.ReadInt32();
            if (len < -1)
            {
                throw new InvalidDataException();
            }
            else if (len == -1)
            {
                uint len1 = (uint)reader.ReadInt32();
                uint len2 = (uint)reader.ReadInt32();

                long xlen = len1;
                if (len1 > 65536)
                {
                    throw new InvalidDataException();
                }

                return (xlen << 32) + len2;
            }
            else
            {
                return len;
            }
        }

        private static void UnpackFlags(int flags, out SEXPTYPE type, out int levels, out bool isObject, out bool hasAttribute, out bool hasTag)
        {
            type = (SEXPTYPE)(flags & 0xFF);
            levels = flags >> 12;
            isObject = (flags & (1 << 8)) != 0;
            hasAttribute = (flags & (1 << 9)) != 0;
            hasTag = (flags & (1 << 10)) != 0;
        }

        private static void ThrowTypeNotSupported(string type)
        {
            throw new NotSupportedException($"The data was of type '{type}', but this type is not supported by the library.");
        }
    }

    internal enum SEXPTYPE
    {
        NILSXP = 0,         /* nil = NULL */
        SYMSXP = 1,         /* symbols */
        LISTSXP = 2,        /* lists of dotted pairs */
        CLOSXP = 3,         /* closures */
        ENVSXP = 4,         /* environments */
        PROMSXP = 5,        /* promises: [un]evaluated closure arguments */
        LANGSXP = 6,        /* language constructs (special lists) */
        SPECIALSXP = 7,     /* special forms */
        BUILTINSXP = 8,     /* builtin non-special forms */
        CHARSXP = 9,        /* "scalar" string type (internal only)*/
        LGLSXP = 10,        /* logical vectors */
        INTSXP = 13,        /* integer vectors */
        REALSXP = 14,       /* real variables */
        CPLXSXP = 15,       /* complex variables */
        STRSXP = 16,        /* string vectors */
        DOTSXP = 17,        /* dot-dot-dot object */
        ANYSXP = 18,        /* make "any" args work */
        VECSXP = 19,        /* generic vectors */
        EXPRSXP = 20,       /* expressions vectors */
        BCODESXP = 21,      /* byte code */
        EXTPTRSXP = 22,     /* external pointer */
        WEAKREFSXP = 23,    /* weak reference */
        RAWSXP = 24,        /* raw bytes */
        S4SXP = 25,         /* S4 non-vector */

        NEWSXP = 30,        /* fresh node creaed in new page */
        FREESXP = 31,       /* node released by GC */

        FUNSXP = 99,        /* Closure or Builtin */


        REFSXP = 255,
        NILVALUE_SXP = 254,
        GLOBALENV_SXP =253,
        UNBOUNDVALUE_SXP = 252,
        MISSINGARG_SXP = 251,
        BASENAMESPACE_SXP = 250,
        NAMESPACESXP = 249,
        PACKAGESXP = 248,
        PERSISTSXP = 247,
        /* the following are speculative--we may or may not need them soon */
        CLASSREFSXP = 246,
        GENERICREFSXP = 245,
        BCREPDEF = 244,
        BCREPREF=  243,
        EMPTYENV_SXP = 242,
        BASEENV_SXP = 241
    }
}
