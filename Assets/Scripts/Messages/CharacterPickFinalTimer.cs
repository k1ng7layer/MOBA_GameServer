using System;

namespace Messages
{
    [Serializable]
    public readonly struct CharacterPickFinalTimer
    {
        public readonly float Value;

        public CharacterPickFinalTimer(float value)
        {
            Value = value;
        }
    }
}