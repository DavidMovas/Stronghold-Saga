using System.Collections.Generic;
using Gameplay.Settlement;
using UnityEngine;

namespace Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        [Header("Time Manager")]
        [SerializeField] private TimeManager timeManager;

        [Header("Workers Hub Configs")] 
        [SerializeField] private int startAmount = 10;
        [SerializeField] private int spawnDayScale = 30;
        
        public SettlementManager SettlementManager => _settlementManager;
        
        private SettlementManager _settlementManager;
        
        private void Awake()
        {
            _settlementManager = new SettlementManager(
                new SettlementStorage(new Dictionary<ResourcesType, int>()),
                new WorkersHub(timeManager, startAmount, spawnDayScale)
                );
        }
    }
}