using UnityEngine;

namespace Rotation
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private AndroidEventsEmitter androidEventsEmitter;
    
        private void Start()
        {
            InitAndroid();
        }

        private void InitAndroid()
        {
            androidEventsEmitter.InitEvents();
            androidEventsEmitter.androidEscape.AddListener(HandleAndroidEscape);
        }

        private void HandleAndroidEscape()
        {
            LoadingController.LoadScene(EScene.MainMenu);
        }
    }
}
