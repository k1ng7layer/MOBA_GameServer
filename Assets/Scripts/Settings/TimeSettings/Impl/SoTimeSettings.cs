using UnityEngine;

namespace Settings.TimeSettings.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(SoTimeSettings), fileName = nameof(SoTimeSettings))]
    public class SoTimeSettings : ScriptableObject, 
        ITimeSettings
    {
        [SerializeField] private float characterPickTime;

        public float CharacterPickTime => characterPickTime;
    }
}