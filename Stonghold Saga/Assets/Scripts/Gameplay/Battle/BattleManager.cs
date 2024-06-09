using System.Collections.Generic;
using Gameplay.Enemy;
using Gameplay.Settlement;
using Gameplay.Settlement.Warriors;
using Gameplay.Windows;
using UnityEngine;
using Random = System.Random;

namespace Gameplay.Battle
{
    public class BattleManager
    {
        public EnemyManager EnemyManager => _enemyManager;

        public bool IsAttack;

        private GameplayManager _gameplayManager;
        private TimeManager _timeManager;
        private WarriorsHub _warriorsHub;
        private WarriorsManager _warriorsManager;
        private EnemyManager _enemyManager;

        private BattleManagerView _battleManagerView;
        
        private Random _random;
        
        public int BattleYear => _battleYear;

        private int _battleYear;

        private int _minBattleYearStep;
        private int _maxBattleYearStep;

        private Dictionary<WarriorType, int> _settlementArmy;
        private Dictionary<WarriorType, int> _enemyArmy;

        private Dictionary<WarriorType, int> _settlementArmyLose;
        private Dictionary<WarriorType, int> _enemyArmyLose;

        private Dictionary<WarriorType, Warrior> _warriorsMap;
        
        private int _settlementPower;
        private int _settlementArmor;
        private int _settlementDefence;

        private int _enemyPower;
        private int _enemyArmor;
        private int _enemyDefence;

        private readonly int _attackPowerPercent;

        private int _settlementAttackPower;
        private int _enemyAttackPower;
        
        private int _workersLose;
        
        private bool _isEnemyAttack;

        private int SettlementHealth
        {
            get { return _settlementHealth; }
            set
            {
                _settlementHealth = value;
                
                _battleManagerView.UpdateHealthBar(ArmyType.Settlement, _settlementHealth);
            }
        }
        private int EnemyHealth
        {
            get { return _enemyHealth; }
            set
            {
                _enemyHealth = value;
                
                _battleManagerView.UpdateHealthBar(ArmyType.Enemy, _enemyHealth);
            }
        }

        private int _settlementHealth;
        private int _enemyHealth;

        public BattleManager(GameplayManager gameplayManager, TimeManager timeManager,
            int minBattleYearStep, int maxBattleYearStep, int deferenceUnitsAmount, int attackPowerPercent)
        {
            _gameplayManager = gameplayManager;
            _gameplayManager.OnSettlementManagerInitialisation += Initialise;
            
            _timeManager = timeManager;
            
            _minBattleYearStep = minBattleYearStep;
            _maxBattleYearStep = maxBattleYearStep;
            _attackPowerPercent = attackPowerPercent;
            
            _enemyManager = new EnemyManager(gameplayManager, deferenceUnitsAmount);
        }

        private void Initialise()
        {
            _random = new Random();
            
            _warriorsHub = _gameplayManager.SettlementManager.WarriorsHub;
            _warriorsManager = _gameplayManager.SettlementManager.WarriorsManager;
            
            SetNextBattleYear();
            
            _gameplayManager.OnSettlementManagerInitialisation -= Initialise;
        }

        public void ConnectBattleView(BattleManagerView battleManagerView) => _battleManagerView = battleManagerView;

        private void SetNextBattleYear()
        {
            _battleYear = _random.Next(_timeManager.Year + _minBattleYearStep, _timeManager.Year + _maxBattleYearStep + 1);
            
            Debug.Log("Battle: " + _battleYear);
            
            _timeManager.SubscribeOnTargetYearEvent(_battleYear, LoadBattle);
        }

        private void LoadBattle()
        {
            _battleManagerView.PauseGame();
            
             _enemyManager.GenerateArmy();
             
             _settlementArmy = new Dictionary<WarriorType, int>(_warriorsHub.GetAllWarriors());
             _enemyArmy = new Dictionary<WarriorType, int>(_enemyManager.GetAllWarriors());
             _warriorsMap = new(_gameplayManager.SettlementManager.WarriorsManager.GetWarriors());
             
             _settlementPower = CountArmyPower(_settlementArmy);
             _enemyPower = CountArmyPower(_enemyArmy);
             
             _settlementDefence = _gameplayManager.SettlementManager.SettlementStorage.GetResourceAmount(ResourcesType.Defense);
             _enemyDefence = _enemyManager.CalculateDefence();

             _settlementAttackPower = CalculateAttackPower(_settlementPower);
             _enemyAttackPower = CalculateAttackPower(_enemyPower);
             
             _battleManagerView.PrintMassage($"S Power: {_settlementPower}");
             _battleManagerView.PrintMassage($"S Attack Power: {_settlementAttackPower}");
             _battleManagerView.PrintMassage($"=============================");
             _battleManagerView.PrintMassage($"E Power: {_enemyPower}");
             _battleManagerView.PrintMassage($"E Attack Power: {_enemyAttackPower}");
             
             _settlementHealth = CountHealth(_settlementArmy);
             _enemyHealth = CountHealth(_enemyArmy);

             _battleManagerView.OpenWindow(WindowsType.Battle);
             
             _battleManagerView.SetHealthBarValues(ArmyType.Settlement, _settlementHealth);
             _battleManagerView.SetHealthBarValues(ArmyType.Enemy, _enemyHealth);
             
             _battleManagerView.SetStats(ArmyType.Settlement, _settlementPower, _settlementDefence);
             _battleManagerView.SetStats(ArmyType.Enemy, _enemyPower, _enemyDefence);
             
             _battleManagerView.LoadArmy(ArmyType.Settlement,_settlementArmy);
             _battleManagerView.LoadArmy(ArmyType.Enemy,_enemyArmy);

             StartBattle();
        }

