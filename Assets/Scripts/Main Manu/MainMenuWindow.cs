using System.Collections.Generic;
using Windows;
using UnityEngine;

public class MainMenuWindow :AbstractWindow
{
    [Header("Name Window")]
    [SerializeField] private AbstractWindow nameWindow;
    
    [Header("Options Window")]
    [SerializeField] private AbstractWindow optionsWindow;
    
    [Header("Loader Window")]
    [SerializeField] private LoaderWindow loaderWindow;

    [Header("Options Controllers List")] 
    [SerializeField] private List<OptionController> optionControllersList;

    private SettingsManager _settingsManager;

    private void Start()
    {
        _settingsManager = FindObjectOfType<SettingsManager>();

        SetManagerToOptions();
    }

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
       loaderWindow.LoadLevel(1);
   }

   public void OnGameExitButton()
   {
       Application.Quit();
   }

   private void SetManagerToOptions()
   {
       if (optionControllersList.Count > 0 && _settingsManager != null)
       {
           foreach (var option in optionControllersList)
           {
               option.SetManager(_settingsManager);
           }
       }
   }
}
