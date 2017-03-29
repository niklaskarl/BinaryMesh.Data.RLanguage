// -----------------------------------------------------------------------
// <copyright file="IRString.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.R.Internal
{
    /// <summary>
    /// Represents a string object in the R object graph.
    /// </summary>
    public interface IRString : IRObject
    {
        /// <summary>
        /// Gets the actual text of the node.
        /// </summary>
        string Text { get; }
    }
}
