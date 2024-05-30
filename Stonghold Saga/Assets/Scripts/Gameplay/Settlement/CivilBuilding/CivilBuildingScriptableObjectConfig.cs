using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Settlement.CivilBuilding
{
    [CreateAssetMenu(menuName = "CivilBuildingConfigSO")]
    public class CivilBuildingScriptableObjectConfig : ScriptableObject
    {
        public ResourcesType resourcesType;
        public ResourcesType upgradeResource;
        public string buildingName;
        public int levelsAmount;
        public int currentLevel;
        public int currentValue;
        public int bonusValue;
        public int upgradeCost;
    }
}