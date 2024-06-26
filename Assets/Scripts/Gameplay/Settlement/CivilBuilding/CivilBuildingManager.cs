using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Settlement.CivilBuilding
{
    public class CivilBuildingManager : MonoBehaviour
    {
        [Header("Game Manager")]
        [SerializeField] private GameplayManager _gameplayManager;
        
        [Header("Time Manager")]
        [SerializeField] private TimeManager _timeManager;
        
        [Header("Civil Buildings List")] 
        [SerializeField] private List<CivilBuilding> _civilBuildings;

        [Header("Civil Building SO Configs List")] 
        [SerializeField] private List<CivilBuildingScriptableObjectConfig> _civilBuildingConfigs;

        private SettlementStorage _settlementStorage;
        private WorkersHub _workersHub;
        
        private int _workersAmount;

        private void Start()
        {
            _timeManager.OnMonthChanged += GetResourcesFormBuildings;
            _gameplayManager.OnSettlementManagerInitialisation += Initiallise;
            
            LoadDataToCivilBuildings();
        }

        private void Initiallise()
        {
            _workersHub = _gameplayManager.SettlementManager.WorkersHub;
            _settlementStorage = _gameplayManager.SettlementManager.SettlementStorage;
            _workersAmount = _workersHub.WorkersAmount / _civilBuildings.Count;
            
            _workersHub.OnWorkersAmountChange += GetNewWorkersAmount;
            _gameplayManager.OnSettlementManagerInitialisation -= Initiallise;
        }

        private void OnDisable()
        {
            _timeManager.OnMonthChanged -= GetResourcesFormBuildings;
            _workersHub.OnWorkersAmountChange -= GetNewWorkersAmount;
        }

        public bool Upgrade(ResourcesType resourcesType, int amount)
        {
            if (_settlementStorage.CheckResource(resourcesType, amount))
            {
                _settlementStorage.GetResource(resourcesType, amount);
                return true;
            }

            return false;
        }

        private void GetResourcesFormBuildings()
        {
            foreach (var building in _civilBuildings)
            {
                _settlementStorage.AddResource(building.resourcesType, building.Work() * _workersAmount);
            }
        }

        private void GetNewWorkersAmount(int amount, int maxAmount)
        {
            _workersAmount = amount / _civilBuildings.Count;
        }

        private void LoadDataToCivilBuildings()
        {
            if (_civilBuildings.Count == _civilBuildingConfigs.Count)
            {
                for (int i = 0; i < _civilBuildings.Count; i++)
                {
                    _civilBuildings[i].LoadData(
                        this,
                        _civilBuildingConfigs[i].resourcesType,
                        _civilBuildingConfigs[i].upgradeResource,
                        _civilBuildingConfigs[i].buildingName,
                        _civilBuildingConfigs[i].levelsAmount,
                        _civilBuildingConfigs[i].currentLevel,
                        _civilBuildingConfigs[i].currentValue,
                        _civilBuildingConfigs[i].bonusValue,
                        _civilBuildingConfigs[i].upgradeCost
                        );
                }
            }
        }
    }
}