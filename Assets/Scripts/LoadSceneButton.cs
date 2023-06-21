using UnityEngine;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private EScene targetScene;
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
