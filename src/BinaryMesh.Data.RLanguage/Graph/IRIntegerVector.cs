﻿// -----------------------------------------------------------------------
// <copyright file="IRIntegerVector.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BinaryMesh.Data.RLanguage.Graph
{
    /// <summary>
    /// Represents a vector of <see cref="int"/> items in the R object graph.
    /// </summary>
    public interface IRIntegerVector : IRNode, IEnumerable<int>
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
        int this[long index] { get; set; }
    }
}
