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
            _workersHub = gameplayManager.SettlementManager.WorkersHub;
            
            _daysAmount = _workersHub.SpawnDayScale;
            
            _workersHub.OnWorkersAmountChange += UpdateWorkersValue;
            _workersHub.OnNewSpawnDay += UpdateWorkersSpawnProgressBar;

            _sawnDaysScaletext.text = $"{_daysAmount} D";
        }

        private void OnDisable()
        {
            _workersHub.OnWorkersAmountChange -= UpdateWorkersValue;
            _workersHub.OnNewSpawnDay -= UpdateWorkersSpawnProgressBar;
        }

        private void UpdateWorkersValue(int amount)
        {
            _amountText.text = amount.ToString();
        }

        private void UpdateWorkersSpawnProgressBar(int days)
        {
            _progressBar.fillAmount = (float)days / (float)_daysAmount;
        }
    }
}