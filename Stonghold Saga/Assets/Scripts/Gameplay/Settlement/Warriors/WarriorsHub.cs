using System.Collections.Generic;

namespace Gameplay.Settlement.Warriors
{
    public class WarriorsHub
    {
        private TimeManager _timeManager;
        private Dictionary<WarriorType, int> _warriorsMap;

        private List<WarriorView> _warriorViews;
        
        private bool _isSomeWarriorView = false;

        public WarriorsHub(TimeManager timeManager)
        {
            _timeManager = timeManager;
            
            _warriorViews = new();
            _warriorsMap = new();

            _timeManager.OnDayChanged += CreateWarriors;
            _timeManager.OnYearChanged += ToActivateWarriorView;
        }
        
        ~WarriorsHub(){
            _timeManager.OnDayChanged -= CreateWarriors;
            _timeManager.OnYearChanged -= ToActivateWarriorView;
        }

        public void ConnectToHub(WarriorView warriorView)
        {
            _warriorViews.Add(warriorView);
            
            if (_warriorViews != null && _warriorViews.Count > 0) _isSomeWarriorView = true;
        }

        private void AddWarrior(WarriorType type)
        {
            if (!_warriorsMap.TryAdd(type, 1))
            {
                _warriorsMap[type] += 1;
            }
        }

        private void CreateWarriors()
        {
            foreach (var warrior in _warriorViews)
            {
                if (warrior.isActive)
                {
                    warrior.CurrentDay += 1;
    
                    if (warrior.CurrentDay >= warrior.SpawnDaysScale)
                    {
                        AddWarrior(warrior.WarriorType);
                        
                        warrior.CurrentDay = 0;
                    }
                }
            }
        }

        private void ToActivateWarriorView()
        {
            if (_warriorViews != null && _warriorViews.Count > 0)
            {
                foreach (var view in _warriorViews)
                {
                    if (view.YearOfSpawn == _timeManager.Year)
                    {
                        view.ChangeState();
                    }
                }
            }
        }
    }

    public enum WarriorType
    {
        Soldier,
        Archer,
        Knight,
        WarHorse,
    }
}
