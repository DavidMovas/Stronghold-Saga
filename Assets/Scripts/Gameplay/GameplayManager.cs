using System.Collections.Generic;
using Gameplay.Settlement;
using UnityEngine;

namespace Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private TimeManager timeManager;
        
        public SettlementManager SettlementManager => _settlementManager;
        
        private SettlementManager _settlementManager;
        
        private void Awake()
        {
            _settlementManager = new SettlementManager(
                new SettlementStorage(new Dictionary<ResourcesType, int>()),
                new WorkersHub(timeManager,10, 15)
                );
        }
    }
}