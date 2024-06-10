using System;
using System.Collections.Generic;

namespace Gameplay.Settlement.Warriors
{
    public class WarriorsHub
    { 
        public event Action<WarriorType, int> OnWarriorsAmountUpdate;

        private GameplayManager _gameplayManager;
        private SettlementStorage _settlementStorage;
        private WarriorsManager _warriorsManager;
        private TimeManager _timeManager;
        
        private Dictionary<WarriorType, int> _warriorsMap;
        private Dictionary<ResourcesType, int> _bonusesMap;

        private List<WarriorView> _warriorViews;

        public WarriorsHub(GameplayManager gameplayManager, TimeManager timeManager)
        {
            _gameplayManager = gameplayManager;
            _timeManager = timeManager;
            
            _warriorViews = new();
            _warriorsMap = new();
            _bonusesMap = new();

            _gameplayManager.OnSettlementManagerInitialisation += Initiallise;

            _timeManager.OnDayChanged += CreateWarriors;
            _timeManager.OnYearChanged += ToActivateWarriorView;
        }

        private void Initiallise()
        {
            _settlementStorage = _gameplayManager.SettlementManager.SettlementStorage;
            _warriorsManager = _gameplayManager.SettlementManager.WarriorsManager;
            
            _gameplayManager.OnSettlementManagerInitialisation -= Initiallise;
        }
        
        ~WarriorsHub(){
            _timeManager.OnDayChanged -= CreateWarriors;
            _timeManager.OnYearChanged -= ToActivateWarriorView;
        }

        public void ConnectToHub(WarriorView warriorView)
        {
            WarriorScriptableObjectConfig config = _gameplayManager.warriorsConfigsMap[warriorView.warriorType];
            
            warriorView.LoadData
                (
                    config.warriorName,
                    config.spawnDayScale,
                    config.power,
                    config.armor,
                    config.yearOfSpawn,
                    config.isActive,
                    config.spawnCost
                    );

            ApplyBonusesForNewWarrior(warriorView);
            
            _warriorViews.Add(warriorView);
        }

        public void SetWarriors(Dictionary<WarriorType, int> armyMap)
        {
            if (armyMap != null)
            {
                _warriorsMap = new(armyMap);
            }
        }
        
        public Dictionary<WarriorType, int> GetWarriors()
        {
            return _warriorsMap;
        }

        public void ApplyBonus(ResourcesType resourcesType, int amount)
        {
            foreach (var warrior in _warriorViews)
            {
                if (resourcesType == ResourcesType.Power)
                {
                    warrior.Power += amount;
                }
                else if (resourcesType == ResourcesType.Armor)
                {
                    warrior.Armor += amount;
                }
                else if (resourcesType == ResourcesType.Time)
                {
                    warrior.SpawnDaysScale -= amount;
                }
            }
            
            _warriorsManager.ApplyBonus(resourcesType, amount);

            if (!_bonusesMap.TryAdd(resourcesType, amount))
            {
                _bonusesMap[resourcesType] += amount;
            }
        }

        private void ApplyBonusesForNewWarrior(WarriorView warrior)
        {
            if (_bonusesMap.Count > 0)
            {
                foreach (var key in _bonusesMap.Keys)
                {
                    if (key == ResourcesType.Power)
                    {
                        warrior.Power += _bonusesMap[key];
                    }
                    else if (key == ResourcesType.Armor)
                    {
                        warrior.Armor += _bonusesMap[key];
                    }
                    else if (key == ResourcesType.Time)
                    {
                        warrior.SpawnDaysScale -= _bonusesMap[key];
                    }
                }
            }
        }

        private void AddWarrior(WarriorType type)
        {
            if (!_warriorsMap.TryAdd(type, 1))
            {
                _warriorsMap[type] += 1;
            }
            
            OnWarriorsAmountUpdate?.Invoke(type, _warriorsMap[type]);
        }
        
        private void CreateWarriors()
        {
            foreach (var warrior in _warriorViews)
            {
                if (warrior.isActive && warrior.isProcessing)
                {
                    warrior.CurrentDay += 1;
    
                    if (warrior.CurrentDay >= warrior.SpawnDaysScale)
                    {
                        AddWarrior(warrior.warriorType);
                        
                        _settlementStorage.AddResource(ResourcesType.Power, warrior.Power);
                        _settlementStorage.AddResource(ResourcesType.Armor, warrior.Armor);
                        
                        warrior.CurrentDay = 0;
                        warrior.isProcessing = false;
                    }
                }
                else if (warrior.isActive && !warrior.isProcessing)
                {
                    bool isPaied = true;
                    
                    foreach (var resource in warrior.SpawnResourcesMap.Keys)
                    {
                        if (!_settlementStorage.CheckResource(resource, warrior.SpawnResourcesMap[resource]))
                        {
                            if (isPaied) isPaied = false;
                        }
                    }

                    if (isPaied)
                    {
                        foreach (var res in warrior.SpawnResourcesMap.Keys)
                        {
                            _settlementStorage.GetResource(res, warrior.SpawnResourcesMap[res]);
                        }

                        warrior.isProcessing = true;
                    }
                }
            }
        }

        private void ToActivateWarriorView()
        {
            if (_warriorViews != null && _warriorViews.Count > 0)
            {
                foreach (var view in _warriorViews)
                {
                    if (view.YearOfSpawn == _timeManager.Year)
                    {
                        view.ChangeState();
                    }
                }
            }
        }
    }

    public enum WarriorType
    {
        Soldier,
        Archer,
        Knight,
        WarHorse,
    }
}
