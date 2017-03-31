// -----------------------------------------------------------------------
// <copyright file="RStringVector.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryMesh.Data.RLanguage.Graph
{
    internal sealed class RStringVector : RObject, IRStringVector, IRVector
    {
        private string[] _items;

        public RStringVector(long length)
            : base(RNodeType.String)
        {
            _items = new string[length];
        }

        public long Count => _items.Length;

        public string this[long index]
        {
            get => _items[index];
            set => _items[index] = value;
        }

        object IRVector.this[long index]
        {
            get => _items[index];
            set => _items[index] = (string)value;
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            for (long i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable<string>).GetEnumerator();
        }
    }
}
