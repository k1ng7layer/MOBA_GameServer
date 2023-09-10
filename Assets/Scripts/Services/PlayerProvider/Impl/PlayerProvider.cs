using System.Collections.Generic;

namespace Services.PlayerProvider.Impl
{
    public class PlayerProvider : IPlayerProvider
    {
        private readonly Dictionary<int, Player> _players = new();

        public IReadOnlyDictionary<int, Player> Players => _players;
        
        public bool TryGet(int id, out Player player)
        {
            return _players.TryGetValue(id, out player);
        }

        public void Remove(Player player)
        {
            if (_players.ContainsKey(player.Id))
                _players.Remove(player.Id);
        }
    }
}