using System.Collections.Generic;

namespace Services.PlayerProvider.Impl
{
    public class PlayerProvider : IPlayerProvider
    {
        private readonly Dictionary<int, Player> _playersTable = new();
        private readonly List<Player> _redTeam = new();
        private readonly List<Player> _blueTeam = new();
        private IEnumerable<Player> _players;

        public IEnumerable<Player> Players
        {
            get
            {
                if (_players == null)
                {
                    _players = _playersTable.Values;
                }

                return _players;
            }
        }

        public IReadOnlyDictionary<int, Player> PlayersTable => _playersTable;

        public IReadOnlyList<Player> RedTeam => _redTeam;

        public IReadOnlyList<Player> BlueTeam => _blueTeam;

        public bool TryGet(int id, out Player player)
        {
            return _playersTable.TryGetValue(id, out player);
        }

        public void Remove(Player player)
        {
            if (_playersTable.ContainsKey(player.Id))
                _playersTable.Remove(player.Id);
        }

        public void AddPlayer(Player player)
        {
            _playersTable.Add(player.Id, player);
            
            if (player.Team == ETeam.Blue)
                _blueTeam.Add(player);
            else _redTeam.Add(player);
        }
    }
}