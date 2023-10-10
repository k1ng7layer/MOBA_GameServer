using PBUnityMultiplayer.Runtime.Configuration.Connection;
using PBUnityMultiplayer.Runtime.Configuration.Connection.Impl;
using PBUnityMultiplayer.Runtime.Configuration.Prefabs;
using PBUnityMultiplayer.Runtime.Configuration.Prefabs.Impl;
using PBUnityMultiplayer.Runtime.Configuration.Server;
using PBUnityMultiplayer.Runtime.Configuration.Server.Impl;
using Settings.TimeSettings;
using Settings.TimeSettings.Impl;
using UnityEngine;
using Zenject;

namespace Installers.Game
{
    [CreateAssetMenu(menuName = "Installers/" + nameof(GameServerSettingsInstaller), fileName = nameof(GameServerSettingsInstaller))]
    public class GameServerSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private SoTimeSettings timeSettings;
        [SerializeField] private ScriptableNetworkConfiguration networkConfiguration;
        [SerializeField] private NetworkPrefabsBase networkPrefabsBase;
        [SerializeField] private DefaultServerConfiguration serverConfiguration;
        
        public override void InstallBindings()
        {
            Container.Bind<ITimeSettings>().FromInstance(timeSettings).AsSingle();
            Container.Bind<INetworkConfiguration>().FromInstance(networkConfiguration).AsSingle();
            Container.Bind<INetworkPrefabsBase>().FromInstance(networkPrefabsBase).AsSingle();
            Container.Bind<IServerConfiguration>().FromInstance(serverConfiguration).AsSingle();
        }
    }
}