using System;

namespace Messages
{
    [Serializable]
    public struct ClientStateReadyMessage
    {
        public int clientId;
        public int stateId;
    }
}