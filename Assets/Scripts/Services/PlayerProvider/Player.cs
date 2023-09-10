namespace Services.PlayerProvider
{
    public class Player
    {
        public bool IsLoaded;
        public bool CharacterLocked;
        
        public Player(int id)
        {
            Id = id;
        }
        
        public int Id { get; }
        public int CharacterId { get; set; }
    }
}