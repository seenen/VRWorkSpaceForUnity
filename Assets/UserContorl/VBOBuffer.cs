using LibraryGeometryFormat;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace VRClient
{

    [Serializable]
    [ProtoContract]
    public class VBOBuffer
    {
        [ProtoBuf.ProtoMember(1)]
        public List<ObjectData> objects = new List<ObjectData>();

        //  顶点
        [ProtoBuf.ProtoMember(2)]
        public List<_Vector3> vertices = new List<_Vector3>();

        //  UV
        [ProtoMember(3)]
        public List<_Vector2> uvs = new List<_Vector2>();

        //  法线
        [ProtoMember(4)]
        public List<_Vector3> normals = new List<_Vector3>();

        [ProtoMember(5)]
        public List<int> triangles = new List<int>();

        [ProtoMember(6)]
        public int t = 0;

        //  模型id
        [ProtoMember(7)]
        public int id { get; set; }

        [ProtoMember(8)]
        public ObjModelRawState state;

    }
}
