using System;

namespace Messages
{
    [Serializable]
    public readonly struct ClientLoadingCompleteMessage
    {
        public readonly int ClientId;

        public ClientLoadingCompleteMessage(int clientId)
        {
            ClientId = clientId;
        }
    }
}