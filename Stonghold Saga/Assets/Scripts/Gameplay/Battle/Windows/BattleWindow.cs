using System.Collections.Generic;
using Windows;
using AYellowpaper.SerializedCollections;
using Gameplay.Settlement.Warriors;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Windows
{
    public class BattleWindow : AbstractWindow
    {
        [Header("Settlement Army Texts Map")]
        [SerializedDictionary] public SerializedDictionary<WarriorType, TextController> settlementArmyTextsMap;
        
        [Header("Enemy Army Texts Map")]
        [SerializedDictionary] public SerializedDictionary<WarriorType, TextController> enemyArmyTextsMap;

        [Header("Health Bars")] 
        [SerializedDictionary] public SerializedDictionary<ArmyType, HealthBarController> healthBarsMap;

        [Header("Powers Texts Map")] 
        [SerializedDictionary] public SerializedDictionary<ArmyType, TextMeshProUGUI> powersTextsMap;
        
        [Header("Defences Texts Map")] 
        [SerializedDictionary] public SerializedDictionary<ArmyType, TextMeshProUGUI> defencesTextsMap;

        private int _fullSettlementHealth;
        private int _fullEnemyHealth;
        public void LoadArmy(ArmyType armyType, Dictionary<WarriorType, int> armyMap)
        {
            if (armyType == ArmyType.Settlement)
            {
                foreach (var type in armyMap.Keys)
                {
                    int amount = armyMap[type];

                    settlementArmyTextsMap[type].ChangeText(amount.ToString());
                }
            }
            else if (armyType == ArmyType.Enemy)
            {
                foreach (var type in armyMap.Keys)
                {
                    int amount = armyMap[type];

                    enemyArmyTextsMap[type].ChangeText(amount.ToString());
                }
            }
        }

        public void UpdateHealthBar(ArmyType armyType, int value)
        {
            if (armyType == ArmyType.Settlement)
            {
                healthBarsMap[armyType].ChangeValue((float) value / _fullSettlementHealth);
            }
            else if(armyType == ArmyType.Enemy)
            {
                healthBarsMap[armyType].ChangeValue((float)value / _fullEnemyHealth);
            }
        }

        public void SetHealthBarStartValue(ArmyType armyType, int value)
        {
            if (armyType == ArmyType.Settlement)
            {
                _fullSettlementHealth = value;
            }
            else if(armyType == ArmyType.Enemy)
            {
                _fullEnemyHealth = value;
            }
        }

        public void SetStats(ArmyType armyType, int power, int defence)
        {
            powersTextsMap[armyType].text = power.ToString();
            defencesTextsMap[armyType].text = defence.ToString();
        }
    }

    public enum ArmyType
    {
        Settlement,
        Enemy,
    }
}