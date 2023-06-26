using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowsController : MonoBehaviour
{
    [SerializeField] private GameObject _viewerWindow;
    [SerializeField] private FullScreenImage _fullScreenImage;
    [SerializeField] private GameObject _closeButton;
    [SerializeField] private GallerySetup _gallerySetup;
    
    private ImageConstructor _imageConstructor;
    
    
    void Start()
    {
        SetupGallery();
        InitCloseButton();
    }

    private void SetupGallery()
    {
        _gallerySetup.CreateEvent();
        _gallerySetup.onImageCreated.AddListener(WaitImageSet);
        _gallerySetup.CreateImagesAndRequests();
    }

    private void WaitImageSet(GameObject prefab)
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
        EnableViewerWindow();
        var fullscreenImage = _fullScreenImage.GetComponent<Image>();
        
        fullscreenImage.sprite = image.sprite;
        fullscreenImage.color = image.color;
    }
    
    private void EnableViewerWindow()
    {
        _viewerWindow.SetActive(true);
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    private void DisableViewerWindow()
    {
        _viewerWindow.SetActive(false);
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void InitCloseButton()
    {
        _closeButton.GetComponent<Button>().onClick.AddListener(DisableViewerWindow);
    }
}
