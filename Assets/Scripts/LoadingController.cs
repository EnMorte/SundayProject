using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public enum EScene
{
    MainMenu = 0,
    Loading = 1,
    Gallery = 2,
    Rotation = 3,
    Vehicle = 4
}
public class LoadingController : MonoBehaviour
{
    private static EScene _targetScene;
    [SerializeField] private float loadingDelayTimer;
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI progressValue;
    
    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }
    
    public static void LoadScene(EScene scene)
    {
        _targetScene = scene;
        SceneManager.LoadScene((int)EScene.Loading, LoadSceneMode.Additive);
    }
    
    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync((int)_targetScene);
        operation.allowSceneActivation = false;
        
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
                break;

            UpdateProgressValue(operation.progress);
            UpdateProgressBar(operation.progress);

            yield return null;
        }
        
        UpdateProgressValue(operation.progress);
        UpdateProgressBar(operation.progress);
        
        yield return new WaitForSeconds(loadingDelayTimer); //Delay is demanded by test assignment and confirmed in hh.ru messaging

        operation.allowSceneActivation = true;
    }

    private void UpdateProgressValue(float value)
    {
        progressValue.text = value * 100 + "%";
    }

    private void UpdateProgressBar(float value)
    {
        progressBar.fillAmount = value;
    }
}
