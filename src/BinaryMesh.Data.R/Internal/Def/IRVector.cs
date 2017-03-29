using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryMesh.Data.R.Internal
{
    public interface IRVector : IRObject, IEnumerable
    {
        object this[long index] { get; set; }

        long Count { get; }
    }
}