        private void StartBattle()
        {
            int dice = _random.Next(1, 11);

            if (dice <= 5) _isEnemyAttack = true;
            else _isEnemyAttack = false;

            _enemyArmyLose = new();
            _settlementArmyLose = new();

            Battle();
        }

        public void Battle()
        {
            if (_settlementHealth > 0 && _enemyHealth > 0)
            {
                if (!IsAttack)
                {
                    IsAttack = true;
                    
                    if (_isEnemyAttack)
                    {
                        _battleManagerView.StartCoroutine(EnemyAttack);
                    }
                    else
                    {
                        _battleManagerView.StartCoroutine(SettlementAttack);
                    }
                }
            }
            
            if (SettlementHealth <= 0)
            {
                _battleManagerView.CloseWindow(WindowsType.Battle);
                _battleManagerView.OpenWindow(WindowsType.BattleLose);
            }
            else if (EnemyHealth <= 0)
            {
                _battleManagerView.CloseWindow(WindowsType.Battle);
                _battleManagerView.OpenWindow(WindowsType.BattleWin);
            }
        }
        
        private void SettlementAttack()
        {
            Dictionary<WarriorType, int> armyMap = new();
            
            foreach (var type in _enemyArmy.Keys)
            {
                int value;
                if (_enemyArmy[type] - _settlementAttackPower <= 0) value = 0;
                else value = _enemyArmy[type] - _settlementAttackPower;
                
                armyMap.Add(type, value);

                if (!_enemyArmyLose.TryAdd(type, value)) _enemyArmyLose[type] += value;
            }
            
            _enemyArmy = new(armyMap);
            
            if (_enemyDefence - _settlementAttackPower <= 0) _enemyDefence = 0;
            else _enemyDefence -= _settlementAttackPower * _attackPowerPercent;
            
            _enemyPower = CountArmyPower(_enemyArmy);
            _enemyAttackPower = CalculateAttackPower(_enemyPower);
            
            EnemyHealth = CountHealth(_enemyArmy);
            
            _battleManagerView.PrintMassage("========================");
            _battleManagerView.PrintMassage($"E Attack Power: {_enemyAttackPower}");
            _battleManagerView.PrintMassage($"E Health: {EnemyHealth}");
            
            _battleManagerView.SetStats(ArmyType.Enemy, _enemyPower, _enemyDefence);
            _battleManagerView.LoadArmy(ArmyType.Enemy, _enemyArmy);
            
            _isEnemyAttack = true;
        }

        private void EnemyAttack()
        {
            Dictionary<WarriorType, int> armyMap = new();
            
            foreach (var type in _settlementArmy.Keys)
            {
                int value;
                if (_settlementArmy[type] - _enemyAttackPower <= 0) value = 0;
                else value = _settlementArmy[type] - _enemyAttackPower;
                
                armyMap.Add(type, value);

                if (!_settlementArmyLose.TryAdd(type, value)) _settlementArmyLose[type] += value;
            }

            _settlementArmy = armyMap;

            if (_settlementDefence - _enemyAttackPower <= 0) _settlementDefence = 0;
            else _settlementDefence -= _enemyAttackPower * _attackPowerPercent;
            
            _settlementPower = CountArmyPower(_settlementArmy);
            _settlementAttackPower = CalculateAttackPower(_settlementPower);
            
            SettlementHealth = CountHealth(_settlementArmy);
            
            _battleManagerView.PrintMassage("========================");
            _battleManagerView.PrintMassage($"S Attack Power: {_settlementAttackPower}");
            _battleManagerView.PrintMassage($"S Health: {SettlementHealth}");
            
            _battleManagerView.SetStats(ArmyType.Settlement,_settlementPower, _settlementDefence);
            _battleManagerView.LoadArmy(ArmyType.Settlement, _settlementArmy);
            
            _isEnemyAttack = false;
        }

        private int CountHealth(Dictionary<WarriorType, int> armyMap)
        {
            int health = 0;

            foreach (var type in armyMap.Keys)
            {
                health += armyMap[type] * _warriorsMap[type].Power;
                health += armyMap[type] * _warriorsMap[type].Armor;
            }

            return health;
        }

        private int CountArmyPower(Dictionary<WarriorType, int> armyMap)
        {
            int power = 0;

            foreach (var type in armyMap.Keys)
            {
                power = armyMap[type] * _warriorsMap[type].Power;
            }

            return power;
        }

        private int CountArmyArmor(Dictionary<WarriorType, int> armyMap)
        {
            int armor = 0;

            foreach (var type in armyMap.Keys)
            {
                armor = armyMap[type] * _warriorsMap[type].Armor;
            }

            return armor;
        }

        private int CalculateAttackPower(int power)
        {
            return _random.Next(power - (power / 10), power + (power / 10)) / _attackPowerPercent;
        }
    }
}