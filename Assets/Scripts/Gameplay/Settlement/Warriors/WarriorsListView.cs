using System;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;

namespace Gameplay.Settlement.Warriors
{
    public class WarriorsListView : MonoBehaviour
    {
        [Header("Game Manager")] 
        [SerializeField] private GameplayManager _gameplayManager;

        [SerializedDictionary] public SerializedDictionary<WarriorType, TextMeshProUGUI> warriorsTextsMap;

        private WarriorsHub _warriorsHub;

        private void Start()
        {
            // _gameplayManager.OnSettlementManagerInitiallisation += Initiallise;
            
            _warriorsHub = _gameplayManager.SettlementManager.WarriorsHub;

            _warriorsHub.OnWarriorsAmountUpdate += UpdateWarriorsAmountView;
        }

        private void Initiallise()
        {
            _warriorsHub = _gameplayManager.SettlementManager.WarriorsHub;

            _warriorsHub.OnWarriorsAmountUpdate += UpdateWarriorsAmountView;
            
            _gameplayManager.OnSettlementManagerInitialisation -= Initiallise;
        }

        private void OnDisable()
        {
            _warriorsHub.OnWarriorsAmountUpdate -= UpdateWarriorsAmountView;
        }

        private void UpdateWarriorsAmountView(WarriorType type, int amount)
        {
            warriorsTextsMap[type].text = amount.ToString();
        }
    }
}