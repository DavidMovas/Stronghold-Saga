using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Settlement.Warriors
{
    public class WarriorView : MonoBehaviour
    {
        [Header("Game Manager")]
        [SerializeField] private GameplayManager _gameplayManager;

        [Header("Panel States")] 
        [SerializeField] private VerticalLayoutGroup _layoutGroup;
        [SerializeField] private GameObject _activeState;
        [SerializeField] private GameObject _lockState;
        
        [Header("Warrior Text Fields")]
        [SerializeField] private TextMeshProUGUI _warriorNameText;
        [SerializeField] private TextMeshProUGUI _warriorSpawnScaleText;
        [SerializeField] private TextMeshProUGUI _warriorPowerText;
        [SerializeField] private TextMeshProUGUI _warriorArmorText;
        [SerializedDictionary] public SerializedDictionary<ResourcesType, TextMeshProUGUI> resourcesSpawnTextsMap;
        
        [Header("Warrior spawn progress bar image")]
        [SerializeField] private Image _warriorSpawnProgressBar;

        public Dictionary<ResourcesType, int> SpawnResourcesMap;

        public int CurrentDay
        {
            get
            { return _currentDay; }
            set
            {
                _currentDay = value;
                _warriorSpawnProgressBar.fillAmount = (float) _currentDay / _spawnDaysScale;
            }
        }
        public int SpawnDaysScale
        {
            get { return _spawnDaysScale; }
            set
            {
                _spawnDaysScale = value;
                _warriorSpawnScaleText.text = _spawnDaysScale + " D";
            }
        }

        public int Power
        {
            get { return _power; }
            set
            {
                _power = value;
                _warriorPowerText.text = _power.ToString();
            }
        }

        public int Armor
        {
            get { return _armor; }
            set
            {
                _armor = value;
                _warriorArmorText.text = _armor.ToString();
            }
        }

        public WarriorType warriorType;
        public int YearOfSpawn => _yearOfSpawn;
        
        [HideInInspector] public bool isActive;
        [HideInInspector] public bool isProcessing;
        
        private WarriorsHub _warriorsHub;
        
        private int _currentDay;
        private int _spawnDaysScale;
        private int _yearOfSpawn;

        private int _power;
        private int _armor;

        private void Start()
        {
            _gameplayManager.OnSettlementManagerInitialisation += Initiallise;
        }

        private void Initiallise()
        {
            _warriorsHub = _gameplayManager.SettlementManager.WarriorsHub;
            _warriorsHub.ConnectToHub(this);
            
            _gameplayManager.OnSettlementManagerInitialisation -= Initiallise;
        }

        public void LoadData(string warriorName, int spawnDaysScale, int power,
            int armor, int yearOfSpawn, bool isWarriorActive, Dictionary<ResourcesType, int> spawnResourcesMap)
        {
            _warriorNameText.text = warriorName;
            _spawnDaysScale = spawnDaysScale;
            _warriorSpawnScaleText.text = _spawnDaysScale + " D";
            _power = power;
            _armor = armor;
            _warriorPowerText.text = _power.ToString();
            _warriorArmorText.text = _armor.ToString();
            _yearOfSpawn = yearOfSpawn;
            isActive = isWarriorActive;
            SpawnResourcesMap = spawnResourcesMap;

            SetResourcesTextData();
        }

        private void SetResourcesTextData()
        {
            foreach (var resource in resourcesSpawnTextsMap.Keys)
            {
                resourcesSpawnTextsMap[resource].text = SpawnResourcesMap[resource].ToString();
            }
        }

        public void ChangeState()
        {
            _layoutGroup.spacing = 0;
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 250f);
            
            _lockState.SetActive(false);
            _activeState.SetActive(true);   
            
            _layoutGroup.spacing = 20;
            _yearOfSpawn = 0;
            isActive = true;
        }
    }
}