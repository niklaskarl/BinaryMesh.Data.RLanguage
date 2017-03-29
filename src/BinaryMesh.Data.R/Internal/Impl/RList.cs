// -----------------------------------------------------------------------
// <copyright file="RList.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.R.Internal
{
    internal sealed class RList : RObject, IRList
    {
        public RList(RObjectType type)
            : base(type)
        {
        }

        public IRObject Tag { get; set; }

        public IRObject Head { get; set; }

        public IRList Tail { get; set; }
    }
}
