using Core.Systems.Impls;
using Services.CharacterPick.Impl;
using Services.CharacterSpawn.Impl;
using Services.GameField;
using Services.GameField.Impl;
using Services.GameState.Impl;
using Services.GameTimer.Impl;
using Services.PlayerProvider.Impl;
using Services.Team.Impl;
using Services.TimeProvider.Impl;
using Systems;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameField gameField;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UnityTimeProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameTimerProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<RandomTeamProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<PickProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<TeamSpawnService>().AsSingle();
            Container.BindInterfacesAndSelfTo<Bootstrap>().AsSingle();

            var gameFieldProvider = new GameFieldProvider(gameField);
            Container.Bind<IGameFieldProvider>().To<GameFieldProvider>().FromInstance(gameFieldProvider);
        }
    }
}