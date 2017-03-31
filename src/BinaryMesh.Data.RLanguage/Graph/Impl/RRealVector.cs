// -----------------------------------------------------------------------
// <copyright file="RRealVector.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryMesh.Data.RLanguage.Graph
{
    internal sealed class RRealVector : RObject, IRRealVector, IRVector
    {
        private double[] _values;

        public RRealVector(long length)
            : base(RNodeType.Real)
        {
            _values = new double[length];
        }

        public long Count => _values.Length;

        public double this[long index]
        {
            get => _values[index];
            set => _values[index] = value;
        }

        object IRVector.this[long index]
        {
            get => _values[index];
            set => _values[index] = (double)value;
        }

        public IEnumerator<double> GetEnumerator()
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
