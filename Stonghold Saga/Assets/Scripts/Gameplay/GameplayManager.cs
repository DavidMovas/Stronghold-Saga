using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Gameplay.Battle;
using Gameplay.Settlement;
using Gameplay.Settlement.Warriors;
using UnityEngine;

namespace Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        public event Action OnSettlementManagerInitialisation; 
        
        [Header("Time Manager")]
        [SerializeField] private TimeManager timeManager;

        [Header("Gameplay Condition Manager")]
        [SerializeField] private GameplayConditionManager gameplayConditionManager;

        [Header("Workers Hub Configs")] 
        [SerializeField] private int startAmount = 10;
        [SerializeField] private int maxAmount = 250;
        [SerializeField] private int spawnDayScale = 30;

        [Header("Battle Manager Configs")] 
        [SerializeField] private int attackPowerPercent = 6;
        [SerializeField] private int minYearToBattleStep = 1;
        [SerializeField] private int maxYearToBattleStep = 10;
        [SerializeField] private int maxDeferenceUnitsAmount = 25;
        
        [Header("Next Year Battle Controller")] 
        [SerializeField] private NextBattleYearPanelController yearPanelController;

        [Header("Warriors Configs")] 
        [SerializedDictionary] public SerializedDictionary<WarriorType, WarriorScriptableObjectConfig> warriorsConfigsMap;

        [Header("Warriors Stats Configs")] 
        [SerializeField] public WarriorsStatsConfigs warriorsStatsConfigs;

        public GameplayConditionManager GameplayConditionManager => gameplayConditionManager;
        public SettlementManager SettlementManager => _settlementManager;
        public BattleManager BattleManager => _battleManager;
        
        private SettlementManager _settlementManager;
        private BattleManager _battleManager;
        
        private void Start()
        {
            _settlementManager = new SettlementManager(
                new SettlementStorage(new Dictionary<ResourcesType, int>()),
                new WorkersHub(timeManager, maxAmount, startAmount, spawnDayScale),
                new WarriorsHub(this, timeManager),
                new WarriorsManager(warriorsConfigsMap)
                );

            _battleManager = new BattleManager(
                this,
                timeManager,
                minYearToBattleStep,
                maxYearToBattleStep,
                maxDeferenceUnitsAmount,
                attackPowerPercent
                );

            StartCoroutine(Notificate());
        }

        public void SetNextBattleYear(int year) => yearPanelController.SetNextBattleYear(year);
        
        private IEnumerator Notificate()
        {
            yield return new WaitForSecondsRealtime(2f);
            
            OnSettlementManagerInitialisation?.Invoke();
        }
    }
}