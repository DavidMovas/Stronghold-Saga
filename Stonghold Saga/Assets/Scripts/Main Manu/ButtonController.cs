using UnityEngine;

namespace Main_Manu
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        
        [SerializeField] private AudioClip onButtonClick;
        [SerializeField] private AudioClip onButtonPointerEnter;
        
        public void OnButtonClick()
        {
            audioSource.clip = onButtonClick;
            audioSource.Play();
        }

        public void OnButtonPointerEnter()
        {
            audioSource.clip = onButtonPointerEnter;
            audioSource.Play();
        }
    }
}