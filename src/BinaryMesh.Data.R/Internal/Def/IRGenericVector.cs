﻿using System;
using System.Collections.Generic;

namespace BinaryMesh.Data.R.Internal
{
    public interface IRGenericVector : IRObject, IEnumerable<IRObject>
    {
        IRObject this[long index] { get; set; }

        long Count { get; }
    }
}