using Systems;
using Zenject;

namespace Installers.Game
{
    public class GameSystemsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InitializeServerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterPickTimerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterPickSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterPickFinalCountdownSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<WaitForClientLoadingSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPlayersSystem>().AsSingle();
        }
    }
}