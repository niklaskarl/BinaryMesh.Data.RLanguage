// -----------------------------------------------------------------------
// <copyright file="IRList.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.R.Internal
{
    public interface IRList : IRObject
    {
        IRObject Tag { get; set; }

        IRObject Head { get; set; }

        IRList Tail { get; set; }
    }
}
