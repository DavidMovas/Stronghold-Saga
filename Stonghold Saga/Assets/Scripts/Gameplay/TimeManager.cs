using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class TimeManager : MonoBehaviour
    {
        public event Action OnDayChanged;
        public event Action OnMonthChanged;
        public event Action OnYearChanged;
        public event Action OnYearGameEndReached;

        [Header("Time Manager View")]
        [SerializeField] private TimeManagerView _timeManagerView;

        [Header("Year of game end")]
        [SerializeField] private int _targetYear;
        
        [Header("Duration per day in seconds")]
        [SerializeField] private float _oneDayDuration;

        [Header("Month and year display text")] 
        [SerializeField] private TextMeshProUGUI _timeText;

        public int Day
        {
            set
            {
                _currentDay = value;

                if (_currentDay > 30)
                {
                    _currentDay = 1;

                    Month += 1;
                }
                
                OnDayChanged?.Invoke();
            }

            get
            {
                return _currentDay;
            }
        }

        public int Month
        {
            set
            {
                _currentMonth = value;

                if (_currentMonth > 12)
                {
                    _currentMonth = 1;

                    Year += 1;
                }


                DisplayTime();
                OnMonthChanged?.Invoke();
            }

            get
            {
                return _currentMonth;
            }
        }

        public int Year
        {
            set
            {
                _currentYear = value;

                if (_currentYear >= _targetYear)
                {
                    _timeManagerView.OnPauseButton();
                    
                    OnYearGameEndReached?.Invoke();
                }

                DisplayTime();
                CheckYearOnEvents();
                
                OnYearChanged?.Invoke();
            }

            get
            {
                return _currentYear;
            }
        }

        private int _currentDay = 1;
        private int _currentMonth = 1;
        private int _currentYear = 1250;

        private Dictionary<int, List<Action>> _eventsMap;

        private float _timer;
        private float _currentOneDayDuration;
        private bool _isProcessing;

        private void Start()
        {
            _timer = 0;
            _currentOneDayDuration = _oneDayDuration;

            _eventsMap = new();
            
            DisplayTime();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_isProcessing && _timer >= _currentOneDayDuration)
            {
                Day += 1;

                _timer = 0;
            }
        }

        public void ManageTime(TimeManageType manageType)
        {
            if (manageType == TimeManageType.Play)
            {
                _currentOneDayDuration = _oneDayDuration;
                _isProcessing = true;
            }
            else if(manageType == TimeManageType.Fast)
            {
                _currentOneDayDuration /= 2;
                _isProcessing = true;
            }
            else if(manageType == TimeManageType.Pause)
            {
                _currentOneDayDuration = _oneDayDuration;
                _isProcessing = false;
            }
        }

        public void SubscribeOnEvent(TimeEventType eventType, Action action)
        {
            if (eventType == TimeEventType.OnDayChanged)
            {
                OnDayChanged += action;
            }
            else if (eventType == TimeEventType.OnMonthChanged)
            {
                OnMonthChanged += action;
            }
            else if(eventType == TimeEventType.OnYearChanged)
            {
                OnYearChanged += action;
            }
            else
            {
                OnYearGameEndReached += action;
            }
        }

        public void UnsubscribeFromEvent(TimeEventType eventType, Action action)
        {
            if (eventType == TimeEventType.OnDayChanged)
            {
                OnDayChanged -= action;
            }
            else if (eventType == TimeEventType.OnMonthChanged)
            {
                OnMonthChanged -= action;
            }
            else if(eventType == TimeEventType.OnYearChanged)
            {
                OnYearChanged -= action;
            }
            else
            {
                OnYearGameEndReached -= action;
            }
        }

        public void SubscribeOnTargetYearEvent(int targetYear, Action action)
        {
            if (_eventsMap != null)
            {
                if (_eventsMap.ContainsKey(targetYear))
                {
                    _eventsMap[targetYear].Add(action);
                }
                else
                {
                    _eventsMap.Add(targetYear, new List<Action> {action});
                }
            }
        }

        private void CheckYearOnEvents()
        {
            List<int> targetYearsList = new();
            
            if (_eventsMap != null && _eventsMap.Count > 0)
            {
                foreach (var year in _eventsMap.Keys)
                {
                    if (_currentYear == year)
                    {
                        targetYearsList.Add(year);

                        foreach (var action in _eventsMap[year])
                        {
                           action?.Invoke();
                        }
                    }
                }

                if (targetYearsList.Count > 0)
                {
                    foreach (var year in targetYearsList)
                    {
                        _eventsMap.Remove(year);
                    }
                }
            }
        }
        
        private void DisplayTime()
        {
            _timeText.text = $"{_currentMonth}.{_currentYear}";
        }
    }

    public enum TimeEventType
    {
        OnDayChanged,
        OnMonthChanged,
        OnYearChanged,
        OnTargetYearReached,
    }

    public enum TimeManageType
    {
        Play,
        Pause,
        Fast,
    }
}