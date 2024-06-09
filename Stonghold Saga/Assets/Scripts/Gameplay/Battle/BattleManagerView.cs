using System;
using System.Collections;
using System.Collections.Generic;
using Windows;
using AYellowpaper.SerializedCollections;
using Gameplay.Settlement.Warriors;
using Gameplay.Windows;
using UnityEngine;

namespace Gameplay.Battle
{
    public class BattleManagerView : MonoBehaviour
    {
        [Header("Time Manager View")] 
        [SerializeField] private TimeManagerView _timeManagerView;
        
        [Header("Gameplay Manager")]
        [SerializeField] private GameplayManager _gameplayManager;

        [Header("Windows Map")] 
        [SerializedDictionary] public SerializedDictionary<WindowsType, AbstractWindow> windowsMap;
        
        [Header("Battle Window")]
        [SerializeField] private BattleWindow _battleWindow;

        [Header("Background Lock Window")]
        [SerializeField] private AbstractWindow _lockWindow;

        private AbstractWindow _currentWindow;
        
        private BattleManager _battleManager;

        private Coroutine _currentCoroutine;

        private void Start()
        {
            _gameplayManager.OnSettlementManagerInitialisation += Initialise;
        }

        private void Initialise()
        {
            _battleManager = _gameplayManager.BattleManager;
            
            _battleManager.ConnectBattleView(this);
            
            _gameplayManager.OnSettlementManagerInitialisation -= Initialise;
        }
        
        public void PauseGame()
        {
            _timeManagerView.OnPauseButton();
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
            yield return new WaitForSeconds(4f);
            
            action?.Invoke();
            
            yield return new WaitForSeconds(2f);
            
            _battleManager.IsAttack = false;
            
            _battleManager.Battle();
        }
        
        public void OpenWindow(WindowsType windowsType)
        {
            _currentWindow?.CloseWindow();
            
            _currentWindow = windowsMap[windowsType];
            
            _lockWindow.OpenWindow();
            _currentWindow.OpenWindow();
        }

        public void CloseWindow(WindowsType windowsType)
        {
            _lockWindow.CloseWindow();
            windowsMap[windowsType].CloseWindow();
        }

        public void LoadArmy(ArmyType armyType, Dictionary<WarriorType, int> armyMap)
        {
            _battleWindow.LoadArmy(armyType, armyMap);
        }

        public void SetHealthBarValues(ArmyType armyType, int value)
        {
            _battleWindow.SetHealthBarStartValue(armyType, value);
        }

        public void UpdateHealthBar(ArmyType armyType, int value)
        {
            _battleWindow.UpdateHealthBar(armyType, value);
        }

        public void SetStats(ArmyType armyType, int power, int defence)
        {
            _battleWindow.SetStats(armyType, power, defence);
        }

        public void PrintMassage(string massage)
        {
            print(massage);
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