using UnityEngine;

namespace Gameplay.Settlement.Warriors
{
    public class WarriorsHubView : MonoBehaviour
    {
        [Header("Warriors Windows")] 
        [SerializeField] private GameObject _warriorsBarrackWindow;
        [SerializeField] private GameObject _warroirsListWindow;
        
        public void SwitchWindows()
        {
            if (_warriorsBarrackWindow.activeSelf)
            {
                _warriorsBarrackWindow.SetActive(false);
                _warroirsListWindow.SetActive(true);
            }
            else if(_warroirsListWindow.activeSelf)
            {
                _warroirsListWindow.SetActive(false);
                _warriorsBarrackWindow.SetActive(true);
            }
        }
    }
}