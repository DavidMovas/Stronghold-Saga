using Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuWindow :AbstractWindow
{
   [SerializeField] private AbstractWindow _nameWindow;
   
   public void OnStartNewGameButton()
   {
      OpenWindow(_nameWindow);
   }
}
