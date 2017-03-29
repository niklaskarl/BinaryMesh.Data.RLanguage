// -----------------------------------------------------------------------
// <copyright file="RList.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.R.Graph
{
    internal sealed class RList : RObject, IRList
    {
        public RList(RNodeType type)
            : base(type)
        {
        }

        public IRNode Tag { get; set; }

        public IRNode Head { get; set; }

        public IRList Tail { get; set; }
    }
}
