using Core.Systems;
using Factories.Character;
using Services.CharacterSpawn;

namespace Systems
{
    public class CharacterProcessSystem : IInitializeSystem
    {
        private readonly CharacterFactory _characterFactory;
        private readonly ITeamSpawnService _teamSpawnService;

        public CharacterProcessSystem(
            CharacterFactory characterFactory, 
            ITeamSpawnService teamSpawnService)
        {
            _characterFactory = characterFactory;
            _teamSpawnService = teamSpawnService;
        }


        public void Initialize()
        {
            
            var character = _characterFactory.Create();
        }
    }
}