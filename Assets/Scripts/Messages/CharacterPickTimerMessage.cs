namespace Messages
{
    public readonly struct CharacterPickTimerMessage
    {
        public readonly float Value;

        public CharacterPickTimerMessage(float value)
        {
            Value = value;
        }
    }
}