// -----------------------------------------------------------------------
// <copyright file="IRNode.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.R.Graph
{
    /// <summary>
    /// The base interface for all nodes in the R object graph.
    /// </summary>
    public interface IRNode
    {
        /// <summary>
        /// Gets the type of the node.
        /// </summary>
        RNodeType ObjectType { get; }

        /// <summary>
        /// Gets or sets the level of the node.
        /// </summary>
        int Levels { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the node is a object.
        /// </summary>
        bool IsObject { get; set; }

        /// <summary>
        /// Gets or sets the attribute node for the node.
        /// </summary>
        IRNode Attribute { get; set; }
    }
}
