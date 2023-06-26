using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ViewImageButton : MonoBehaviour
{
    public UnityEvent<Image> onImageClick;
    private Button _button;
    private Image _image;
    

    public void Init()
    {
        ActivateButton();
        SaveImage();
        CreateClickEvent();
    }

    private void ActivateButton()
    {
        _button = GetComponent<Button>();
        _button.interactable = true;
    }

    private void CreateClickEvent()
    {
        _button.onClick.AddListener(ViewImage);
        onImageClick ??= new UnityEvent<Image>();
    }

    private void SaveImage()
    {
        _image = GetComponent<Image>();
    }

    private void ViewImage()
    {
        onImageClick.Invoke(_image);
    }
}
