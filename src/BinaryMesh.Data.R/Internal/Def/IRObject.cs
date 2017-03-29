// -----------------------------------------------------------------------
// <copyright file="IRObject.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.R.Internal
{
    /// <summary>
    /// The base interface for all objects in the R object graph.
    /// </summary>
    public interface IRObject
    {
        /// <summary>
        /// Gets the type of the node.
        /// </summary>
        RObjectType ObjectType { get; }

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
        IRObject Attribute { get; set; }
    }
}
