using System.Collections.Generic;
using Models;

namespace Services.CharacterPick
{
    public interface IPickProvider
    {
        IReadOnlyDictionary<int, CharacterDto> PickTable { get; }
        void AddCharacterPick(CharacterDto characterDto, int playerId);
    }
}