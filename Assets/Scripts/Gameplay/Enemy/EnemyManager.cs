using System;
using System.Collections.Generic;
using Gameplay.Settlement;
using Gameplay.Settlement.Warriors;

namespace Gameplay.Enemy
{
    public class EnemyManager
    {
        private GameplayManager _gameplayManager;
        private WarriorsHub _warriorsHub;
        
        private Random _random;

        private Dictionary<WarriorType, int> _enemyArmy;

        private Dictionary<WarriorType, int> _warriorsPowerMap;

        private int _deferenceAmount;

        public EnemyManager(GameplayManager gameplayManager, int deferenceAmount)
        {
            _gameplayManager = gameplayManager;
            
            _gameplayManager.OnSettlementManagerInitialisation += Initialise;

            _deferenceAmount = deferenceAmount;
        }

        private void Initialise()
        {
            _warriorsHub = _gameplayManager.SettlementManager.WarriorsHub;

            _warriorsPowerMap = _gameplayManager.warriorsStatsConfigs.warriorsStarsConfigsMap;
            
            _gameplayManager.OnSettlementManagerInitialisation -= Initialise;
        }
        
        public void GenerateArmy()
        {
            _random = new();
            _enemyArmy = new();

            Dictionary<WarriorType, int> warriorsMap = _warriorsHub.GetWarriors();

            foreach (var type in warriorsMap.Keys)
            {
                int amount = warriorsMap[type];

                while (amount - _deferenceAmount < 0)
                {
                    _deferenceAmount -= 5;
                }

                _enemyArmy.Add(type, _random.Next(amount - _deferenceAmount, amount + _deferenceAmount));
            }
        }
        
        public int CalculateDefence()
        {
            int defence = _gameplayManager.SettlementManager.SettlementStorage.GetResourceAmount(ResourcesType.Defense);
            
            return _random.Next(0, defence + 1);
        }

        public Dictionary<WarriorType, int> GetAllWarriors()
        {
            return _enemyArmy;
        }
    }
}