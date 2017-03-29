// -----------------------------------------------------------------------
// <copyright file="IRStringVector.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BinaryMesh.Data.R.Internal
{
    public interface IRStringVector : IRObject, IEnumerable<IRString>
    {
        long Count { get; }

        IRString this[long index] { get; set; }
    }
}
