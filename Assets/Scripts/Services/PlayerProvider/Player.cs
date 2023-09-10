namespace Services.PlayerProvider
{
    public class Player
    {
        public bool IsLoaded;
        public bool CharacterLocked;
        
        public Player(int id, ETeamType teamType)
        {
            Id = id;
            TeamType = teamType;
        }
        
        public int Id { get; }
        public ETeamType TeamType { get; }
        public int CharacterId { get; set; }
    }
}