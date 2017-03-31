// -----------------------------------------------------------------------
// <copyright file="RIntegerVector.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryMesh.Data.RLanguage.Graph
{
    internal sealed class RIntegerVector : RObject, IRIntegerVector, IRVector
    {
        private int[] _values;

        public RIntegerVector(long length)
            : base(RNodeType.Integer)
        {
            _values = new int[length];
        }

        public long Count => _values.Length;

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
