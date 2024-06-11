using UnityEngine;

namespace Windows
{
    public abstract class AbstractWindow : MonoBehaviour
    {
        public virtual void OpenWindow(AbstractWindow window)
        {
            CloseWindow(this);
            window.gameObject.SetActive(true);
        }

        public virtual void OpenWindow()
        {
            this.gameObject.SetActive(true);
        }

        public virtual void CloseWindow(AbstractWindow window)
        {
            window.gameObject.SetActive(false);
        }

        public virtual void CloseWindow()
        {
            this.gameObject.SetActive(false);
        }
    }
}