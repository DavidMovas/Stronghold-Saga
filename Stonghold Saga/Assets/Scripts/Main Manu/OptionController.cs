using UnityEngine;

public class OptionController : MonoBehaviour
{
   [SerializeField] private SettingsManager settingsManager;
   
   [SerializeField] private SoundType soundType;

   public void OnSliderValueChange(float value)
   {
      settingsManager.SetVolume(soundType, value);
   }
}
