namespace Services.GameTimer
{
    public interface IGameTimerProvider
    {
        GameTimer CreateTimer(string name, float targetMilliseconds);
    }
}