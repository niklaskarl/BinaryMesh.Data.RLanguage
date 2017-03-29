// -----------------------------------------------------------------------
// <copyright file="IRString.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.R.Graph
{
    /// <summary>
    /// Represents a string object in the R object graph.
    /// </summary>
    public interface IRString : IRNode
    {
        /// <summary>
        /// Gets the actual string of the node.
        /// </summary>
        string String { get; }
    }
}
