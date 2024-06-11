using Windows;
using UnityEngine;

namespace Main_Manu
{
    public class NameWindow : AbstractWindow
    {
        [SerializeField] private MainMenuWindow mainManuWindow;
        
        public string SettlementName
        {
            get => _settlementName;
        }

        private bool _canPlay;

        private string _settlementName;
        
        public void OnBackButton()
        {
            OpenWindow(mainManuWindow);
        }

        public void OnStartButton()
        {
            if(_canPlay) mainManuWindow.LoadGameplayScene();
        }

        public void OnEndChangeInputField(string input)
        {
            if (input.Length > 1 && input.Length <= 20)
            {
                _settlementName = input;
                _canPlay = true;
            }
            else _canPlay = false;
        }
    }
}