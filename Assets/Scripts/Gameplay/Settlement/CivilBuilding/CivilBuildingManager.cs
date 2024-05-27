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
            LoadDataToCivilBuildings();

            _settlementStorage = _gameplayManager.SettlementManager.SettlementStorage;
            _workersHub = _gameplayManager.SettlementManager.WorkersHub;

            _workersAmount = _workersHub.WorkersAmount / _civilBuildings.Count;
            
            _timeManager.OnMonthChanged += GetResourcesFormBuildings;
            _workersHub.OnWorkersAmountChange += GetNewWorkersAmount;
        }

        private void OnDisable()
        {
            _timeManager.OnMonthChanged -= GetResourcesFormBuildings;
            _workersHub.OnWorkersAmountChange -= GetNewWorkersAmount;
        }

        private void GetResourcesFormBuildings()
        {
            foreach (var building in _civilBuildings)
            {
                _settlementStorage.AddResource(building.resourcesType, building.Work() * _workersAmount);
            }
        }

        private void GetNewWorkersAmount(int amount)
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
                        _civilBuildingConfigs[i].resourcesType,
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