// -----------------------------------------------------------------------
// <copyright file="RObject.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.RLanguage.Graph
{
    internal class RObject : IRNode
    {
        public RObject(RNodeType type)
        {
            ObjectType = type;
        }

        public static IRNode EmptyEnvironment { get; } = new RStaticObject(RNodeType.Environment);

        public static IRNode BaseEnvironment { get; } = new RStaticObject(RNodeType.Environment);

        public static IRNode GlobalEnvironment { get; } = new RStaticObject(RNodeType.Environment);

        public static IRNode UnboundValue { get; } = new RStaticObject(RNodeType.Special);

        public static IRNode MissingArg { get; } = new RStaticObject(RNodeType.Special);

        public static IRNode BaseNamespace { get; } = new RStaticObject(RNodeType.Special);

        public RNodeType ObjectType { get; }

        public int Levels { get; set; }

        public bool IsObject { get; set; }

        public IRNode Attribute { get; set; }

        private sealed class RStaticObject : IRNode
        {
            public RStaticObject(RNodeType type)
            {
                ObjectType = type;
            }

            public RNodeType ObjectType { get; }

            public int Levels
            {
                get => 0;
                set => throw new NotSupportedException();
            }

            public bool IsObject
            {
                get => false;
                set => throw new NotSupportedException();
            }

            public IRNode Attribute
            {
                get => null;
                set => throw new NotSupportedException();
            }

            public IRNode Tag
            {
                get => null;
                set => throw new NotSupportedException();
            }

            public IRNode CAR
            {
                get => null;
                set => throw new NotSupportedException();
            }

            public IRNode CAD
            {
                get => null;
                set => throw new NotSupportedException();
            }
        }
    }
}
