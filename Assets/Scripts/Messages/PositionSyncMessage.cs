using System;

namespace Messages
{
    [Serializable]
    public struct PositionSyncMessage
    {
        public int networkObjId;
        public float destinationX;
        public float destinationY;
        public float destinationZ;
        public float actualX;
        public float actualY;
        public float actualZ;
    }
}