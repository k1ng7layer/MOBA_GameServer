using System.Collections.Generic;
using Models;

namespace Services.CharacterPick.Impl
{
    public class PickProvider : IPickProvider
    {
        private readonly Dictionary<int, CharacterDto> _pickTable = new();
        
        public IReadOnlyDictionary<int, CharacterDto> PickTable => _pickTable;
        
        public void AddCharacterPick(CharacterDto characterDto, int playerId)
        {
            _pickTable.Add(playerId, characterDto);
        }
    }
}