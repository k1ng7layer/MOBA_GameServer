using System;
using System.Collections.Generic;
using Extensions;
using PBUnityMultiplayer.Runtime.Configuration.Server;
using Services.PlayerProvider;

namespace Services.Team.Impl
{
    public class RandomTeamProvider : ITeamProvider
    {
        private readonly IServerConfiguration _serverConfiguration;
        private readonly Queue<ETeam> _teamTypeQueue = new();

        public RandomTeamProvider(IServerConfiguration serverConfiguration)
        {
            _serverConfiguration = serverConfiguration;
            FillQueue();
        }
        
        public ETeam GetTeamType()
        {
            return _teamTypeQueue.Dequeue();
        }

        private void FillQueue()
        {
            var teamList = new List<ETeam>();

            for (int i = 0; i < _serverConfiguration.MaxClients; i++)
            {
                teamList.Add(ETeam.Blue);
            }

            for (int i = 0; i < _serverConfiguration.MaxClients; i++)
            {
                teamList.Add(ETeam.Red);
            }
            
            teamList.Shuffle();

            foreach (var teamType in teamList)
            {
                _teamTypeQueue.Enqueue(teamType);
            }
        }
    }
}