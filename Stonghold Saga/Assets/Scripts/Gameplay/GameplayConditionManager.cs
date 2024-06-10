using Windows;
using Gameplay.Windows;
using UnityEngine;

namespace Gameplay
{
    public class GameplayConditionManager : MonoBehaviour
    {
        [Header("Time Manager")] 
        [SerializeField] private TimeManager timeManager;

        [Header("Game Win Window")] 
        [SerializeField] private GameResultWindow gameWinWindow;

        [Header("Game Lose Window")] 
        [SerializeField] private GameResultWindow gameLoseWindow;

        [Header("Lock Background Window")]
        [SerializeField] private AbstractWindow lockBackgroundWindow;

        [Header("Amount Battle Loses To Lose Game")]
        [SerializeField] private int loseBattleAmountToLoseGame;

        public int WindsAmount
        {
            get
            {
                return _winsAmount;
            }
            set
            {
                _winsAmount = value;
            }
        }
        public int LoseAmount
        {
            get
            {
                return _loseAmount;
            }

            set
            {
                _loseAmount = value;

                if (_loseAmount >= loseBattleAmountToLoseGame)
                {
                    OnGameLoseEvent();
                }
            }
        }

        private int _winsAmount;
        private int _loseAmount;
        
        private void Start()
        {
            timeManager.OnYearGameEndReached += OnGameWinEvent;
        }

        private void OnDisable()
        {
            timeManager.OnYearGameEndReached -= OnGameWinEvent;
        }

        private void OnGameWinEvent()
        {
            gameWinWindow.OpenWindow();
            lockBackgroundWindow.OpenWindow();
        }

        private void OnGameLoseEvent()
        {
            gameLoseWindow.OpenWindow();
            lockBackgroundWindow.OpenWindow();
        }
    }
}