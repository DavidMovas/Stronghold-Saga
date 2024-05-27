using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Settlement.CivilBuilding
{
    public class CivilBuilding : MonoBehaviour
    {
        [Header("Title Text")] 
        [SerializeField] private TextMeshProUGUI _titleText;

        [Header("Building Stats")] 
        [SerializeField] private TextMeshProUGUI _currentValueText;
        [SerializeField] private TextMeshProUGUI _bunusValueText;
        [SerializeField] private TextMeshProUGUI _costValueToUpgradeText;

        [Header("Upgrade Button")] 
        [SerializeField] private Button _upgradeButton;

        [HideInInspector] public ResourcesType resourcesType; 
        
        private string _buildingName;
        private int _levelsAmount;
        private int _currentLevel;
        private int _currentValue;
        private int _bonusValue;
        private int _upgradeCost;

        public void LoadData(ResourcesType type, string buildingName, int levelsAmount, int currentLevel, int currentValue, int bonusValue, int upgradeCost)
        {
            resourcesType = type;
            _buildingName = buildingName;
            _currentLevel = currentLevel;
            _currentValue = currentValue;
            _bonusValue = bonusValue;
            _upgradeCost = upgradeCost;
            _levelsAmount = levelsAmount;

            DisplayLoadedData();
        }

        public int Work()
        {
            return _currentValue;
        }

        public void UpgradeBuilding()
        {
            
        }

        private void DisplayLoadedData()
        {
            _titleText.text = $"{_buildingName} Lvl {_currentLevel}";
            _currentValueText.text = $"{_currentValue}";
            _bunusValueText.text = $" + {_bonusValue}";
            _costValueToUpgradeText.text = $"{_upgradeCost}";
        }
    }
}