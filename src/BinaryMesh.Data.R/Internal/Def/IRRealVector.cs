using System;
using System.Collections.Generic;

namespace BinaryMesh.Data.R.Internal
{
    public interface IRRealVector : IRObject, IEnumerable<double>
    {
        double this[long index] { get; set; }

        long Count { get; }
    }
}
