// -----------------------------------------------------------------------
// <copyright file="Vector.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using BinaryMesh.Data.RLanguage.Graph;

namespace BinaryMesh.Data.RLanguage
{
    /// <summary>
    /// A list of elements of a specific <see cref="VectorType"/>.
    /// </summary>
    public sealed class Vector
    {
        private readonly IRVector _vector;

        private readonly VectorType _type;

        internal Vector(IRNode node)
        {
            if (node is IRVector vector)
            {
                switch (vector.ObjectType)
                {
                    case RNodeType.Integer:
                        _type = VectorType.Integer;
                        break;
                    case RNodeType.Real:
                        _type = VectorType.Real;
                        break;
                    case RNodeType.String:
                        _type = VectorType.String;
                        break;
                    default:
                        throw new NotSupportedException();
                }

                _vector = vector;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Gets the type of the elements of the vector.
        /// </summary>
        public VectorType Type => _type;

        /// <summary>
        /// Gets the number of elements in the vector.
        /// </summary>
        public long Count => _vector.Count;

        internal IRVector Node => _vector;

        /// <summary>
        /// Gets element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element</param>
        /// <returns>The element at the specified index.</returns>
        public object this[long index]
        {
            get
            {
                return _vector[index];
            }
        }

        /// <summary>
        /// Reads a data frame from a serialized data source. This can either be a file created with
        /// the readRDS or the serialize function.
        /// </summary>
        /// <param name="stream">The stream from which to read the serialized data.</param>
        /// <returns></returns>
        public static Vector ReadFromStream(Stream stream)
        {
            IRNode node = Serializer.Unserialize(stream);
            return new Vector(node);
        }

        /// <summary>
        /// Gets element at the specified index as an integer.
        /// </summary>
        /// <param name="index">The index of the element</param>
        /// <returns>The element at the specified index as an integer.</returns>
        public int GetInteger(long index)
        {
            if (_vector is IRIntegerVector vector)
            {
                return vector[index];
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Gets element at the specified index as a real number.
        /// </summary>
        /// <param name="index">The index of the element</param>
        /// <returns>The element at the specified index as a double.</returns>
        public double GetReal(long index)
        {
            if (_vector is IRRealVector vector)
            {
                return vector[index];
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Gets element at the specified index as a string.
        /// </summary>
        /// <param name="index">The index of the element</param>
        /// <returns>The element at the specified index as a string.</returns>
        public string GetString(long index)
        {
            if (_vector is IRStringVector vector)
            {
                return vector[index];
            }

            throw new InvalidOperationException();
        }
    }
}
