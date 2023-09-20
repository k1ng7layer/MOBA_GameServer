using System;

namespace Messages
{
    [Serializable]
    public struct PlayerDestinationMessage
    {
        public int clientId;
        public int networkObjectId;
        public float x;
        public float y;
        public float z;
    }
}