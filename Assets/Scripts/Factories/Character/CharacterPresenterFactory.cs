using Presenters;
using Presenters.Character;
using Presenters.Character.Impl;
using Views.Character.Impl;
using Zenject;

namespace Factories.Character
{
    public class CharacterPresenterFactory : PlaceholderFactory<CharacterView, Models.Character, CharacterPresenter>
    {
        
    }
}