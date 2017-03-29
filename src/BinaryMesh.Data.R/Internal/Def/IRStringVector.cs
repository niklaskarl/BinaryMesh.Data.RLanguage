using System;
using System.Collections.Generic;

namespace BinaryMesh.Data.R.Internal
{
    public interface IRStringVector : IRObject, IEnumerable<IRString>
    {
        IRString this[long index] { get; set; }

        long Count { get; }
    }
}
