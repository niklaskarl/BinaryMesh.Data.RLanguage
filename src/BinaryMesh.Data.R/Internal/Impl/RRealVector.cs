using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryMesh.Data.R.Internal
{
    internal sealed class RRealVector : RObject, IRRealVector, IRVector
    {
        private double[] _values;

        public RRealVector(long length)
            : base(RObjectType.Real)
        {
            _values = new double[length];
        }

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

        public long Count => _values.Length;

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
