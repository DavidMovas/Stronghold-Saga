using Windows;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gameplay.Windows
{
    public class GameResultWindow : AbstractWindow
    {
        public void OnExitButton()
        {
            SceneManager.LoadScene(0);
        }
    }
}