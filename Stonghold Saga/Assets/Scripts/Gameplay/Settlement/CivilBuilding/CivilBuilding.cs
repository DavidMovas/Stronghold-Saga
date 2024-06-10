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
        private ResourcesType _upgradeResource;

        private CivilBuildingManager _manager;
        
        private string _buildingName;
        private int _levelsAmount;
        private int _currentLevel;
        private int _currentValue;
        private int _bonusValue;
        private int _upgradeCost;

        public void LoadData(CivilBuildingManager manager, ResourcesType type,ResourcesType upgradeResource, string buildingName, int levelsAmount, int currentLevel, int currentValue, int bonusValue, int upgradeCost)
        {
            _manager = manager;
            resourcesType = type;
            _upgradeResource = upgradeResource;
            _buildingName = buildingName;
            _currentLevel = currentLevel;
            _currentValue = currentValue;
            _bonusValue = bonusValue;
            _upgradeCost = upgradeCost;
            _levelsAmount = levelsAmount;

            DisplayData();
        }

        public int Work()
        {
            return _currentValue;
        }

        public void UpgradeBuildingButton()
        {
            if (_manager.Upgrade(_upgradeResource, _upgradeCost))
            {
                _currentLevel += 1;
                _currentValue += _bonusValue;
                _upgradeCost *= 2;

                DisplayData();

                if (_currentLevel == _levelsAmount)
                {
                    _upgradeButton.interactable = false;
                    _bunusValueText.text = " - ";
                    _costValueToUpgradeText.text = " - ";
                }
            }
        }

        private void DisplayData()
        {
            _titleText.text = $"{_buildingName} Lvl {_currentLevel}";
            _currentValueText.text = $"{_currentValue}";
            _bunusValueText.text = $"+ {_bonusValue}";
            
            decimal value = _upgradeCost;
                
            if (value < 10_000)
            {
                _costValueToUpgradeText.text = $"{value}";
            }
            else if (value > 10_000 && value < 1_000_000)
            {
                _costValueToUpgradeText.text = $"{value / 1_000:N1} K";
            }
            else
            {
                _costValueToUpgradeText.text = $"{value / 1_000_000:N1} M";
            }
        }
        
    }
}