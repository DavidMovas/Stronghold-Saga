using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Mixer")] 
    [SerializeField] private AudioMixer audioMixer;
    
    public void SetVolume(SoundType soundType, float volume)
    {
        switch (soundType)
        {
            case SoundType.Master:
                audioMixer.SetFloat("Master",  Mathf.Log10(volume) * 20);
                break;
            case SoundType.Music:
                audioMixer.SetFloat("Music",  Mathf.Log10(volume) * 20);
                break;
            case SoundType.SFX:
                audioMixer.SetFloat("SFX",  Mathf.Log10(volume) * 20);
                break;
            case SoundType.UIEffects:
                audioMixer.SetFloat("UIEffects",  Mathf.Log10(volume) * 20);
                break;
            default:
                return;
        }
    }
}

public enum SoundType
{
    Master,
    Music,
    SFX,
    UIEffects,
}
