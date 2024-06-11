using System;
using System.Collections.Generic;

namespace Gameplay.Settlement
{
    public class SettlementStorage
    {
        public event Action<ResourcesType, int> OnStorageValueUpdate;
        
        private Dictionary<ResourcesType, int> _settlementStorage;

        public SettlementStorage(Dictionary<ResourcesType, int> storage)
        {
            _settlementStorage = storage;
        }

        public void AddResource(ResourcesType resourcesType, int amount)
        {
            if (_settlementStorage.ContainsKey(resourcesType))
            {
                _settlementStorage[resourcesType] += amount;
            }
            else
            {
                _settlementStorage.Add(resourcesType, amount);
            }
            
            OnStorageValueUpdate?.Invoke(resourcesType, _settlementStorage[resourcesType]);
        }

        public void GetResource(ResourcesType resourcesType, int amount)
        {
            if (_settlementStorage.ContainsKey(resourcesType))
            {
                if (_settlementStorage[resourcesType] >= amount)
                {
                    _settlementStorage[resourcesType] -= amount;
                    
                    OnStorageValueUpdate?.Invoke(resourcesType, _settlementStorage[resourcesType]);
                }
            }
        }

        public void SetResource(ResourcesType resourcesType, int amount)
        {
            if (!_settlementStorage.TryAdd(resourcesType, amount))
            {
                _settlementStorage[resourcesType] = amount;
            }
            
            OnStorageValueUpdate?.Invoke(resourcesType, _settlementStorage[resourcesType]);
        }

        public bool CheckResource(ResourcesType resourcesType, int amount)
        {
            if (_settlementStorage.ContainsKey(resourcesType))
            {
                if (_settlementStorage[resourcesType] >= amount) return true;
            }

            return false; 
        }

        public int GetResourceAmount(ResourcesType type)
        {
            int amount;

            if (_settlementStorage.ContainsKey(type))
            {
                amount = _settlementStorage[type];
                return amount;
            }

            return 0;
        }
    }

    public enum ResourcesType
    {
        Wheat,
        Wood,
        Iron,
        Power,
        Armor,
        Defense,
        Time,
    }
}