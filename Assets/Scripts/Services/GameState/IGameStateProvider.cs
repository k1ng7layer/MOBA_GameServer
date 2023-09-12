using System;

namespace Services.GameState
{
    public interface IGameStateProvider
    {
        event Action<EGameState> GameStateChanged;
        EGameState CurrentState { get; }
        void SetState(EGameState gameState);
        void AddGameStateListener(IGameStateListener gameStateListener);
        void RemoveGameStateListener(IGameStateListener gameStateListener);
    }
}