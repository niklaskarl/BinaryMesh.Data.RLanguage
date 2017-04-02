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
        /// Checks whether the element at the specified index is <c>null</c>.
        /// </summary>
        /// <param name="index">The index of the element to check.</param>
        /// <returns><c>true</c> if the element is <c>null</c>; <c>false</c> otherwise.</returns>
        public bool IsNull(long index)
        {
            if (_vector is IRRealVector realVector)
            {
                double value = realVector[index];
                return double.IsNaN(value) && (BitConverter.DoubleToInt64Bits(value) & 0x0007FFFFFFFFFFFF) != 0;
            }
            else if (_vector is IRIntegerVector integerVector)
            {
                return integerVector[index] == int.MinValue;
            }
            else if (_vector is IRStringVector stringVector)
            {
                return stringVector[index] == null;
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Gets element at the specified index as an integer.
        /// </summary>
        /// <param name="index">The index of the element</param>
        /// <returns>The element at the specified index as an integer.</returns>
        public int GetInteger(long index)
        {
            if (TryGetInteger(index, out int value))
            {
                return value;
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Gets the element at the specified index as an integer.
        /// </summary>
        /// <param name="index">The index of the element to get.</param>
        /// <param name="value">When this method returns, contains the value for the element, if it is not <c>null</c>.</param>
        /// <returns><c>true</c> if the element is not <c>null</c>; <c>false</c> otherwise.</returns>
        public bool TryGetInteger(long index, out int value)
        {
            if (_vector is IRIntegerVector integerVector)
            {
                int element = integerVector[index];
                if (element == int.MinValue)
                {
                    value = default(int);
                    return false;
                }

                value = element;
                return true;
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Gets the element at the specified index as a real number.
        /// </summary>
        /// <param name="index">The index of the element</param>
        /// <returns>The element at the specified index as a double.</returns>
        public double GetReal(long index)
        {
            if (TryGetReal(index, out double value))
            {
                return value;
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Gets the element at the specified index as a real number.
        /// </summary>
        /// <param name="index">The index of the element to get.</param>
        /// <param name="value">When this method returns, contains the value for the element, if it is not <c>null</c>.</param>
        /// <returns><c>true</c> if the element is not <c>null</c>; <c>false</c> otherwise.</returns>
        public bool TryGetReal(long index, out double value)
        {
            if (_vector is IRRealVector realVector)
            {
                double element = realVector[index];
                if (double.IsNaN(element) && (BitConverter.DoubleToInt64Bits(element) & 0x0007FFFFFFFFFFFF) != 0)
                {
                    value = default(double);
                    return false;
                }

                value = element;
                return true;
            }
            else if (_vector is IRIntegerVector integerVector)
            {
                int element = integerVector[index];
                if (element == int.MinValue)
                {
                    value = default(double);
                    return false;
                }

                value = element;
                return true;
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
