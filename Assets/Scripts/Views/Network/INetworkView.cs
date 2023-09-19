namespace Views.Network
{
    public interface INetworkView
    {
        int OwnerId { get; }
        void SetOwnerId(int id);
    }
}