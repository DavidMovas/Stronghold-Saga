using System.Collections.Generic;
using Windows;
using AYellowpaper.SerializedCollections;
using Gameplay.Settlement.Warriors;
using TMPro;
using UnityEngine;

namespace Gameplay.Windows
{
    public class BattleResultWindow : AbstractWindow
    {
        [Header("Power Lose")]
        [SerializeField] private TextMeshProUGUI startPowerText;
        [SerializeField] private TextMeshProUGUI losePowerText;
        
        [Header("Defence Lose")]
        [SerializeField] private TextMeshProUGUI startDefenceText;
        [SerializeField] private TextMeshProUGUI loseDefenceText;

        [Header("Workers Lose")] 
        [SerializeField] private TextMeshProUGUI workersLoseText;

        [Header("Army Lose Map")] [SerializedDictionary("Type", "Text")]
        public SerializedDictionary<WarriorType, TextMeshProUGUI> warriorsLoseTextMap;

        public void LoadData(int startPower, int losePower, int startDefence, int loseDefence, int workersLose,
            Dictionary<WarriorType, int> armyLoseMap)
        {
            startPowerText.text = startPower.ToString();
            losePowerText.text = losePower.ToString();
            startDefenceText.text = startDefence.ToString();
            loseDefenceText.text = loseDefence.ToString();
            workersLoseText.text = workersLose.ToString();

            foreach (var type in armyLoseMap.Keys)
            {
                warriorsLoseTextMap[type].text = armyLoseMap[type].ToString();
            }
        }
    }
}