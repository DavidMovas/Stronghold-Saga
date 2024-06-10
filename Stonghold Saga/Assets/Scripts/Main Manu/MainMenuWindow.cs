using Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuWindow :AbstractWindow
{
    [SerializeField] private AbstractWindow nameWindow;
    [SerializeField] private AbstractWindow optionsWindow;
   
   public void OnStartNewGameButton()
   {
      OpenWindow(nameWindow);
   }

   public void OnOptionsWindow()
   {
       OpenWindow(optionsWindow);
   }

   public void LoadGameplayScene()
   {
       SceneManager.LoadScene(1);
   }
}
