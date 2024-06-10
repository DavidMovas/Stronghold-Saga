using System;
using System.Collections;
using System.Collections.Generic;
using Windows;
using AYellowpaper.SerializedCollections;
using Gameplay.Settlement.Warriors;
using Gameplay.Windows;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Battle
{
    public class BattleManagerView : MonoBehaviour
    {
        [Header("Time Manager View")] 
        [SerializeField] private TimeManagerView timeManagerView;
        
        [Header("Gameplay Manager")]
        [SerializeField] private GameplayManager gameplayManager;

        [Header("Windows Map")] 
        [SerializedDictionary] public SerializedDictionary<WindowsType, AbstractWindow> windowsMap;
        
        [Header("Battle Window")]
        [SerializeField] private BattleWindow battleWindow;

        [Header("Battle Result Windows")] 
        [SerializeField] private BattleResultWindow battleWinWindow;
        [SerializeField] private BattleResultWindow battleLoseWindow;
        
        [Header("Background Lock Window")]
        [SerializeField] private AbstractWindow lockWindow;

        private AbstractWindow _currentWindow;
        
        private BattleManager _battleManager;

        private Coroutine _currentCoroutine;

        private void Start()
        {
            gameplayManager.OnSettlementManagerInitialisation += Initialise;
        }

        private void Initialise()
        {
            _battleManager = gameplayManager.BattleManager;
            
            _battleManager.ConnectBattleView(this);
            
            gameplayManager.OnSettlementManagerInitialisation -= Initialise;
        }
        
        public void PauseGame()
        {
            timeManagerView.OnPauseButton();
        }

        public void StartCoroutine(Action action)
        {
            StartCoroutine(Attack(action));
        }

        public void StopCoroutine()
        {
            StopCoroutine(Attack(null));
        }

        private IEnumerator Attack(Action action)
        {
            yield return new WaitForSeconds(2.5f);
            
            action?.Invoke();
            
            yield return new WaitForSeconds(1f);
            
            _battleManager.IsAttack = false;
            
            _battleManager.Battle();
        }
        
        public void OpenWindow(WindowsType windowsType)
        {
            _currentWindow?.CloseWindow();
            
            _currentWindow = windowsMap[windowsType];
            
            lockWindow.OpenWindow();
            
            _currentWindow.OpenWindow();
        }

        public void CloseWindow(WindowsType windowsType)
        {
            lockWindow.CloseWindow();
            windowsMap[windowsType].CloseWindow();
        }

        public void LoadArmy(ArmyType armyType, Dictionary<WarriorType, int> armyMap, bool anim = true)
        {
            battleWindow.LoadArmy(armyType, armyMap, anim);
        }

        public void SetHealthBarValues(ArmyType armyType, int value)
        {
            battleWindow.SetHealthBarStartValue(armyType, value);
        }

        public void UpdateHealthBar(ArmyType armyType, int value)
        {
            battleWindow.UpdateHealthBar(armyType, value);
        }

        public void SetStats(ArmyType armyType, int power, int defence)
        {
            battleWindow.SetStats(armyType, power, defence);
        }

        public void LoadBattleResult(WindowsType windowsType,
            int startPower, int losePower, int startDefence, int loseDefence, int workersLose,
            Dictionary<WarriorType, int> armyLoseMap)
        {
            if (windowsType == WindowsType.BattleWin)
            {
                battleWinWindow.LoadData(startPower, losePower, startDefence, loseDefence, workersLose, armyLoseMap);
            }
            else if (windowsType == WindowsType.BattleLose)
            {
                battleLoseWindow.LoadData(startPower, losePower, startDefence, loseDefence, workersLose, armyLoseMap);
            }
        }
    }

    public enum WindowsType
    {
        GameLose,
        GameWin,
        Battle,
        BattleWin,
        BattleLose,
    }
}