using System.Collections.Generic;
using Core.Systems;
using Factories.Character;
using Models;
using Presenters;
using Services.CharacterSpawn;
using UniRx;
using Views.Character.Impl;

namespace Systems
{
    public class CharacterProcessSystem : IInitializeSystem
    {
        private readonly CharacterFactory _characterFactory;
        private readonly CharacterPresenterFactory _characterPresenterFactory;
        private readonly ITeamSpawnService _teamSpawnService;
        private readonly List<CharacterPresenter> _characterPresenters = new();

        public CharacterProcessSystem(
            CharacterFactory characterFactory,
            CharacterPresenterFactory characterPresenterFactory,
            ITeamSpawnService teamSpawnService)
        {
            _characterFactory = characterFactory;
            _characterPresenterFactory = characterPresenterFactory;
            _teamSpawnService = teamSpawnService;
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
                
                _characterPresenters.Add(characterPresenter);
            }
        }
    }
}