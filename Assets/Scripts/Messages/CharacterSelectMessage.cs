using System;

namespace Messages
{
    [Serializable]
    public readonly struct CharacterSelectMessage
    {
        public readonly int ClientId;
        public readonly int CharacterId;

        public CharacterSelectMessage(int characterId, int clientId)
        {
            CharacterId = characterId;
            ClientId = clientId;
        }
    }
}