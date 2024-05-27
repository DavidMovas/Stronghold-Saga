using Windows;
using UnityEngine;

public class MainMenuWindow :AbstractWindow
{
   [SerializeField] private AbstractWindow _nameWindow;
   
   public void OnStartNewGameButton()
   {
      OpenWindow(_nameWindow);
   }
}
