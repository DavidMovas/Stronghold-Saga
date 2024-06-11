using Gameplay.Settlement.Warriors;

namespace Gameplay.Settlement
{
    public class SettlementManager
    {
        public SettlementStorage SettlementStorage
        {
            get
            {
                if (_settlementStorage != null) return _settlementStorage;
                return null;
            }
        }
        public WorkersHub WorkersHub
        {
            get
            {
                if (_workersHub != null) return _workersHub;
                return null;
            }
        }
        public WarriorsHub WarriorsHub
        {
            get
            {
                if (_warriorsHub != null) return _warriorsHub;
                return null;
            }
        }
        public WarriorsManager WarriorsManager
        {
            get
            {
                return _warriorsManager;
            }
        }

        private SettlementStorage _settlementStorage;
        private WorkersHub _workersHub;
        private WarriorsHub _warriorsHub;
        private WarriorsManager _warriorsManager;

        public SettlementManager(SettlementStorage settlementStorage, WorkersHub workersHub, WarriorsHub warriorsHub, WarriorsManager warriorsManager)
        {
            _settlementStorage = settlementStorage;
            _workersHub = workersHub;
            _warriorsHub = warriorsHub;
            _warriorsManager = warriorsManager;
        }
    }
}