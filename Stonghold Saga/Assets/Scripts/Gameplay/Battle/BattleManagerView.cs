using UnityEngine;

namespace Gameplay.Battle
{
    public class BattleView : MonoBehaviour
    {
        [Header("Gameplay Manager")]
        [SerializeField] private GameplayManager _gameplayManager;

        [Header("Windows")] 
        [SerializeField] private GameObject _lockWindow;
        [SerializeField] private GameObject _gameWinWindow;
        [SerializeField] private GameObject _gameLoseWindow;
        [SerializeField] private GameObject _battleWindow;
        [SerializeField] private GameObject _battleWinWindow;
        [SerializeField] private GameObject _battleLoseWindow;
        private BattleManager _battleManager;


        private void Start()
        {
            _gameplayManager.OnSettlementManagerInitiallisation += Initialise;
        }

        private void Initialise()
        {
            _battleManager = _gameplayManager.BattleManager;
            
            _battleManager.ConnectBattleView(this);
            
            _gameplayManager.OnSettlementManagerInitiallisation -= Initialise;
        }
    }
}