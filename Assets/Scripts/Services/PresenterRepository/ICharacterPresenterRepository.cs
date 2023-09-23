using System.Collections.Generic;
using Presenters.Character;

namespace Services.PresenterRepository
{
    public interface ICharacterPresenterRepository
    {
        IEnumerable<ICharacterPresenter> CharacterPresenters { get; }
        void Add(int characterId, ICharacterPresenter presenter);
        bool TryGetPresenter(int characterId, out ICharacterPresenter presenter);
    }
}