using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class TimeManagerView : MonoBehaviour
    {
        [Header("Time Manager")]
        [SerializeField] private TimeManager _timeManager;
        
        [Header("X value time speed")]
        [SerializeField] private TextMeshProUGUI _timeSpeedText;

        [Header("Buttons back panels")] 
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _playPanel;
        [SerializeField] private GameObject _fastPanel;

        private GameObject _currentPanel;
        
        private bool _isPause;
        private bool _isPlay;
        private bool _isFast;
        
        private void Start()
        {
            _currentPanel = _playPanel;
            
            OnPauseButton();
        }

        public void OnPauseButton()
        {
            if (!_isPause)
            {
                _isPlay = false;
                _isFast = false;
                
                _isPause = true;
                _timeSpeedText.text = "X1";
                _timeManager.ManageTime(TimeManageType.Pause);
                
                SwitchButtonBackPanel(_pausePanel);
            }
        }

        public void OnPlayButton()
        {
            if (!_isPlay)
            {
                _isPause = false;
                _isFast = false;
                
                _isPlay = true;
                _timeSpeedText.text = "X1";
                _timeManager.ManageTime(TimeManageType.Play);
                
                SwitchButtonBackPanel(_playPanel);
            }
        }

        public void OnFastButton()
        {
            if (!_isFast)
            {
                _isPause = false;
                _isPlay = false;
                
                _isFast = true;
                _timeSpeedText.text = "X2";
                _timeManager.ManageTime(TimeManageType.Fast);
                
                SwitchButtonBackPanel(_fastPanel);
            }
        }

        private void SwitchButtonBackPanel(GameObject panel)
        {
            if (_currentPanel != null)
            {
                _currentPanel.SetActive(false);
                _currentPanel = panel;
                _currentPanel.SetActive(true);
            }
        }
    }
}