using System;

namespace BinaryMesh.Data.RLanguage
{
    /// <summary>
    /// Specifies the data type of a <see cref="DataFrameColumn"/>.
    /// </summary>
    public enum DataFrameColumnType
    {
        /// <summary>
        /// The items of the <see cref="DataFrameColumn"/> are of type <see cref="double"/>.
        /// </summary>
        Real,

        /// <summary>
        /// The items of the <see cref="DataFrameColumn"/> are of type <see cref="int"/>.
        /// </summary>
        Integer,

        /// <summary>
        /// The items of the <see cref="DataFrameColumn"/> are of type <see cref="string"/>.
        /// </summary>
        String
    }
}
