using System;
using Core.Systems;
using Services.GameState;

namespace Systems.Abstract
{
    public abstract class AGameStateSystem : IInitializeSystem, 
        IGameStateListener,
        IDisposable
    {
        private readonly IGameStateProvider _gameStateProvider;

        protected AGameStateSystem(IGameStateProvider gameStateProvider)
        {
            _gameStateProvider = gameStateProvider;
        }
        
        public abstract EGameState GameState { get; }
        
        void IInitializeSystem.Initialize()
        {
            _gameStateProvider.AddGameStateListener(this);

            OnInitialize();
        }

        void IGameStateListener.OnGameStateChanged()
        {
            OnStateChanged();
        }

        protected abstract void OnStateChanged();
        
        protected virtual void OnInitialize()
        {}

        void IDisposable.Dispose()
        {
            _gameStateProvider.RemoveGameStateListener(this);
            OnDisposing();
        }

        protected virtual void OnDisposing()
        {}
    }
}