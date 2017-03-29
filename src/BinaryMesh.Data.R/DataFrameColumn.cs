// -----------------------------------------------------------------------
// <copyright file="DataFrameColumn.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using BinaryMesh.Data.R.Internal;

namespace BinaryMesh.Data.R
{
    public sealed class DataFrameColumn
    {
        private IRVector _column;

        internal DataFrameColumn(IRVector column)
        {
            _column = column;
            if (_column is IRStringVector vector)
            {
                _column = new StringVectorWrapper(vector);
            }
        }

        public long Count => _column.Count;

        public object this[long index]
        {
            get
            {
                return _column[index];
            }
        }

        public int GetInteger(long index)
        {
            if (_column is IRIntegerVector column)
            {
                return column[index];
            }

            throw new InvalidOperationException();
        }

        public double GetReal(long index)
        {
            if (_column is IRRealVector column)
            {
                return column[index];
            }

            throw new InvalidOperationException();
        }

        public string GetString(long index)
        {
            if (_column is StringVectorWrapper column)
            {
                return column[index];
            }

            throw new InvalidOperationException();
        }

        private class StringVectorWrapper : IRVector
        {
            private IRStringVector _vector;

            public StringVectorWrapper(IRStringVector vector)
            {
                _vector = vector;
            }

            public long Count => _vector.Count;

            public RObjectType ObjectType => RObjectType.Special;

            public int Levels
            {
                get => _vector.Levels;
                set => _vector.Levels = value;
            }

            public bool IsObject
            {
                get => _vector.IsObject;
                set => _vector.IsObject = value;
            }

            public IRObject Attribute
            {
                get => _vector.Attribute;
                set => _vector.Attribute = value;
            }

            public string this[long index]
            {
                get => _vector[index].Text;
                set => _vector[index] = new RString(value);
            }

            object IRVector.this[long index]
            {
                get => this[index];
                set => this[index] = (string)value;
            }

            public IEnumerator<string> GetEnumerator()
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
}
