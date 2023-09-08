using Core.Systems;
using Messages;
using PBUnityMultiplayer.Runtime.Core.Server;
using Services.CharacterPick;
using Services.GameState;
using Systems.Abstract;

namespace Systems
{
    public class CharacterPickSystem : AGameStateSystem, 
        IUpdateSystem
    {
        private readonly ICharacterPickTimerProvider _characterPickTimerProvider;
        private readonly INetworkServerManager _serverManager;

        public CharacterPickSystem(
            IGameStateProvider gameStateProvider, 
            ICharacterPickTimerProvider characterPickTimerProvider,
            INetworkServerManager serverManager
        ) : base(gameStateProvider)
        {
            _characterPickTimerProvider = characterPickTimerProvider;
            _serverManager = serverManager;
        }

        protected override EGameState GameState => EGameState.CharacterPick;
        
        protected override void OnGameStateChanged()
        {
            _characterPickTimerProvider.StartTimer();
        }

        public void Update()
        {
            var timer = _characterPickTimerProvider.Value;
            
            _serverManager.SendMessage(new CharacterPickTimerMessage(timer));
        }
    }
}