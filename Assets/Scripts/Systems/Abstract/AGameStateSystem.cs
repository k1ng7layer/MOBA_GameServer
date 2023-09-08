using System;
using Core.Systems;
using Services.GameState;

namespace Systems.Abstract
{
    public abstract class AGameStateSystem : IInitializeSystem, 
        IDisposable
    {
        private readonly IGameStateProvider _gameStateProvider;

        protected AGameStateSystem(IGameStateProvider gameStateProvider)
        {
            _gameStateProvider = gameStateProvider;
        }
        
        protected abstract EGameState GameState { get; }
        
        public void Initialize()
        {
            _gameStateProvider.GameStateChanged += OnStateChanged;
        }

        private void OnStateChanged(EGameState gameState)
        {
            if(gameState != GameState)
                return;

            OnGameStateChanged();
        }

        protected abstract void OnGameStateChanged();

        void IDisposable.Dispose()
        {
            _gameStateProvider.GameStateChanged -= OnStateChanged;
        }
    }
}