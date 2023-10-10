using PBUnityMultiplayer.Runtime.Core.Server;
using PBUnityMultiplayer.Runtime.Core.Server.Impl;
using Services.GameState.Impl;
using Services.PlayerProvider.Impl;
using Systems;
using UnityEngine;
using Zenject;

namespace Installers.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private NetworkServerManager serverManager;
        
        public override void InstallBindings()
        {
            var server = Container.InstantiatePrefabForComponent<NetworkServerManager>(serverManager);
            Container.Bind<INetworkServerManager>().To<NetworkServerManager>().FromInstance(server)
                .AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<GameStateProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerProvider>().AsSingle();
        }
    }
}