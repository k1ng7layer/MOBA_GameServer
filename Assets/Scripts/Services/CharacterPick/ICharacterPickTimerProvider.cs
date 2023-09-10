using System;

namespace Services.CharacterPick
{
    public interface ICharacterPickTimerProvider
    {
        float Value { get; }
        event Action Elapsed;
        void StartTimer();
        void StartFinalCountdown();
    }
}