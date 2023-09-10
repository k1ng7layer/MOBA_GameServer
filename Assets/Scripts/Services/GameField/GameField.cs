using Settings.LevelSettings;
using UnityEngine;

namespace Services.GameField
{
    public class GameField : MonoBehaviour
    {
        [Header("Red team settings")] private TeamLevelSettings redTeamLevelSettings;
        [Header("Blue team settings")] private TeamLevelSettings blueTeamLevelSettings;

        public TeamLevelSettings RedTeamLevelSettings => redTeamLevelSettings;
        public TeamLevelSettings BlueTeamLevelSettings => blueTeamLevelSettings;
    }
}