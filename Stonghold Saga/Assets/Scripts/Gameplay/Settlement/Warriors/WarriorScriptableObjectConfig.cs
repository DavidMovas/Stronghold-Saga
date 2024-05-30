using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Gameplay.Settlement.Warriors
{
    [CreateAssetMenu(menuName = "Warriors Configs")]
    public class WarriorScriptableObjectConfig : ScriptableObject
    {
        public WarriorType warriorType;
        public string warriorName;
        public Sprite sprite;
        public int spawnDayScale;
        public int power;
        public int armor;
        public int yearOfSpawn;
        public bool isActive;

        [SerializedDictionary] public SerializedDictionary<ResourcesType, int> spawnCost;
    }
}