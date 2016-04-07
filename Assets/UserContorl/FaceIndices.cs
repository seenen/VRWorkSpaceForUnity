using ProtoBuf;
using System;

namespace VRClient
{
    [Serializable]
    [ProtoContract]
    public struct FaceIndices
    {
        [ProtoBuf.ProtoMember(1)]
        public int vi;

        [ProtoBuf.ProtoMember(2)]
        public int vu;

        [ProtoMember(3)]
        public int vn;
    }

}
