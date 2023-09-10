using UnityEngine;

namespace Settings.TimeSettings.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(SoTimeSettings), fileName = nameof(SoTimeSettings))]
    public class SoTimeSettings : ScriptableObject, 
        ITimeSettings
    {
        [SerializeField] private float characterPickTime;
        [SerializeField] private float characterPickFinalStateTime;

        public float CharacterPickTime => characterPickTime;

        public float CharacterPickFinalStateTime => characterPickFinalStateTime;
    }
}