// -----------------------------------------------------------------------
// <copyright file="IRString.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.R.Internal
{
    public interface IRString : IRObject
    {
        string Text { get; }
    }
}
