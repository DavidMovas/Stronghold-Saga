using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio Mixer")] 
    [SerializeField] private AudioMixer audioMixer;
    public AudioMixer AudioMixer
    {
        get
        {
            return audioMixer;
        }
        set
        {
            audioMixer = value;
        }
    }

    private Dictionary<SoundType, float> _settingsMap;

    private void Awake()
    {
        _settingsMap = new();
        
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetVolume(SoundType soundType, float volume)
    {
        float value = Mathf.Log10(volume) * 20;
        if (!_settingsMap.TryAdd(soundType, value)) _settingsMap[soundType] = value;
        
        switch (soundType)
        {
            case SoundType.Master:
                audioMixer.SetFloat("Master",  value);
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

    public Dictionary<SoundType, float> CopySettings()
    {
        return _settingsMap;
    }

    public void SetSettings(Dictionary<SoundType, float> settings)
    {
        _settingsMap = settings;
    }
}

public enum SoundType
{
    Master,
    Music,
    SFX,
    UIEffects,
}
