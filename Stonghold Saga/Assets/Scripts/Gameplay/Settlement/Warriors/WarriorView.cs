using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Settlement.Warriors
{
    public class WarriorView : MonoBehaviour
    {
        [Header("Panel States")] 
        [SerializeField] private GameObject _activeState;
        [SerializeField] private GameObject _lockState;
        
        [Header("Warrior Text Fields")]
        [SerializeField] private TextMeshProUGUI _warriorNameText;
        [SerializeField] private TextMeshProUGUI _warriorSpawnScaleText;
        [SerializeField] private TextMeshProUGUI _warriorPowerText;
        [SerializeField] private TextMeshProUGUI _warriorArmorText;
        [SerializeField] private TextMeshProUGUI _warriorFirstSpawnResource;
        [SerializeField] private TextMeshProUGUI _warriorSecondSpawnResource;
        
        [Header("Warrior spawn progress bar image")]
        [SerializeField] private Image _warriorSpawnProgressBar;

        [Header("Warrior spawn resource icons")]
        [SerializeField] private Image _firstIcon;
        [SerializeField] private Image _secondIcon;

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
                _warriorSpawnScaleText.text = _spawnDaysScale + "D";
            }
        }

        public WarriorType WarriorType => _warriorType;
        public int YearOfSpawn => _yearOfSpawn;
        [HideInInspector] public bool isActive;
        
        private WarriorsHub _warriorsHub;

        private WarriorType _warriorType;
        
        private int _currentDay;
        private int _spawnDaysScale;
        private int _yearOfSpawn;
        
        public void LoadData(WarriorsHub warriorsHub)
        {
            _warriorsHub = warriorsHub;
            
            _warriorsHub.ConnectToHub(this);
        }

        public void ChangeState()
        {
            _lockState.SetActive(false);
            _activeState.SetActive(true);
            
            RectTransform rectTransform = _activeState.GetComponent<RectTransform>();

            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 250f);
            
            isActive = true;
        }
    }
}