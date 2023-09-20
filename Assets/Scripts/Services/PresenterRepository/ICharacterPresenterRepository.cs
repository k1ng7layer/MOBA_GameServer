using Presenters.Character;

namespace Services.PresenterRepository
{
    public interface ICharacterPresenterRepository
    {
        void Add(int characterId, ICharacterPresenter presenter);
        bool TryGetPresenter(int characterId, out ICharacterPresenter presenter);
    }
}