using Windows;
using UnityEngine;

namespace Main_Manu
{
    public class NameWindow : AbstractWindow
    {
        [SerializeField] private AbstractWindow _mainManuWindow;
        
        public string SettlementName
        {
            get => _settlementName;
        }

        private string _settlementName;
        
        public void OnBackButton()
        {
            OpenWindow(_mainManuWindow);
        }

        public void OnEndChangeInputField(string input)
        {
            if (input.Length > 1 && input.Length <= 20)
            {
                _settlementName = input;
                
                print(_settlementName);
            }
        }
    }
}