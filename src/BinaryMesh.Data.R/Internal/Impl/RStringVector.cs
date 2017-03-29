using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryMesh.Data.R.Internal
{
    internal sealed class RStringVector : RObject, IRStringVector, IRVector
    {
        private IRString[] _items;

        public RStringVector(long length)
            : base(RObjectType.String)
        {
            _items = new IRString[length];
        }
        
        public IRString this[long index]
        {
            get => _items[index];
            set => _items[index] = value;
        }

        object IRVector.this[long index]
        {
            get => _items[index];
            set => _items[index] = (IRString)value;
        }

        public long Count => _items.Length;
        
        IEnumerator<IRString> IEnumerable<IRString>.GetEnumerator()
        {
            for (long i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable<IRString>).GetEnumerator();
        }
    }
}
