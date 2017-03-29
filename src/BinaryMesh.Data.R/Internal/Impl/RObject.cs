// -----------------------------------------------------------------------
// <copyright file="RObject.cs" company="Binary Mesh">
// Copyright © Binary Mesh. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace BinaryMesh.Data.R.Internal
{
    internal class RObject : IRObject
    {
        public RObject(RObjectType type)
        {
            ObjectType = type;
        }

        public static IRObject EmptyEnvironment { get; } = new RStaticObject(RObjectType.Environment);

        public static IRObject BaseEnvironment { get; } = new RStaticObject(RObjectType.Environment);

        public static IRObject GlobalEnvironment { get; } = new RStaticObject(RObjectType.Environment);

        public static IRObject UnboundValue { get; } = new RStaticObject(RObjectType.Special);

        public static IRObject MissingArg { get; } = new RStaticObject(RObjectType.Special);

        public static IRObject BaseNamespace { get; } = new RStaticObject(RObjectType.Special);

        public RObjectType ObjectType { get; }

        public int Levels { get; set; }

        public bool IsObject { get; set; }

        public IRObject Attribute { get; set; }

        private sealed class RStaticObject : IRObject
        {
            public RStaticObject(RObjectType type)
            {
                ObjectType = type;
            }

            public RObjectType ObjectType { get; }

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

            public IRObject Attribute
            {
                get => null;
                set => throw new NotSupportedException();
            }

            public IRObject Tag
            {
                get => null;
                set => throw new NotSupportedException();
            }

            public IRObject CAR
            {
                get => null;
                set => throw new NotSupportedException();
            }

            public IRObject CAD
            {
                get => null;
                set => throw new NotSupportedException();
            }
        }
    }
}
