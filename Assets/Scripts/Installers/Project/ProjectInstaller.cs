using PBUnityMultiplayer.Runtime.Core.Server;
using PBUnityMultiplayer.Runtime.Core.Server.Impl;
using UnityEngine;
using Zenject;

namespace Installers.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private NetworkServerManager serverManager;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<NetworkServerManager>().FromNewComponentOnNewPrefab(serverManager)
                .AsSingle();
        }
    }
}