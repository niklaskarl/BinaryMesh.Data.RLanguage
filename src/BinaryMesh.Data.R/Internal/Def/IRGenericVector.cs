// -----------------------------------------------------------------------
// <copyright file="IRGenericVector.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BinaryMesh.Data.R.Internal
{
    public interface IRGenericVector : IRObject, IEnumerable<IRObject>
    {
        long Count { get; }

        IRObject this[long index] { get; set; }
    }
}
