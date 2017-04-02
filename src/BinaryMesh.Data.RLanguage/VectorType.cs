// -----------------------------------------------------------------------
// <copyright file="VectorType.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.RLanguage
{
    /// <summary>
    /// Specifies the data type of a <see cref="Vector"/>.
    /// </summary>
    public enum VectorType
    {
        /// <summary>
        /// The elements of the <see cref="Vector"/> are of type <see cref="double"/>.
        /// </summary>
        Real,

        /// <summary>
        /// The elements of the <see cref="Vector"/> are of type <see cref="int"/>.
        /// </summary>
        Integer,

        /// <summary>
        /// The elements of the <see cref="Vector"/> are of type <see cref="string"/>.
        /// </summary>
        String
    }
}
