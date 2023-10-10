using Settings.LevelSettings;
using UnityEngine;

namespace Services.GameField
{
    public class GameField : MonoBehaviour
    {
        [Header("Red team settings")] 
        [SerializeField] private Transform[] redTeamLevelSettings;
        [Header("Blue team settings")] 
        [SerializeField] private Transform[] blueTeamLevelSettings;

        public Transform[] RedTeamLevelSettings => redTeamLevelSettings;
        public Transform[] BlueTeamLevelSettings => blueTeamLevelSettings;
    }
}