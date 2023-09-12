namespace Services.GameState
{
    public interface IGameStateListener
    {
        EGameState GameState { get; }
        void OnGameStateChanged();
    }
}