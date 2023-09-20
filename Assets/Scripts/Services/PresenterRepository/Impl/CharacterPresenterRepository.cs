using System.Collections.Generic;
using Presenters.Character;

namespace Services.PresenterRepository.Impl
{
    public class CharacterPresenterRepository : ICharacterPresenterRepository
    {
        private readonly Dictionary<int, ICharacterPresenter> _characterPresenters = new();
        public void Add(int characterId, ICharacterPresenter presenter)
        {
            _characterPresenters.Add(characterId, presenter);
        }

        public bool TryGetPresenter(int characterId, out ICharacterPresenter presenter)
        {
            return _characterPresenters.TryGetValue(characterId, out presenter);
        }
    }
}