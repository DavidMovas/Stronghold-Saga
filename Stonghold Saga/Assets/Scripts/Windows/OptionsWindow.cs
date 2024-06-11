using Windows;
using UnityEngine;

public class OptionsWindow : AbstractWindow
{
    [SerializeField] private AbstractWindow window;
    
    public void OnCloseButton()
    {
        OpenWindow(window);
    }

    public void OnBackButton()
    {
        this.CloseWindow();
    }
}
