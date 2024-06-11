using System;

namespace Gameplay.Settlement
{
    public class WorkersHub
    {
        public event Action<int, int> OnWorkersAmountChange;
        public event Action<int> OnNewSpawnDay;

        public int SpawnDayScale => _spawnDayScale;

        private TimeManager _timeManager;
        
        public int WorkersAmount => _workersAmount;
        private int MaxWorkers => _maxWorkers;
            
        private int _workersAmount;
        private int _maxWorkers;

        private int _spawnDayScale;
        private int _currentDay;

        public WorkersHub(TimeManager timeManager, int maxWorkers, int amount, int spawnDayScale)
        {
            _timeManager = timeManager;
            _workersAmount = amount;
            _maxWorkers = maxWorkers;
            _spawnDayScale = spawnDayScale;

            _timeManager.OnDayChanged += SpawnWorker;
        }
        
        public void GetWorkers(int amount)
        {
            if (_workersAmount >= amount)
            {
                _workersAmount -= amount;
            }
            else
            {
                _workersAmount = 0;
            }
            
            OnWorkersAmountChange?.Invoke(_workersAmount, _maxWorkers);
        }
        
        private void AddWorkers(int amount)
        {
            _workersAmount += amount;
            
            OnWorkersAmountChange?.Invoke(_workersAmount, _maxWorkers);
        }

        private void SpawnWorker()
        {
            if (_workersAmount < _maxWorkers)
            {
                _currentDay += 1;

                if (_currentDay > _spawnDayScale)
                {
                    AddWorkers(1);
                
                    _currentDay = 0;
                }
            
                OnNewSpawnDay?.Invoke(_currentDay);
            }
        }
    }
}