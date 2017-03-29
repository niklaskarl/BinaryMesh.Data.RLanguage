// -----------------------------------------------------------------------
// <copyright file="IRVector.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;

namespace BinaryMesh.Data.R.Internal
{
    /// <summary>
    /// The base interface for all vectors in the R object graph.
    /// </summary>
    public interface IRVector : IRObject, IEnumerable
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
        object this[long index] { get; set; }
    }
}
