// -----------------------------------------------------------------------
// <copyright file="RObjectType.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.R.Internal
{
    /// <summary>
    /// Defines the types of nodes in the R object graph.
    /// </summary>
    public enum RObjectType
    {
        /// <summary>
        /// The nil object.
        /// </summary>
        Nil = 0,                /* nil = NULL */

        /// <summary>
        /// A symbol object.
        /// </summary>
        Symbol = 1,             /* symbols */

        /// <summary>
        /// A list represented as a head and a tail.
        /// </summary>
        List = 2,               /* lists of dotted pairs */

        /// <summary>
        /// A closure object.
        /// </summary>
        Closure = 3,            /* closures */

        /// <summary>
        /// A environment object.
        /// </summary>
        Environment = 4,        /* environments */

        /// <summary>
        /// A promise object.
        /// </summary>
        Promise = 5,            /* promises: [un]evaluated closure arguments */

        /// <summary>
        /// A language construct.
        /// </summary>
        Language = 6,           /* language constructs (special lists) */

        /// <summary>
        /// A special form object.
        /// </summary>
        Special = 7,            /* special forms */

        /// <summary>
        /// A builtin, non-special form object.
        /// </summary>
        Builtin = 8,            /* builtin non-special forms */

        /// <summary>
        /// A scalar string object.
        /// </summary>
        Char = 9,               /* "scalar" string type (internal only)*/

        /// <summary>
        /// A vector of logic elements.
        /// </summary>
        Logical = 10,           /* logical vectors */

        /// <summary>
        /// A vector of integer elements.
        /// </summary>
        Integer = 13,           /* integer vectors */

        /// <summary>
        /// A vector of real elements.
        /// </summary>
        Real = 14,              /* real variables */

        /// <summary>
        /// A vector of complex elements.
        /// </summary>
        Complex = 15,           /* complex variables */

        /// <summary>
        /// A vector of string elements.
        /// </summary>
        String = 16,            /* string vectors */

        /// <summary>
        /// A dot-dot-dot object.
        /// </summary>
        Dot = 17,               /* dot-dot-dot object */

        /// <summary>
        /// Makes "any" args work.
        /// </summary>
        Any = 18,               /* make "any" args work */

        /// <summary>
        /// A vector of generic elements.
        /// </summary>
        Vector = 19,            /* generic vectors */

        /// <summary>
        /// A vector of expression elements.
        /// </summary>
        Expression = 20,        /* expressions vectors */

        /// <summary>
        /// A byte code.
        /// </summary>
        ByteCode = 21,          /* byte code */

        /// <summary>
        /// A external pointer reference.
        /// </summary>
        ExternalPointer = 22,   /* external pointer */

        /// <summary>
        /// A weak reference.
        /// </summary>
        WeakReference = 23,     /* weak reference */

        /// <summary>
        /// Raw bytes.
        /// </summary>
        Raw = 24,               /* raw bytes */

        /// <summary>
        /// A S4 non-vector.
        /// </summary>
        S4 = 25,                /* S4 non-vector */

        /// <summary>
        /// A fresh node created in a new page.
        /// </summary>
        New = 30,               /* fresh node creaed in new page */

        /// <summary>
        /// A node released by the garbage collector.
        /// </summary>
        Free = 31,              /* node released by GC */

        /// <summary>
        /// A closure or builtin object.
        /// </summary>
        Function = 99,          /* Closure or Builtin */
    }
}
