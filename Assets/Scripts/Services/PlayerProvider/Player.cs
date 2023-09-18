using Models;
using Views.Character;
using Views.Character.Impl;

namespace Services.PlayerProvider
{
    public class Player
    {
        public bool IsLoaded;

        public Player(int id, ETeamType teamType)
        {
            Id = id;
            TeamType = teamType;
        }
        
        public int Id { get; }
        public ETeamType TeamType { get; }
        public Character Character { get; private set; }

        public void SetCharacter(Character character)
        {
            Character = character;
        }
    }
}