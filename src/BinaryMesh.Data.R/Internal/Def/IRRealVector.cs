// -----------------------------------------------------------------------
// <copyright file="IRRealVector.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BinaryMesh.Data.R.Internal
{
    public interface IRRealVector : IRObject, IEnumerable<double>
    {
        long Count { get; }

        double this[long index] { get; set; }
    }
}
