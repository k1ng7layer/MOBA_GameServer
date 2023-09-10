namespace Messages
{
    public readonly struct CharacterPickMessage
    {
        public readonly int ClientId;

        public CharacterPickMessage(int clientId)
        {
            ClientId = clientId;
        }
    }
}