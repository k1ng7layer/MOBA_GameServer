namespace Services.GameField.Impl
{
    public class GameFieldProvider : IGameFieldProvider
    {
        public GameFieldProvider(GameField field)
        {
            Field = field;
        }

        public GameField Field { get; }
    }
}