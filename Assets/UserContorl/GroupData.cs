using ProtoBuf;
using System;
using System.Collections.Generic;

namespace VRClient
{
    [Serializable]
    [ProtoContract]
    public class GroupData
    {
        [ProtoBuf.ProtoMember(1)]
        public string name;

        [ProtoBuf.ProtoMember(2)]
        public string materialName;

        [ProtoBuf.ProtoMember(3)]
        public List<FaceIndices> faces;
        public GroupData()
        {
            faces = new List<FaceIndices>();
        }
        public bool isEmpty { get { return faces.Count == 0; } }
    }
}
