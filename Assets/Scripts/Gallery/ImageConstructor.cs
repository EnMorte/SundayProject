using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageConstructor : MonoBehaviour
{
    private Sprite _imageSprite;
    private Image _image;

    private RectTransform _rectTransform;
    private RectTransform _scrollViewRectTransform;
    private Vector2 _relativePosition;
    private bool _imageSet;

    private UnityWebRequest _request;
    private Coroutine _imageDownloading;

    private GameObject _spinner;

    public UnityEvent<GameObject> onImageSet;

    private void Start()
    {
        Init();
    }
    
    private void Init()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _scrollViewRectTransform = FindObjectOfType<ScrollRect>().gameObject.GetComponent<RectTransform>();
        _spinner = GetComponentInChildren<SpinnerRotation>().gameObject;
        onImageSet ??= new UnityEvent<GameObject>();
    }

    public void SaveRequest(UnityWebRequest request)
    {
        _request = request;
    }

    private void Update()
    {
        if (_imageSet)
            return;
        
        CalculateRelativePosition();

        if (IsOutsideView(_relativePosition))
            return;
        
        _imageDownloading ??= StartCoroutine(DownloadImage());
    }

    private void CalculateRelativePosition()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _scrollViewRectTransform, _rectTransform.position, null, out _relativePosition);
    }

    private bool IsOutsideView(Vector2 localPoint)
    {
        return !_scrollViewRectTransform.rect.Contains(localPoint);
    }
    
    private IEnumerator DownloadImage()
    {
        yield return _request.SendWebRequest();
        
        if (_request.result == UnityWebRequest.Result.Success)
            SaveImage(((DownloadHandlerTexture)_request.downloadHandler).texture);
        else
            StopAndReport(_request);
        
        SetImage(_imageSprite);
        onImageSet.Invoke(gameObject);
    }
    
    private void SetImage(Sprite image)
    {
        _image.sprite = image;
        _image.color = new Color(255, 255, 255, 255);
        DisableSpinner();
        _imageSet = true;
    }

    private void DisableSpinner()
    {
        _spinner.SetActive(false);
    }
    
    private void SaveImage(Texture2D texture)
    {
        var rect = new Rect(0, 0, texture.width, texture.height);
        var pivot = new Vector2(0.5f, 0.5f);
        _imageSprite = Sprite.Create(texture, rect, pivot);
    }

    private void StopAndReport(UnityWebRequest request)
    {
        Debug.Log(request.error);
        StopCoroutine(_imageDownloading);
    }
}
