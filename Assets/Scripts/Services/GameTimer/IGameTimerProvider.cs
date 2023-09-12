namespace Services.GameTimer
{
    public interface IGameTimerProvider
    {
        GameTimer CreateTimer(string name, float targetMilliseconds);
        bool TryGet(string name, out GameTimer timer);
    }
}