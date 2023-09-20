using System.Collections.Generic;
using Core.Systems;
using Factories.Character;
using Models;
using Presenters;
using Presenters.Character;
using Presenters.Character.Impl;
using Services.CharacterSpawn;
using Services.PresenterRepository;
using UniRx;
using Views.Character.Impl;

namespace Systems
{
    public class CharacterProcessSystem : IInitializeSystem
    {
        private readonly CharacterFactory _characterFactory;
        private readonly CharacterPresenterFactory _characterPresenterFactory;
        private readonly ITeamSpawnService _teamSpawnService;
        private readonly ICharacterPresenterRepository _characterPresenterRepository;
        private readonly List<CharacterPresenter> _characterPresenters = new();

        public CharacterProcessSystem(
            CharacterFactory characterFactory,
            CharacterPresenterFactory characterPresenterFactory,
            ITeamSpawnService teamSpawnService,
            ICharacterPresenterRepository characterPresenterRepository)
        {
            _characterFactory = characterFactory;
            _characterPresenterFactory = characterPresenterFactory;
            _teamSpawnService = teamSpawnService;
            _characterPresenterRepository = characterPresenterRepository;
        }
        
        public void Initialize()
        {
            _teamSpawnService.TeamSpawned.Subscribe(OnTeamSpawned);
        }

        private void OnTeamSpawned(List<CharacterView> characterViews)
        {
            foreach (var characterView in characterViews)
            {
                var character = _characterFactory.Create();
                var characterPresenter = _characterPresenterFactory.Create(characterView, character);
                var networkId = characterView.NetworkObjectId;
                _characterPresenters.Add(characterPresenter);
                _characterPresenterRepository.Add(networkId, characterPresenter);
            }
        }
    }
}