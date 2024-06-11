using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Settlement
{
    public class WorkersHubView : MonoBehaviour
    {
        [Header("Game Manager")] 
        [SerializeField] private GameplayManager gameplayManager;
        
        [Header("Workers amount text")]
        [SerializeField] private TextMeshProUGUI _amountText;

        [Header("Workers spawn days scale")]
        [SerializeField] private TextMeshProUGUI _sawnDaysScaletext;

        [Header("Workers spawn progress bar")] 
        [SerializeField] private Image _progressBar;

        private WorkersHub _workersHub;
        
        private int _daysAmount;

        private void Start()
        {
            gameplayManager.OnSettlementManagerInitialisation += Initialise;
        }

        private void Initialise()
        {
            _workersHub = gameplayManager.SettlementManager.WorkersHub;
            _daysAmount = _workersHub.SpawnDayScale;
            
            _sawnDaysScaletext.text = $"{_daysAmount} D";
            
            _workersHub.OnWorkersAmountChange += UpdateWorkersValue;
            _workersHub.OnNewSpawnDay += UpdateWorkersSpawnProgressBar;
            gameplayManager.OnSettlementManagerInitialisation -= Initialise;
        }

        private void OnDisable()
        {
            _workersHub.OnWorkersAmountChange -= UpdateWorkersValue;
            _workersHub.OnNewSpawnDay -= UpdateWorkersSpawnProgressBar;
        }

        private void UpdateWorkersValue(int amount, int maxAmount)
        {
            _amountText.text = $"{amount}/{maxAmount}";
        }

        private void UpdateWorkersSpawnProgressBar(int days)
        {
            _progressBar.fillAmount = (float)days / (float)_daysAmount;
        }
    }
}