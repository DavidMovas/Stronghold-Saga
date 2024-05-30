namespace Gameplay.Settlement
{
    public class SettlementManager
    {
        public SettlementStorage SettlementStorage => _settlementStorage;
        public WorkersHub WorkersHub => _workersHub;
        
        private SettlementStorage _settlementStorage;
        private WorkersHub _workersHub;

        public SettlementManager(SettlementStorage storage, WorkersHub workersHub)
        {
            _settlementStorage = storage;
            _workersHub = workersHub;
        }
    }
}