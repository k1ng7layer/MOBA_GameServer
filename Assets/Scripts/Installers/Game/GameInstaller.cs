using Core.Systems.Impls;
using Factories.Character;
using PBUnityMultiplayer.Runtime.Utils.IdGenerator;
using PBUnityMultiplayer.Runtime.Utils.IdGenerator.Impl;
using Presenters;
using Presenters.Character;
using Presenters.Character.Impl;
using Services.CharacterPick.Impl;
using Services.CharacterSpawn.Impl;
using Services.GameField;
using Services.GameField.Impl;
using Services.GameState.Impl;
using Services.GameTimer.Impl;
using Services.Ownership.Impl;
using Services.PlayerProvider.Impl;
using Services.PresenterRepository.Impl;
using Services.Team.Impl;
using Services.TimeProvider.Impl;
using Systems;
using UnityEngine;
using Views.Character.Impl;
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
            Container.BindInterfacesAndSelfTo<RandomTeamProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<NetworkOwnershipRepository>().AsSingle();
            Container.BindInterfacesAndSelfTo<PickProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterPresenterRepository>().AsSingle();
            Container.Bind<IIdGenerator<ushort>>().To<NetworkObjectIdGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<Bootstrap>().AsSingle();

            var gameFieldProvider = new GameFieldProvider(gameField);
            Container.Bind<IGameFieldProvider>().To<GameFieldProvider>().FromInstance(gameFieldProvider);

            Container.BindFactory<CharacterView, Models.Character, CharacterPresenter, CharacterPresenterFactory>().AsSingle();
            Container.BindFactory<Models.Character, CharacterFactory>().AsSingle();
        }
    }
}