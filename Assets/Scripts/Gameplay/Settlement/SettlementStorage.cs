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

        public bool GetResource(ResourcesType resourcesType, int amount)
        {
            if (_settlementStorage.ContainsKey(resourcesType))
            {
                if (_settlementStorage[resourcesType] >= amount)
                {
                    _settlementStorage[resourcesType] -= amount;
                    return true;
                }
                
                OnStorageValueUpdate?.Invoke(resourcesType, _settlementStorage[resourcesType]);
            }

            return false;
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
    }
}