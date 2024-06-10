using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Settlement.MilitaryBuilding
{
    public class MilitaryBuilding : MonoBehaviour
    {
        [Header("States")] 
        [SerializeField] private GameObject _activeState;
        [SerializeField] private GameObject _lockState;

        [Header("Military Building Type")] 
        [SerializeField] public MilitaryBuildingType militaryBuildingType;

        [Header("Building name text")]
        [SerializeField] private TextMeshProUGUI _buildingNameText;

        [Header("Upgrade button")] 
        [SerializeField] private Button _upgradeButton;

        [Header("Build Resources Texts Map")] 
        [SerializedDictionary] public SerializedDictionary<ResourcesType, TextMeshProUGUI> _buildResourcesTexts;

        [Header("Upgrade Resources Texts Map")] 
        [SerializedDictionary] public SerializedDictionary<ResourcesType, TextMeshProUGUI> _upgradeResourcesTexts;
        
        [Header("Current Resources Texts Map")] 
        [SerializedDictionary] public SerializedDictionary<ResourcesType, TextMeshProUGUI> _currentResourcesTexts;
        
        [Header("Bonus Resources Texts Map")] 
        [SerializedDictionary] public SerializedDictionary<ResourcesType, TextMeshProUGUI> _bonusResourcesTexts;
        
        private MilitaryBuildingManager _buildingManager;

        private int _currentLevel;
        private int _levelsAmount;

        private Dictionary<ResourcesType, int> _buildResourcesMap;
        private Dictionary<ResourcesType, int> _upgradeResourcesMap;
        private Dictionary<ResourcesType, int> _buildingResourcesMap;
        private Dictionary<ResourcesType, int> _buildingBonusResourcesMap;

        public void LoadData(
            MilitaryBuildingManager buildingManager,
            int startLevel, int levelsAmount, 
            Dictionary<ResourcesType, int> buildResourcesMap,
            Dictionary<ResourcesType, int> upgradeResourcesMap,
            Dictionary<ResourcesType, int> buildingResourcesMap,
            Dictionary<ResourcesType, int> buildingBonusResourcesMap
            )
        {
            _buildingManager = buildingManager;
            _currentLevel = startLevel;
            _levelsAmount = levelsAmount;
            _buildResourcesMap = buildResourcesMap;
            _upgradeResourcesMap = upgradeResourcesMap;
            _buildingResourcesMap = buildingResourcesMap;
            _buildingBonusResourcesMap = buildingBonusResourcesMap;

            _buildingNameText.text = militaryBuildingType + " Lvl " + _currentLevel;
            
            LoadDictionaryValuesToDictionaryTexts(_buildResourcesMap, _buildResourcesTexts);
        }
        
        public void OnUpgradeButton()
        {
            if (_buildingManager.UpgradeBuilding(_upgradeResourcesMap))
            {
                _currentLevel += 1;

                Dictionary<ResourcesType, int> tempMap = new();
                
                foreach (var key in _buildingBonusResourcesMap.Keys)
                {
                    tempMap.Add(key,_buildingResourcesMap[key] + _buildingBonusResourcesMap[key]);
                }

                _buildingResourcesMap = CopyDictionary(tempMap);
                
                tempMap.Clear();
                
                foreach (var key in _upgradeResourcesMap.Keys)
                {
                    tempMap.Add(key, _upgradeResourcesMap[key] * 2);
                }

                _upgradeResourcesMap = CopyDictionary(tempMap);
                
                _buildingNameText.text = militaryBuildingType + " Lvl " + _currentLevel;
                
                LoadDictionaryValuesToDictionaryTexts(_buildingResourcesMap, _currentResourcesTexts);
                LoadDictionaryValuesToDictionaryTexts(_upgradeResourcesMap, _upgradeResourcesTexts);
                
                _buildingManager.ApplyBuildingBonuses(_buildingBonusResourcesMap);
            }

            if (_currentLevel == _levelsAmount)
            {
                _upgradeButton.interactable = false;
            }
        }
        
        public void OnBuildButton()
        {
            if (_buildingManager.UpgradeBuilding(_buildResourcesMap))
            {
                ChangeState();
                
                LoadDictionaryValuesToDictionaryTexts(_upgradeResourcesMap, _upgradeResourcesTexts);
                LoadDictionaryValuesToDictionaryTexts(_buildingResourcesMap, _currentResourcesTexts);
                LoadDictionaryValuesToDictionaryTexts(_buildingBonusResourcesMap, _bonusResourcesTexts);
                
                _buildingManager.ApplyBuildingBonuses(_buildingResourcesMap);
            }
        }

        private void LoadDictionaryValuesToDictionaryTexts(Dictionary<ResourcesType, int> values,
            Dictionary<ResourcesType, TextMeshProUGUI> texts)
        {
            foreach (var text in texts.Keys)
            {
                decimal amount = values[text];
                
                if (amount < 10_000)
                {
                    texts[text].text = $" {amount}";
                }
                else if (amount > 10_000 && amount < 1_000_000)
                {
                    texts[text].text = $" {amount / 1_000:N1} K";
                }
                else
                {
                    texts[text].text = $" {amount / 1_000_000:N1} M";
                }
            }
        }

        private Dictionary<ResourcesType, int> CopyDictionary(Dictionary<ResourcesType, int> originalMap)
        {
            Dictionary<ResourcesType, int> newMap = new();

            foreach (var key in originalMap.Keys)
            {
                newMap.Add(key, originalMap[key]);
            }

            return newMap;
        }

        private void ChangeState()
        {
            _lockState.SetActive(false);
            _activeState.SetActive(true);
        }
    }
}