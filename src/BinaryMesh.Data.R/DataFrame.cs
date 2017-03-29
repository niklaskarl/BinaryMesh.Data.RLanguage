// -----------------------------------------------------------------------
// <copyright file="DataFrame.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BinaryMesh.Data.R.Graph;

namespace BinaryMesh.Data.R
{
    /// <summary>
    /// A tabular data structure to store large sets of data.
    /// It is compatible with R data.frame objects and can be read from and
    /// stored to rds files to exchange data with R scripts.
    /// </summary>
    public sealed class DataFrame : IReadOnlyDictionary<string, DataFrameColumn>
    {
        private IRNode _object;

        private KeyValuePair<string, DataFrameColumn>[] _columns;

        internal DataFrame(IRNode obj)
        {
            _object = obj;

            bool isDataFrame = false;
            for (IRList attribute = _object.Attribute as IRList; attribute != null; attribute = attribute.Tail)
            {
                if (attribute.Tag is IRString tag)
                {
                    switch (tag.String)
                    {
                        case "class":
                            isDataFrame = ProcessClassAttribute(attribute.Head);
                            break;
                        case "names":
                            ProcessNamesAttribute(attribute.Head);
                            break;
                        case "row.names":
                            ProcessRowNamesAttribute(attribute.Head);
                            break;
                    }
                }
            }

            if (!isDataFrame || _columns == null)
            {
                throw new InvalidDataException("The object is not a dataframe.");
            }

            if (_object is IRGenericVector columns)
            {
                if (columns.Count != _columns.Length)
                {
                    throw new InvalidDataException("The number of column names doesn't match the number of columns.");
                }

                int rowCount = -1;
                for (int i = 0; i < columns.Count; i++)
                {
                    if (columns[i] is IRVector item)
                    {
                        if (rowCount != item.Count)
                        {
                            if (rowCount == -1)
                            {
                                rowCount = (int)item.Count;
                            }
                            else
                            {
                                throw new InvalidDataException("The columns have different numbers of rows.");
                            }
                        }

                        _columns[i] = new KeyValuePair<string, DataFrameColumn>(_columns[i].Key, new DataFrameColumn(item));
                    }
                    else
                    {
                        throw new InvalidDataException();
                    }
                }

                RowCount = rowCount;
            }
            else
            {
                throw new InvalidDataException();
            }
        }

        /// <summary>
        /// Gets the number of columns in the data frame.
        /// </summary>
        public int Count => _columns.Length;

        /// <summary>
        /// Gets the number of rows in the data frame.
        /// </summary>
        public long RowCount { get; }

        /// <summary>
        /// Gets a enumeration of all column names in the data frame.
        /// </summary>
        public IEnumerable<string> Keys
        {
            get
            {
                for (int i = 0; i < _columns.Length; i++)
                {
                    yield return _columns[i].Key;
                }
            }
        }

        /// <summary>
        /// Gets a enumeration of all columns in the data frame.
        /// </summary>
        public IEnumerable<DataFrameColumn> Values
        {
            get
            {
                for (int i = 0; i < _columns.Length; i++)
                {
                    yield return _columns[i].Value;
                }
            }
        }

        /// <summary>
        /// Gets the column for a specific name.
        /// </summary>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>The column with the specified name.</returns>
        public DataFrameColumn this[string columnName]
        {
            get
            {
                if (TryGetValue(columnName, out DataFrameColumn item))
                {
                    return item;
                }

                throw new KeyNotFoundException();
            }
        }

        /// <summary>
        /// Reads a data frame from a serialized data source. This can either be a file created with
        /// the readRDS or the serialize function.
        /// </summary>
        /// <param name="stream">The stream from which to read the serialized data.</param>
        /// <returns>The unserialized data frame.</returns>
        public static DataFrame ReadFromStream(Stream stream)
        {
            IRNode obj = Serializer.Unserialize(stream);
            return new DataFrame(obj);
        }

        /// <summary>
        /// Checks whether the data frame contains a column with the specified name.
        /// </summary>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>A value indicating whether the data frame contains a column with the specified name or not.</returns>
        public bool ContainsKey(string columnName)
        {
            return _columns.Any(kvp => kvp.Key == columnName);
        }

        /// <summary>
        /// Tries to get the column with the specified name and returns a value indicating success.
        /// </summary>
        /// <param name="columnName">The name of the column.</param>
        /// <param name="column">When the operation was succeeefull, contains the column.</param>
        /// <returns>A value indicating whether the operation was successfull.</returns>
        public bool TryGetValue(string columnName, out DataFrameColumn column)
        {
            for (int i = 0; i < _columns.Length; i++)
            {
                if (_columns[i].Key == columnName)
                {
                    column = _columns[i].Value;
                    return true;
                }
            }

            column = null;
            return false;
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, DataFrameColumn>> GetEnumerator()
        {
            for (int i = 0; i < _columns.Length; i++)
            {
                yield return _columns[i];
            }
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool ProcessClassAttribute(IRNode item)
        {
            if (item is IRStringVector classes)
            {
                for (int i = 0; i < classes.Count; i++)
                {
                    if (classes[i].String == "data.frame")
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void ProcessNamesAttribute(IRNode item)
        {
            if (item is IRStringVector names)
            {
                _columns = new KeyValuePair<string, DataFrameColumn>[names.Count];
                for (int i = 0; i < names.Count; i++)
                {
                    _columns[i] = new KeyValuePair<string, DataFrameColumn>(names[i].String, null);
                }
            }
        }

        private void ProcessRowNamesAttribute(IRNode item)
        {
        }
    }
}
