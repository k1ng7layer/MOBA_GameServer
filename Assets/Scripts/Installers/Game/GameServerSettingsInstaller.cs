using PBUnityMultiplayer.Runtime.Configuration.Connection;
using PBUnityMultiplayer.Runtime.Configuration.Connection.Impl;
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
        
        public override void InstallBindings()
        {
            Container.Bind<ITimeSettings>().FromInstance(timeSettings).AsSingle();
            Container.Bind<INetworkConfiguration>().FromInstance(networkConfiguration).AsSingle();
        }
    }
}