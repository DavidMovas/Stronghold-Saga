using Gameplay;
using Gameplay.Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextBattleYearPanelController : MonoBehaviour
{
    [Header("Time Manager")] 
    [SerializeField] private TimeManager timeManager;
    
    [Header("Battle Manager View")]
    [SerializeField] private BattleManagerView battleManagerView;

    [Header("Battle Year Text")] 
    [SerializeField] private TextMeshProUGUI nextBattleYearText;

    [Header("Progress Bar Filler Image")]
    [SerializeField] private Image progressBarFillerImage;

    private int _targetYear;
    
    private int _month;
    private int _year;

    private bool _isProgress;

    private void Start()
    {
        _year = timeManager.Year;
        
        timeManager.OnMonthChanged += IncrementMonth;
    }

    private void OnDisable()
    {
        timeManager.OnMonthChanged -= IncrementMonth;
    }

    private void Update()
    {
        if (_isProgress)
        {
            
        }
    }

    public void SetNextBattleYear(int year)
    {
        _year = timeManager.Year;
        _targetYear = year;
        
        if (_targetYear > _year) _isProgress = true;

        progressBarFillerImage.fillAmount = 0f;
    }

    private void UpdateProgressBar()
    {
        progressBarFillerImage.fillAmount =  ((float) _year / 12) / ((float) _targetYear / 12);
    }

    private void IncrementMonth()
    {
        _month += 1;

        if (_month / 12 == 0)
        {
            _month = 0;
            _year += 1;
        }

        UpdateProgressBar();
        
        if (_year == _targetYear) progressBarFillerImage.fillAmount = 1f;
    }
    
}
