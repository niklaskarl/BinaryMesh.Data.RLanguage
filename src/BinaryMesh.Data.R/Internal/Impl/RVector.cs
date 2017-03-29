using System;

namespace BinaryMesh.Data.R.Internal
{
    internal static class RVector
    {
        public static IRVector AllocateVector(RObjectType type, long length)
        {
            switch (type)
            {
                case RObjectType.Real:
                    return new RRealVector(length);
                case RObjectType.String:
                    return new RStringVector(length);
                case RObjectType.Vector:
                    return new RGenericVector(length);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
