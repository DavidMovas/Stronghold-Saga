using Windows;
using Gameplay.Settlement;
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

        [Header("Sounds")] 
        [SerializeField] private SFXController sfxController;
        [SerializeField] private AudioClip gameWinClip;
        [SerializeField] private AudioClip gameLoseClip;

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
            lockBackgroundWindow.OpenWindow();
            gameWinWindow.OpenWindow();
            sfxController.PlayClip(gameWinClip);
        }

        private void OnGameLoseEvent()
        {
            lockBackgroundWindow.OpenWindow();
            gameLoseWindow.OpenWindow();
            sfxController.PlayClip(gameLoseClip);
        }
    }
}