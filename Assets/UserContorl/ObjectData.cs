using ProtoBuf;
using System;
using System.Collections.Generic;

namespace VRClient
{
    [Serializable]
    [ProtoContract]
    public class ObjectData
    {
        [ProtoBuf.ProtoMember(1)]
        public string name;

        [ProtoBuf.ProtoMember(2)]
        public List<GroupData> groups;

        [ProtoBuf.ProtoMember(3)]
        public List<FaceIndices> allFaces;
        public ObjectData()
        {
            groups = new List<GroupData>();
            allFaces = new List<FaceIndices>();
        }
    }
}
