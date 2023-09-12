using PBUnityMultiplayer.Runtime.Core.Server;
using PBUnityMultiplayer.Runtime.Core.Server.Impl;
using Services.GameState.Impl;
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
            Container.Bind<INetworkServerManager>().To<NetworkServerManager>().FromNewComponentOnNewPrefab(serverManager)
                .AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<GameStateProvider>().AsSingle();
        }
    }
}