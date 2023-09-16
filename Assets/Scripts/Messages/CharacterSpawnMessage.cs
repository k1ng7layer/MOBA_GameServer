using System;

namespace Messages
{
    [Serializable]
    public struct CharacterSpawnMessage
    {
        public int ClientId;
        public int CharacterId;
    }
}