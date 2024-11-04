using System;
using UnityEngine;

namespace NJM.Template {

    [CreateAssetMenu(fileName = "So_GameSetting", menuName = "NJM/GameSetting", order = 0)]
    public class GameSettingSO : ScriptableObject {
        public GameSettingTM tm;
    }
}