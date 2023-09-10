using System;
using System.Collections.Generic;
using Extensions;
using Services.PlayerProvider;

namespace Services.Team.Impl
{
    public class RandomTeamProvider : ITeamProvider
    {
        private readonly Queue<ETeamType> _teamTypeQueue = new();

        public RandomTeamProvider()
        {
            FillQueue();
        }
        
        public ETeamType GetTeamType()
        {
            return _teamTypeQueue.Dequeue();
        }

        private void FillQueue()
        {
            var teamList = new List<ETeamType>();

            for (int i = 0; i < 5; i++)
            {
                teamList.Add(ETeamType.Blue);
            }

            for (int i = 0; i < 5; i++)
            {
                teamList.Add(ETeamType.Red);
            }
            
            teamList.Shuffle();

            foreach (var teamType in teamList)
            {
                _teamTypeQueue.Enqueue(teamType);
            }
        }
    }
}