// -----------------------------------------------------------------------
// <copyright file="DataFrame.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using BinaryMesh.Data.RLanguage.Graph;

namespace BinaryMesh.Data.RLanguage
{
    /// <summary>
    /// A tabular data structure to store large sets of data.
    /// It is compatible with R data.frame objects and can be read from and
    /// stored to rds files to exchange data with R scripts.
    /// </summary>
    public sealed class DataFrame
    {
        internal DataFrame(IRNode node)
        {
            bool isDataFrame = false;
            string[] names = null;
            for (IRList attribute = node.Attribute as IRList; attribute != null; attribute = attribute.Tail)
            {
                if (attribute.Tag is IRString tag)
                {
                    switch (tag.String)
                    {
                        case "class":
                            isDataFrame = ProcessClassAttribute(attribute.Head);
                            break;
                        case "names":
                            names = ProcessNamesAttribute(attribute.Head);
                            break;
                        case "row.names":
                            ProcessRowNamesAttribute(attribute.Head);
                            break;
                    }
                }
            }

            if (!isDataFrame || names == null)
            {
                throw new InvalidDataException("The object is not a dataframe.");
            }

            if (node is IRGenericVector columns)
            {
                if (columns.Count != names.Length)
                {
                    throw new InvalidDataException("The number of column names doesn't match the number of columns.");
                }

                int rowCount = -1;
                DataFrameColumn[] c = new DataFrameColumn[columns.Count];
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
                        
                        c[i] = new DataFrameColumn(names[i], (IRVector)columns[i]);
                    }
                    else
                    {
                        throw new InvalidDataException();
                    }

                }

                Columns = new DataFrameColumnCollection(c);
                RowCount = rowCount;
            }
            else
            {
                throw new InvalidDataException();
            }
        }

        /// <summary>
        /// Gets a collection of all <see cref="DataFrameColumn"/>s of the data frame.
        /// </summary>
        public DataFrameColumnCollection Columns { get; }

        /// <summary>
        /// Gets the number of rows in the data frame.
        /// </summary>
        public long RowCount { get; }
        
        /// <summary>
        /// Reads a data frame from a serialized data source. This can either be a file created with
        /// the readRDS or the serialize function.
        /// </summary>
        /// <param name="stream">The stream from which to read the serialized data.</param>
        /// <returns>The unserialized data frame.</returns>
        public static DataFrame ReadFromStream(Stream stream)
        {
            IRNode node = Serializer.Unserialize(stream);
            return new DataFrame(node);
        }

        /// <summary>
        /// Serializes the data frame to a file and optionaly compresses it with gzip.
        /// The stream content will be compatible with the readRDS function of R.
        /// </summary>
        /// <param name="stream">The stream to which to write the serialized data frame.</param>
        public void WriteToStream(Stream stream)
        {
            WriteToStream(stream, true);
        }

        /// <summary>
        /// Serializes the data frame to a file and compresses it with gzip.
        /// If compression is used, the stream content will be compatible with the readRDS function of R.
        /// If no compression is used, the stream content will be compatible with the unserialize function of R.
        /// </summary>
        /// <param name="stream">The stream to which to write the serialized data frame.</param>
        /// <param name="compress">A value indicating whether to compress the data with gzip.</param>
        public void WriteToStream(Stream stream, bool compress)
        {
            IRGenericVector node = new RGenericVector(Columns.Count)
            {
                Attribute = new RList(RNodeType.List)
                {
                    Tag = new RString("names"),
                    Head = ConstructNamesAttribute(),
                    Tail = new RList(RNodeType.List)
                    {
                        Tag = new RString("row.names"),
                        Head = ConstructRowNamesAttribute(),
                        Tail = new RList(RNodeType.List)
                        {
                            Tag = new RString("class"),
                            Head = new RStringVector(1)
                            {
                                [0] = "data.frame"
                            }
                        }
                    }
                }
            };

            for (int i = 0; i < Columns.Count; i++)
            {
                node[i] = Columns[i].Vector.Node;
            }

            Serializer.Serialize(node, stream, compress);
        }

        private bool ProcessClassAttribute(IRNode item)
        {
            if (item is IRStringVector classes)
            {
                for (int i = 0; i < classes.Count; i++)
                {
                    if (classes[i] == "data.frame")
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private string[] ProcessNamesAttribute(IRNode item)
        {
            if (item is IRStringVector names)
            {
                string[] result = new string[names.Count];
                for (int i = 0; i < names.Count; i++)
                {
                    result[i] = names[i];
                }

                return result;
            }

            throw new InvalidDataException();
        }

        private void ProcessRowNamesAttribute(IRNode item)
        {
        }

        private IRNode ConstructNamesAttribute()
        {
            IRStringVector names = new RStringVector(Columns.Count);
            for (int i = 0; i < Columns.Count; i++)
            {
                names[i] = Columns[i].Name;
            }

            return names;
        }

        private IRNode ConstructRowNamesAttribute()
        {
            IRIntegerVector rowNames = new RIntegerVector(RowCount);
            for (int i = 0; i < Columns.Count; i++)
            {
                rowNames[i] = i;
            }

            return rowNames;
        }
    }
}
