using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Gameplay.Settlement.Warriors
{
    [CreateAssetMenu(menuName = "Warriors stats configs")]
    public class WarriorsStatsConfigs : ScriptableObject
    {
        public SerializedDictionary<WarriorType, int> warriorsStarsConfigsMap;
    }
}