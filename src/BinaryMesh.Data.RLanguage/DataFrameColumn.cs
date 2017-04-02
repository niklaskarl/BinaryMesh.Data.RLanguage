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

        private readonly Vector _vector;

        internal DataFrameColumn(string name, IRVector vector)
        {
            _name = name;
            _vector = new Vector(vector);
        }

        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Gets the vector containing the data of the column.
        /// </summary>
        public Vector Vector => _vector;
    }
}
