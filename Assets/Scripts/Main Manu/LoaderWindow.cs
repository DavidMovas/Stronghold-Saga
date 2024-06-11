using System.Collections;
using Windows;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoaderWindow : AbstractWindow
{
    [Header("Main Manu Window")]
    [SerializeField] private AbstractWindow mainMenuWindow;

    [Header("Name Window")] 
    [SerializeField] private AbstractWindow nameWindow;
    
    [Header("Progress Value Text")]
    [SerializeField] private TextMeshProUGUI progressValueText;
    
    [Header("Progress Value Image")]
    [SerializeField] private Image fillImage;

    public void LoadLevel(int levelId)
    {
        mainMenuWindow.CloseWindow();
        nameWindow.CloseWindow();
        this.OpenWindow();
        
        StartCoroutine(LoadLevelAsync(levelId));
    }

    private IEnumerator LoadLevelAsync(int levelId)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelId);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f );

            fillImage.fillAmount = progressValue;
            progressValueText.text = $"{progressValue * 100} %";

            yield return null;
        }
    }
}
