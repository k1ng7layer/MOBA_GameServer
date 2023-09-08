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
        
        public override void InstallBindings()
        {
            Container.Bind<ITimeSettings>().FromInstance(timeSettings).AsSingle();
        }
    }
}