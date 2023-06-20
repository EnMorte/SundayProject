using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    public EScene targetScene;
    private Button _button;
    
    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadScene);
    }

    public void LoadScene()
    {
        LoadingController.LoadScene(targetScene);
    }
}
