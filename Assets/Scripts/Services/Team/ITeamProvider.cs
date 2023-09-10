using Services.PlayerProvider;

namespace Services.Team
{
    public interface ITeamProvider
    {
        ETeamType GetTeamType();
    }
}