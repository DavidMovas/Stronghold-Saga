using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;

namespace Gameplay.Settlement.SettlementView
{
    public class SettlementView: MonoBehaviour
    {
        [Header("Gameplay Manager")] 
        [SerializeField] private GameplayManager gameplayManager;

        [Header("Resource - UI Text Map")] [SerializedDictionary("ResourcesType", "Amount")]
        public SerializedDictionary<ResourcesType, TextMeshProUGUI> _textMap;

        private void Start()
        {
            gameplayManager.OnSettlementManagerInitialisation += Initiallise;
        }

        private void Initiallise()
        {
            gameplayManager.SettlementManager.SettlementStorage.OnStorageValueUpdate += UpdateResourceView;
            gameplayManager.OnSettlementManagerInitialisation -= Initiallise;
        }

        private void OnDisable()
        {
            gameplayManager.SettlementManager.SettlementStorage.OnStorageValueUpdate -= UpdateResourceView;
        }

        private void UpdateResourceView(ResourcesType resourcesType, int amount)
        {
            if (_textMap.ContainsKey(resourcesType))
            {
                decimal value = amount;
                
                if (value < 10_000)
                {
                    _textMap[resourcesType].text = $"{value}";
                }
                else if (value > 10_000 && value < 1_000_000)
                {
                    _textMap[resourcesType].text = $"{value / 1_000:N1} K";
                }
                else
                {
                    _textMap[resourcesType].text = $"{value / 1_000_000:N1} M";
                }
            }
        }
    }
}