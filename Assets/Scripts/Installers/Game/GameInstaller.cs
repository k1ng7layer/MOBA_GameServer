using Services.GameState.Impl;
using Services.TimeProvider.Impl;
using Systems;
using Zenject;

namespace Installers.Game
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CharacterPickTimerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityTimeProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityTimeProvider>().AsSingle();
        }
    }
}