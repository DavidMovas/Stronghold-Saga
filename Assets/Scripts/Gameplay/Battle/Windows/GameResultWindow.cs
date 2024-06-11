using Windows;
using UnityEngine.SceneManagement;

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