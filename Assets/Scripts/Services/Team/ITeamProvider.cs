using Services.PlayerProvider;

namespace Services.Team
{
    public interface ITeamProvider
    {
        ETeam GetTeamType();
    }
}