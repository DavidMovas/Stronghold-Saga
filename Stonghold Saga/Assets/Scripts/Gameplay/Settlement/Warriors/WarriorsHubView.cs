using UnityEngine;

namespace Gameplay.Settlement.Warriors
{
    public class WarriorsHubView : MonoBehaviour
    {
        [Header("Warriors Windows")] 
        public CanvasGroup warriorsBarrackWindow;
        public CanvasGroup warriorsListWindow;
        
        public void SwitchWindows()
        {
            if (warriorsBarrackWindow.alpha > 0)
            {
                warriorsBarrackWindow.alpha = 0;
                warriorsListWindow.alpha = 1;
            }
            else if(warriorsListWindow.alpha > 0)
            {
                warriorsListWindow.alpha = 0;
                warriorsBarrackWindow.alpha = 1;
            }
        }
    }
}