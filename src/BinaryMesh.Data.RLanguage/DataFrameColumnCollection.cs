using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryMesh.Data.RLanguage
{
    /// <summary>
    /// A collection of <see cref="DataFrameColumn"/> objects that can be searched by name.
    /// </summary>
    public sealed class DataFrameColumnCollection : IReadOnlyList<DataFrameColumn>, IList<DataFrameColumn>, IReadOnlyDictionary<string, DataFrameColumn>
    {
        private readonly DataFrameColumn[] _columns;

        internal DataFrameColumnCollection(DataFrameColumn[] columns)
        {
            _columns = columns;
        }

        /// <summary>
        /// Gets the number of <see cref="DataFrameColumn"/>s in the collection.
        /// </summary>
        public int Count => _columns.Length;

        /// <summary>
        /// Gets the names of all <see cref="DataFrameColumn"/>s in the collection.
        /// </summary>
        public IEnumerable<string> Keys
        {
            get
            {
                for (int i = 0; i < Count; i++)
                {
                    yield return this[i].Name;
                }
            }
        }

        /// <summary>
        /// Gets all <see cref="DataFrameColumn"/>s in the collection.
        /// </summary>
        public IEnumerable<DataFrameColumn> Values => this;

        bool ICollection<DataFrameColumn>.IsReadOnly => true;

        /// <summary>
        /// Gets the <see cref="DataFrameColumn"/> with the specified index.
        /// </summary>
        /// <param name="index">The index of the <see cref="DataFrameColumn"/> to get.</param>
        public DataFrameColumn this[int index] => _columns[index];

        /// <summary>
        /// Gets the <see cref="DataFrameColumn"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="DataFrameColumn"/> to get.</param>
        public DataFrameColumn this[string name]
        {
            get
            {
                if (TryGetValue(name, out DataFrameColumn value))
                {
                    return value;
                }

                throw new KeyNotFoundException();
            }
        }

        DataFrameColumn IList<DataFrameColumn>.this[int index]
        {
            get => _columns[index];
            set => throw new NotSupportedException();
        }

        /// <summary>
        /// Gets the <see cref="DataFrameColumn"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="DataFrameColumn"/> to get.</param>
        /// <param name="value">When this method returns, contains the <see cref="DataFrameColumn"/> with the specified name, if the name is found; otherwise, <c>null</c>. This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if the collection contains a <see cref="DataFrameColumn"/> with the specified name; otherwise, <c>false</c>.</returns>
        public bool TryGetValue(string name, out DataFrameColumn value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Name == name)
                {
                    value = this[i];
                    return true;
                }
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Searches for the specified <see cref="DataFrameColumn"/> and returns the zero-based index of the first occurrence within the collection.
        /// </summary>
        /// <param name="item">The <see cref="DataFrameColumn"/> to locate in the collection.</param>
        /// <returns>The zero-based index of the first occurrence of the <see cref="DataFrameColumn"/> within the collection, if found; otherwise, –1.</returns>
        public int IndexOf(DataFrameColumn item)
        {
            return ((IList<DataFrameColumn>)_columns).IndexOf(item);
        }

        /// <inheritdoc/>
        public bool Contains(DataFrameColumn item)
        {
            return ((IList<DataFrameColumn>)_columns).Contains(item);
        }

        /// <summary>
        /// Determines whether the collection contains a <see cref="DataFrameColumn"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the <see cref="DataFrameColumn"/> to locate in the collection.</param>
        /// <returns><c>true</c> if the collection contains a <see cref="DataFrameColumn"/> with the specified name; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(string name)
        {
            return TryGetValue(name, out DataFrameColumn value);
        }

        /// <summary>
        /// Copies the entire collection to a compatible one-dimensional array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the <see cref="DataFrameColumn"/>s copied from the collection. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(DataFrameColumn[] array, int arrayIndex)
        {
            ((IList<DataFrameColumn>)_columns).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="IEnumerator{T}"/> for the collection.</returns>
        public IEnumerator<DataFrameColumn> GetEnumerator()
        {
            return ((IList<DataFrameColumn>)_columns).GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, DataFrameColumn>> IEnumerable<KeyValuePair<string, DataFrameColumn>>.GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return new KeyValuePair<string, DataFrameColumn>(this[i].Name, this[i]);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void IList<DataFrameColumn>.Insert(int index, DataFrameColumn item)
        {
            throw new NotSupportedException();
        }

        void ICollection<DataFrameColumn>.Add(DataFrameColumn item)
        {
            throw new NotSupportedException();
        }

        void IList<DataFrameColumn>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        bool ICollection<DataFrameColumn>.Remove(DataFrameColumn item)
        {
            throw new NotSupportedException();
        }

        void ICollection<DataFrameColumn>.Clear()
        {
            throw new NotSupportedException();
        }
    }
}
