﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryMesh.Data.R.Internal
{
    internal sealed class RGenericVector : RObject, IRGenericVector, IRVector
    {
        private IRObject[] _items;

        public RGenericVector(long length)
            : base(RObjectType.Vector)
        {
            _items = new IRObject[length];
        }
        
        public IRObject this[long index]
        {
            get => _items[index];
            set => _items[index] = value;
        }

        object IRVector.this[long index]
        {
            get => _items[index];
            set => _items[index] = (IRObject)value;
        }

        public long Count => _items.Length;
        
        IEnumerator<IRObject> IEnumerable<IRObject>.GetEnumerator()
        {
            for (long i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable<IRObject>).GetEnumerator();
        }
    }
}