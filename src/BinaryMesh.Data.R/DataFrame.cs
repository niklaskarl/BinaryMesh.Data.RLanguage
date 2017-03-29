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
using BinaryMesh.Data.R.Internal;

namespace BinaryMesh.Data.R
{
    public sealed class DataFrame : IReadOnlyDictionary<string, DataFrameColumn>
    {
        private IRObject _object;

        private KeyValuePair<string, DataFrameColumn>[] _columns;

        internal DataFrame(IRObject obj)
        {
            _object = obj;

            bool isDataFrame = false;
            for (IRList attribute = _object.Attribute as IRList; attribute != null; attribute = attribute.Tail)
            {
                if (attribute.Tag is IRString tag)
                {
                    switch (tag.Text)
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

        public DataFrameColumn this[string key]
        {
            get
            {
                if (TryGetValue(key, out DataFrameColumn item))
                {
                    return item;
                }

                throw new KeyNotFoundException();
            }
        }

        public int Count => _columns.Length;

        public long RowCount { get; }

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

        public static DataFrame ReadFromStream(Stream stream)
        {
            IRObject obj = Serializer.Unserialize(stream);
            return new DataFrame(obj);
        }

        public bool ContainsKey(string key)
        {
            return _columns.Any(kvp => kvp.Key == key);
        }

        public bool TryGetValue(string key, out DataFrameColumn value)
        {
            for (int i = 0; i < _columns.Length; i++)
            {
                if (_columns[i].Key == key)
                {
                    value = _columns[i].Value;
                    return true;
                }
            }

            value = null;
            return false;
        }

        public IEnumerator<KeyValuePair<string, DataFrameColumn>> GetEnumerator()
        {
            for (int i = 0; i < _columns.Length; i++)
            {
                yield return _columns[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool ProcessClassAttribute(IRObject item)
        {
            if (item is IRStringVector classes)
            {
                for (int i = 0; i < classes.Count; i++)
                {
                    if (classes[i].Text == "data.frame")
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void ProcessNamesAttribute(IRObject item)
        {
            if (item is IRStringVector names)
            {
                _columns = new KeyValuePair<string, DataFrameColumn>[names.Count];
                for (int i = 0; i < names.Count; i++)
                {
                    _columns[i] = new KeyValuePair<string, DataFrameColumn>(names[i].Text, null);
                }
            }
        }

        private void ProcessRowNamesAttribute(IRObject item)
        {
        }
    }
}
