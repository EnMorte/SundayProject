using UnityEngine;
using UnityEngine.UI;

public class ImageConstructor : MonoBehaviour
{
    private Sprite _image;
    private Image _imageComponent;

    public void SetImage(Sprite image)
    {
        _imageComponent = GetComponent<Image>();
        _imageComponent.sprite = image;
    }
}
