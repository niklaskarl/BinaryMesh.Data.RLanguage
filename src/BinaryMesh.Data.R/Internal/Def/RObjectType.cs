// -----------------------------------------------------------------------
// <copyright file="RObjectType.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.R.Internal
{
    public enum RObjectType
    {
        Nil = 0,                /* nil = NULL */
        Symbol = 1,             /* symbols */
        List = 2,               /* lists of dotted pairs */
        Closure = 3,            /* closures */
        Environment = 4,        /* environments */
        Promise = 5,            /* promises: [un]evaluated closure arguments */
        Language = 6,           /* language constructs (special lists) */
        Special = 7,            /* special forms */
        Builtin = 8,            /* builtin non-special forms */
        Char = 9,               /* "scalar" string type (internal only)*/
        Logical = 10,           /* logical vectors */
        Integer = 13,           /* integer vectors */
        Real = 14,              /* real variables */
        Complex = 15,           /* complex variables */
        String = 16,            /* string vectors */
        Dot = 17,               /* dot-dot-dot object */
        Any = 18,               /* make "any" args work */
        Vector = 19,            /* generic vectors */
        Expression = 20,        /* expressions vectors */
        ByteCode = 21,          /* byte code */
        ExternalPointer = 22,   /* external pointer */
        WeakReference = 23,     /* weak reference */
        Raw = 24,               /* raw bytes */
        S4 = 25,                /* S4 non-vector */

        New = 30,               /* fresh node creaed in new page */
        Free = 31,              /* node released by GC */

        Function = 99,          /* Closure or Builtin */
    }
}
