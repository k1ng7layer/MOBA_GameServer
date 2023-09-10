using System.Collections.Generic;

namespace Services.PlayerProvider
{
    public interface IPlayerProvider
    {
        IReadOnlyDictionary<int, Player> Players { get; }
        bool TryGet(int id, out Player player);
        void Remove(Player player);
    }
}