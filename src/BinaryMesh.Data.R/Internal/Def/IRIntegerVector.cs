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
