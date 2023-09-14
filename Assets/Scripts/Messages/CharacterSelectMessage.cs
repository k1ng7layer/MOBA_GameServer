using System;

namespace Messages
{
    [Serializable]
    public struct CharacterSelectMessage
    {
        public int ClientId;
        public int CharacterId;
    }
}