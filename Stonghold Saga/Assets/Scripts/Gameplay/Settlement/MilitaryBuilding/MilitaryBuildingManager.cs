using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Gameplay.Settlement.Warriors;
using UnityEngine;

namespace Gameplay.Settlement.MilitaryBuilding
{
    public class MilitaryBuildingManager : MonoBehaviour
    {
        [Header("Gameplay Manager")] 
        [SerializeField] private GameplayManager _gameplayManager;

        [Header("Military Building List")] 
        [SerializeField] private List<MilitaryBuilding> _militaryBuildings;

        [Header("Military Building Configs")]
        [SerializedDictionary] public SerializedDictionary<MilitaryBuildingType, MilitaryBuildingScriptableObjectConfig> militaryBuildingsConfigsMap;

        private SettlementStorage _settlementStorage;
        private WarriorsHub _warriorsHub;

        private void Start()
        {
            _gameplayManager.OnSettlementManagerInitialisation += Initialise;

            LoadDataToBuildings();
        }

        private void Initialise()
        {
            _settlementStorage = _gameplayManager.SettlementManager.SettlementStorage;
            _warriorsHub = _gameplayManager.SettlementManager.WarriorsHub;
            
            _gameplayManager.OnSettlementManagerInitialisation -= Initialise;
        }

        private void LoadDataToBuildings()
        {
            foreach (var building in _militaryBuildings)
            {
                MilitaryBuildingScriptableObjectConfig config = militaryBuildingsConfigsMap[building.militaryBuildingType];
                
                building.LoadData
                    (
                        this,
                        config.startLevel,
                        config.levelsAmount,
                        config.buildResourcesMap,
                        config.upgradeResourcesMap,
                        config.buildingResourcesMap,
                        config.buildingBonusResourcesMap
                        );
            }
        }

        public void ApplyBuildingBonuses(Dictionary<ResourcesType, int> bonuses)
        {
            foreach (var bonus in bonuses.Keys)
            {
                if (bonus == ResourcesType.Defense)
                {
                    _settlementStorage.AddResource(bonus, bonuses[bonus]);
                }
                else
                {
                    _warriorsHub.ApplyBonus(bonus, bonuses[bonus]);
                }
            }
        }

        public bool UpgradeBuilding(Dictionary<ResourcesType, int> upgradeMap)
        {
            bool response = true;

            foreach (var resource in upgradeMap.Keys)
            {
                if (!_settlementStorage.CheckResource(resource, upgradeMap[resource]))
                {
                    if (response) response = false;
                }
            }

            if (response)
            {
                foreach (var resource in upgradeMap.Keys)
                {
                    _settlementStorage.GetResource(resource, upgradeMap[resource]);
                }
            }

            return response;
        }
    }

    public enum MilitaryBuildingType
    {
        Barrack,
        Forge,
        Fortress,
    }
}