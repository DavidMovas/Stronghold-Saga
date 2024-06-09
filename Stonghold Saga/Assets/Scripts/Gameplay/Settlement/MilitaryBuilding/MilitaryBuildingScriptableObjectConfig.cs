using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Gameplay.Settlement.MilitaryBuilding
{
    [CreateAssetMenu(menuName = "Military Building Configs")]
    public class MilitaryBuildingScriptableObjectConfig : ScriptableObject
    {
        public MilitaryBuildingType militaryBuildingType;

        public int startLevel;
        public int levelsAmount;
        
        [Header("Demand resources for build")]
        public SerializedDictionary<ResourcesType, int> buildResourcesMap;
        
        [Header("Demand resources for upgrade")]
        public SerializedDictionary<ResourcesType, int> upgradeResourcesMap;

        [Header("Resources that building change")]
        public SerializedDictionary<ResourcesType, int> buildingResourcesMap;

        [Header("Resources bonus values")] 
        public SerializedDictionary<ResourcesType, int> buildingBonusResourcesMap;
    }
}