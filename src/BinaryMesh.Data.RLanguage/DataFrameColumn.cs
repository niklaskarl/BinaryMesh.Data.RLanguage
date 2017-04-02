// -----------------------------------------------------------------------
// <copyright file="DataFrameColumn.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using BinaryMesh.Data.RLanguage.Graph;

namespace BinaryMesh.Data.RLanguage
{
    /// <summary>
    /// Represents a column in a data frame.
    /// </summary>
    public sealed class DataFrameColumn
    {
        private readonly string _name;

        private readonly IRVector _column;

        internal DataFrameColumn(string name, IRVector column)
        {
            _name = name;
            _column = column;
        }

        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Gets the data type of the elements in the column.
        /// </summary>
        public DataFrameColumnType DataType
        {
            get
            {
                if (_column is IRIntegerVector)
                {
                    return DataFrameColumnType.Integer;
                }
                else if (_column is IRRealVector)
                {
                    return DataFrameColumnType.Real;
                }
                else if (_column is IRStringVector)
                {
                    return DataFrameColumnType.String;
                }

                throw new Exception();
            }
        }

        /// <summary>
        /// Gets the number of rows in the column.
        /// </summary>
        public long Count => _column.Count;

        /// <summary>
        /// Gets element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element</param>
        /// <returns>The element at the specified index.</returns>
        public object this[long index]
        {
            get
            {
                return _column[index];
            }
        }

        /// <summary>
        /// Gets element at the specified index as an integer.
        /// </summary>
        /// <param name="index">The index of the element</param>
        /// <returns>The element at the specified index as an integer.</returns>
        public int GetInteger(long index)
        {
            if (_column is IRIntegerVector column)
            {
                return column[index];
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Gets element at the specified index as a double.
        /// </summary>
        /// <param name="index">The index of the element</param>
        /// <returns>The element at the specified index as a double.</returns>
        public double GetReal(long index)
        {
            if (_column is IRRealVector column)
            {
                return column[index];
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Gets element at the specified index as a string.
        /// </summary>
        /// <param name="index">The index of the element</param>
        /// <returns>The element at the specified index as a string.</returns>
        public string GetString(long index)
        {
            if (_column is IRStringVector column)
            {
                return column[index];
            }

            throw new InvalidOperationException();
        }

        internal IRNode GetVectorNode()
        {
            return _column;
        }
    }
}
