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
        _imageComponent.color = new Color(255, 255, 255, 255);
        DisableSpinner();
    }

    private void DisableSpinner()
    {
        GetComponentInChildren<SpinnerRotation>().gameObject.SetActive(false);
    }
}
