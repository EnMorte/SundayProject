using UnityEngine;
using UnityEngine.UI;

namespace Gallery
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject viewerWindow, galleryWindow, fullScreenImage, closeButton;
        [SerializeField] private AndroidEventsEmitter androidEventsEmitter;
        [SerializeField] private GallerySetup gallerySetup;
    
        private ImageConstructor _imageConstructor;
    
        private void Start()
        {
            SetupGallery();
            InitCloseButton();
            InitAndroid();
        }

        private void InitAndroid()
        {
            androidEventsEmitter.InitEvents();
            androidEventsEmitter.androidEscape.AddListener(HandleAndroidEscape);
        }

        private void SetupGallery()
        {
            gallerySetup.CreateEvent();
            gallerySetup.onImageCreated.AddListener(WaitImageToSet);
            gallerySetup.CreateImagesAndRequests();
        }

        private void WaitImageToSet(GameObject prefab)
        {
            prefab.GetComponent<ImageConstructor>().onImageSet.AddListener(InitButton);
        }

        private void InitButton(GameObject prefab)
        {
            var imageButton = prefab.GetComponent<ViewImageButton>();
        
            imageButton.Init();
            imageButton.onImageClick.AddListener(ViewImage);
        }

        private void ViewImage(Image image)
        {
            SwitchToImageViewer();
            var fsImage = fullScreenImage.GetComponent<Image>();
        
            fsImage.sprite = image.sprite;
            fsImage.color = image.color;
        }
    
        private void SwitchToImageViewer()
        {
            galleryWindow.SetActive(false);
            viewerWindow.SetActive(true);
            Screen.orientation = ScreenOrientation.AutoRotation;
        }

        private void SwitchToGallery()
        {
            viewerWindow.SetActive(false);
            galleryWindow.SetActive(true);
            Screen.orientation = ScreenOrientation.Portrait;
        }

        private void InitCloseButton()
        {
            closeButton.GetComponent<Button>().onClick.AddListener(SwitchToGallery);
        }

        private void HandleAndroidEscape()
        {
            if(galleryWindow.activeInHierarchy)
                LoadingController.LoadScene(EScene.MainMenu);
            
            if (viewerWindow.activeInHierarchy)
                SwitchToGallery();
        }
    }
}
