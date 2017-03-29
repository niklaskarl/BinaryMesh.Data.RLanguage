// -----------------------------------------------------------------------
// <copyright file="RGenericVector.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryMesh.Data.R.Graph
{
    internal sealed class RGenericVector : RObject, IRGenericVector, IRVector
    {
        private IRNode[] _items;

        public RGenericVector(long length)
            : base(RNodeType.Vector)
        {
            _items = new IRNode[length];
        }

        public long Count => _items.Length;

        public IRNode this[long index]
        {
            get => _items[index];
            set => _items[index] = value;
        }

        object IRVector.this[long index]
        {
            get => _items[index];
            set => _items[index] = (IRNode)value;
        }

        IEnumerator<IRNode> IEnumerable<IRNode>.GetEnumerator()
        {
            for (long i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable<IRNode>).GetEnumerator();
        }
    }
}
