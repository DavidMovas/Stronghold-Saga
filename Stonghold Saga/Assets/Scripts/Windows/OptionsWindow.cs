using Windows;
using UnityEngine;

public class OptionsWindow : AbstractWindow
{
    [SerializeField] private AbstractWindow mainManuWindow;
    
    public void OnCloseButton()
    {
        OpenWindow(mainManuWindow);
    }
}
