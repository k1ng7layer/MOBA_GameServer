using Views.Interfaces;

namespace Views.Character
{
    public interface ICharacterView : IAiView
    {
        void Initialize(int prefabId);
    }
}