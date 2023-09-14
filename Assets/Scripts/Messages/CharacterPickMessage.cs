using System;

namespace Messages
{
    [Serializable]
    public struct CharacterPickMessage
    {
        public int ClientId;
        public int CharacterId;
    }
}