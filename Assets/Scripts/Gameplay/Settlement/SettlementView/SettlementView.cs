using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Settlement.SettlementView
{
    public class SettlementView: MonoBehaviour
    {
        [FormerlySerializedAs("_gameManager")]
        [Header("Settlement Manager")] 
        [SerializeField] private GameplayManager gameplayManager;

        [Header("Resource - UI Text Map")] [SerializedDictionary("ResourcesType", "Amout")]
        public SerializedDictionary<ResourcesType, TextMeshProUGUI> _textMap;

        private void Start()
        {
            gameplayManager.SettlementManager.SettlementStorage.OnStorageValueUpdate += UpdateResourceView;
        }

        private void OnDisable()
        {
            gameplayManager.SettlementManager.SettlementStorage.OnStorageValueUpdate -= UpdateResourceView;
        }

        private void UpdateResourceView(ResourcesType resourcesType, int amount)
        {
            if (_textMap.ContainsKey(resourcesType))
            {
                if (amount < 10_000)
                {
                    _textMap[resourcesType].text = $"{amount}";
                }
                else if (amount > 10_000 && amount < 1_000_000)
                {
                    _textMap[resourcesType].text = $"{amount / 1_000:N1} K";
                }
                else
                {
                    _textMap[resourcesType].text = $"{amount / 1_000_000:N1} M";
                }
            }
        }
    }
}