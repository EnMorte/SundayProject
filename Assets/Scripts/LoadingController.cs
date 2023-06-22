using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EScene
{
    MainMenu = 0,
    Loading = 1,
    Gallery = 2,
    Vehicle = 3,
    Rotation = 4,
    Humanoid = 5
}
public class LoadingController : MonoBehaviour
{
    private static EScene _targetScene;
    [SerializeField] private float loadingDelayTimer;
    
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

            yield return null;
        }
        
        yield return new WaitForSeconds(loadingDelayTimer); //Delay is demanded by test assignment and confirmed in hh.ru messaging

        operation.allowSceneActivation = true;
    }
}
