using System;

namespace Gameplay.Settlement
{
    public class WorkersHub
    {
        public event Action<int> OnWorkersAmountChange;
        public event Action<int> OnNewSpawnDay;

        public int SpawnDayScale => _spawnDayScale;

        private TimeManager _timeManager;
        
        public int WorkersAmount => _workersAmount;
            
        private int _workersAmount;

        private int _spawnDayScale;
        private int _currentDay;

        public WorkersHub(TimeManager timeManager, int amount, int spawnDayScale)
        {
            _timeManager = timeManager;
            _workersAmount = amount;
            _spawnDayScale = spawnDayScale;

            _timeManager.OnDayChanged += SpawnWorker;
        }

        public void AddWorkers(int amount)
        {
            _workersAmount += amount;
            
            OnWorkersAmountChange?.Invoke(_workersAmount);
        }

        public bool TryGetWorkers(int amount)
        {
            if (_workersAmount >= amount)
            {
                _workersAmount -= amount;
                
                OnWorkersAmountChange?.Invoke(_workersAmount);

                return true;
            }

            return false;
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
            
            OnWorkersAmountChange?.Invoke(_workersAmount);
        }

        private void SpawnWorker()
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