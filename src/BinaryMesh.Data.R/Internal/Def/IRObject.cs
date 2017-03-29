using System;

namespace BinaryMesh.Data.R.Internal
{
    public interface IRObject
    {
        RObjectType ObjectType { get; }

        int Levels { get; set; }

        bool IsObject { get; set; }

        IRObject Attribute { get; set; }
    }
}
