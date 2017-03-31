// -----------------------------------------------------------------------
// <copyright file="RVector.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.RLanguage.Graph
{
    internal static class RVector
    {
        public static IRVector AllocateVector(RNodeType type, long length)
        {
            switch (type)
            {
                case RNodeType.Real:
                    return new RRealVector(length);
                case RNodeType.String:
                    return new RStringVector(length);
                case RNodeType.Vector:
                    return new RGenericVector(length);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
