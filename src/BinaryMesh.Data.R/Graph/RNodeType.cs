// -----------------------------------------------------------------------
// <copyright file="RNodeType.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.R.Graph
{
    /// <summary>
    /// Defines the types of nodes in the R object graph.
    /// </summary>
    public enum RNodeType
    {
        /// <summary>
        /// The nil node.
        /// </summary>
        Nil = 0,

        /// <summary>
        /// A symbol node.
        /// </summary>
        Symbol = 1,

        /// <summary>
        /// A list node represented as a head and a tail.
        /// </summary>
        List = 2,

        /// <summary>
        /// A closure node.
        /// </summary>
        Closure = 3,

        /// <summary>
        /// A environment node.
        /// </summary>
        Environment = 4,

        /// <summary>
        /// A promise node.
        /// </summary>
        Promise = 5,

        /// <summary>
        /// A language construct node.
        /// </summary>
        Language = 6,

        /// <summary>
        /// A special form node.
        /// </summary>
        Special = 7,

        /// <summary>
        /// A builtin, non-special form node.
        /// </summary>
        Builtin = 8,

        /// <summary>
        /// A scalar string node.
        /// </summary>
        Char = 9,

        /// <summary>
        /// A vector node of logic elements.
        /// </summary>
        Logical = 10,

        /// <summary>
        /// A vector node of integer elements.
        /// </summary>
        Integer = 13,

        /// <summary>
        /// A vector node of real elements.
        /// </summary>
        Real = 14,

        /// <summary>
        /// A vector node of complex elements.
        /// </summary>
        Complex = 15,

        /// <summary>
        /// A vector node of string elements.
        /// </summary>
        String = 16,

        /// <summary>
        /// A dot-dot-dot node.
        /// </summary>
        Dot = 17,

        /// <summary>
        /// A node that makes "any" args work.
        /// </summary>
        Any = 18,

        /// <summary>
        /// A vector node of generic elements.
        /// </summary>
        Vector = 19,

        /// <summary>
        /// A vector node of expression elements.
        /// </summary>
        Expression = 20,

        /// <summary>
        /// A byte code node.
        /// </summary>
        ByteCode = 21,

        /// <summary>
        /// A external pointer node.
        /// </summary>
        ExternalPointer = 22,

        /// <summary>
        /// A weak reference node.
        /// </summary>
        WeakReference = 23,

        /// <summary>
        /// A raw bytes node.
        /// </summary>
        Raw = 24,

        /// <summary>
        /// A S4 non-vector node.
        /// </summary>
        S4 = 25,

        /// <summary>
        /// A fresh node created in a new page.
        /// </summary>
        New = 30,

        /// <summary>
        /// A node released by the garbage collector.
        /// </summary>
        Free = 31,

        /// <summary>
        /// A closure or builtin node.
        /// </summary>
        Function = 99,
    }
}
