using System.Collections.Generic;
using Presenters.Character;

namespace Services.PresenterRepository.Impl
{
    public class CharacterPresenterRepository : ICharacterPresenterRepository
    {
        private readonly Dictionary<int, ICharacterPresenter> _characterPresenters = new();
        private readonly List<ICharacterPresenter> _characterPresentersList = new();

        public IEnumerable<ICharacterPresenter> CharacterPresenters => _characterPresentersList;

        public void Add(int characterId, ICharacterPresenter presenter)
        {
            var hasPresenter = _characterPresenters.ContainsKey(characterId);
            
            if(hasPresenter)
                return;
            
            _characterPresenters.Add(characterId, presenter);
            
            _characterPresentersList.Add(presenter);
        }

        public bool TryGetPresenter(int characterId, out ICharacterPresenter presenter)
        {
            return _characterPresenters.TryGetValue(characterId, out presenter);
        }
    }
}