using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Settlement.Warriors
{
    public class WarriorsManager
    {
        private Dictionary<WarriorType, Warrior> _warriorsMap;

        public WarriorsManager(Dictionary<WarriorType, WarriorScriptableObjectConfig> warriorsConfigsMap)
        {
            CreateWarriorsMap(warriorsConfigsMap);
        }
        
        public void ApplyBonus(ResourcesType resourcesType, int amount)
        {
            foreach (var type in _warriorsMap.Keys)
            {
                switch (resourcesType)
                {
                    case ResourcesType.Power:
                        _warriorsMap[type].Power += amount;
                        break;
                    case ResourcesType.Armor:
                        _warriorsMap[type].Armor += amount;
                        break;
                    default:
                        return;
                }
            }
        }

        public Dictionary<WarriorType, Warrior> GetWarriors()
        {
            return _warriorsMap;
        }

        private void CreateWarriorsMap(Dictionary<WarriorType, WarriorScriptableObjectConfig> configs)
        {
            _warriorsMap = new();

            foreach (var type in configs.Keys)
            {
                Warrior warrior = new Warrior(type,configs[type].power, configs[type].armor);
                
                _warriorsMap.Add(type, warrior);
            }
        }
    }
}