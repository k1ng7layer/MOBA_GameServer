using Models;
using Services.GameState;
using Views.Character;
using Views.Character.Impl;

namespace Services.PlayerProvider
{
    public class Player
    {
        public bool IsLoaded;

        public Player(int id, ETeam team)
        {
            Id = id;
            Team = team;
        }
        
        public int Id { get; }
        public ETeam Team { get; }
        public EGameState GameState { get; set; }
        public int CharacterId { get; set; }
        public bool CharacterLocked { get; set; }
    }
}