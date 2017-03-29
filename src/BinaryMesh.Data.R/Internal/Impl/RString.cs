using System;
using System.Text;

namespace BinaryMesh.Data.R.Internal
{
    internal sealed class RString : IRString
    {
        private string _text;

        public RString(string text)
        {
            _text = text;
        }

        public RString(byte[] buffer, CharEncoding encoding)
        {
            Encoding enc;
            switch (encoding)
            {
                case CharEncoding.Native:
                    enc = Encoding.GetEncoding(0);
                    break;
                case CharEncoding.Utf8:
                    enc = Encoding.UTF8;
                    break;
                case CharEncoding.Latin1:
                    enc = Encoding.GetEncoding(1252);
                    break;
                case CharEncoding.Bytes:
                    enc = Encoding.UTF8;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _text = enc.GetString(buffer);
        }

        public static IRString NotAvailable { get; } = new RString(null);

        public RObjectType ObjectType => RObjectType.Char;

        public int Levels { get; set; }

        public bool IsObject { get; set; }

        public IRObject Attribute { get; set; }

        public IRObject Tag { get; set; }

        public IRObject CAR { get; set; }

        public IRObject CAD { get; set; }

        public string Text => _text;

        public enum CharEncoding
        {
            Native,
            Utf8,
            Latin1,
            Bytes
        }
    }
}
