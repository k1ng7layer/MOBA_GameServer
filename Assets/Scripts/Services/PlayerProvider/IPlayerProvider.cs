using System.Collections.Generic;

namespace Services.PlayerProvider
{
    public interface IPlayerProvider
    {
        IEnumerable<Player> Players { get; }
        IReadOnlyDictionary<int, Player> PlayersTable { get; }
        IReadOnlyList<Player> RedTeam { get; }
        IReadOnlyList<Player> BlueTeam { get; }
        void AddPlayer(Player player);
        bool TryGet(int id, out Player player);
        void Remove(Player player);
    }
}