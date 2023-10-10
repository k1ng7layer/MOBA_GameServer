using System;

namespace Messages
{
    [Serializable]
    public struct CharacterSpawnMessage
    {
        public int ClientId;
        public int CharacterId;
        public ushort NetworkId;
        public float PositionX;
        public float PositionY;
        public float PositionZ;
        public float RotationX;
        public float RotationY;
        public float RotationZ;
        public float RotationW;
    }
}