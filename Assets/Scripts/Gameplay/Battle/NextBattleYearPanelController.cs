using System;
using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextBattleYearPanelController : MonoBehaviour
{
    [Header("Time Manager")] 
    [SerializeField] private TimeManager timeManager;
    
    [Header("Battle Year Text")] 
    [SerializeField] private TextMeshProUGUI nextBattleYearText;

    [Header("Progress Bar Filler Image")]
    [SerializeField] private Image progressBarFillerImage;

    private int _targetYear;
    private int _startYear;

    private int _targetYearInDays;
    private int _startYearInDays;
    private int _currentYearInDays;
    
    private int _currentYear;

    private float _currentValue;
    
    private void Start()
    {
        timeManager.OnDayChanged += IncrementDays;
    }

    private void OnDisable()
    {
        timeManager.OnDayChanged -= IncrementDays;
    }

    public void SetNextBattleYear(int year)
    {
        _targetYear = year;
        _startYear = timeManager.Year;
        _currentYear = _startYear;
        
        _startYearInDays = _startYear * 360;
        _targetYearInDays = _targetYear * 360;
        _currentYearInDays = _startYear * 360 + 10;
        
        progressBarFillerImage.fillAmount = 0f;
        nextBattleYearText.text = _targetYear.ToString();
    }

    private void UpdateProgressBar(float amount) => progressBarFillerImage.fillAmount = amount;

    private void IncrementDays()
    {
        _currentYearInDays += 1;
        
        _currentValue = CountValue(_currentYearInDays);
        
        UpdateProgressBar(_currentValue);
    }

    private float CountValue(int currentYearInDays)
    {
        float first = (float) currentYearInDays - _startYearInDays;
        float second = (float) _targetYearInDays - _startYearInDays;
        
        return first / second;
    }
}
