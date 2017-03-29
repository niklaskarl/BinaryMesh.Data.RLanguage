// -----------------------------------------------------------------------
// <copyright file="IRList.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.R.Graph
{
    /// <summary>
    /// Represents a list node in the R object graph.
    /// </summary>
    public interface IRList : IRNode
    {
        /// <summary>
        /// Gets or sets the tag node of the first list element.
        /// </summary>
        IRNode Tag { get; set; }

        /// <summary>
        /// Gets or sets the first node in the list.
        /// </summary>
        IRNode Head { get; set; }

        /// <summary>
        /// Gets or sets the list of all nodes but the first node in the list.
        /// </summary>
        IRList Tail { get; set; }
    }
}
