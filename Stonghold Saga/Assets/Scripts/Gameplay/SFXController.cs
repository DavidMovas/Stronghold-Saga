using UnityEngine;

namespace Gameplay.Settlement
{
    public class SFXController : MonoBehaviour
    {
        [Header("Audio Source")]
        [SerializeField] private AudioSource audioSource;

        public void PlayClip(AudioClip clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }

        public void StopPlay()
        {
            audioSource.Stop();
        }
    }
}