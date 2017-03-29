// -----------------------------------------------------------------------
// <copyright file="IRIntegerVector.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BinaryMesh.Data.R.Internal
{
    public interface IRIntegerVector : IRObject, IEnumerable<int>
    {
        int this[long index] { get; set; }

        long Count { get; }
    }
}
