using System;

namespace Services.GameState.Impl
{
    public class GameStateProvider : IGameStateProvider
    {
        public event Action<EGameState> GameStateChanged;
        public EGameState CurrentState { get; private set; }
        
        public void SetState(EGameState gameState)
        {
            CurrentState = gameState;
            
            GameStateChanged?.Invoke(gameState);
        }
    }
}