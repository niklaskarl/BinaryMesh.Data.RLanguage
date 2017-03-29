// -----------------------------------------------------------------------
// <copyright file="RIntegerVector.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryMesh.Data.R.Internal
{
    internal sealed class RIntegerVector : RObject, IRIntegerVector, IRVector
    {
        private int[] _values;

        public RIntegerVector(long length)
            : base(RObjectType.Integer)
        {
            _values = new int[length];
        }

        public int this[long index]
        {
            get => _values[index];
            set => _values[index] = value;
        }

        object IRVector.this[long index]
        {
            get => _values[index];
            set => _values[index] = (int)value;
        }

        public long Count => _values.Length;

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
