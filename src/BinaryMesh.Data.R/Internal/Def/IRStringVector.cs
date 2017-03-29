// -----------------------------------------------------------------------
// <copyright file="IRStringVector.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BinaryMesh.Data.R.Internal
{
    /// <summary>
    /// Represents a vector of <see cref="IRString"/> items in the R object graph.
    /// </summary>
    public interface IRStringVector : IRObject, IEnumerable<IRString>
    {
        /// <summary>
        /// Gets the number of elements in the vector.
        /// </summary>
        long Count { get; }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element.</param>
        /// <returns>The element at the specified index.</returns>
        IRString this[long index] { get; set; }
    }
}
