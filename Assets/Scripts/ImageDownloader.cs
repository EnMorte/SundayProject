using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ImageDownloader : MonoBehaviour
{
    [SerializeField] private GameObject _imagePrefab;
    private Sprite _image;
    private int _imageIndex = 1;
    private string _directoryURL = "http://data.ikppbb.com/test-task-unity-data/pics/";
    private string _fileExtension = ".jpg";
    private Coroutine _generateImages;
    private bool _continueGenerating;
    
    private void Start()
    {
        _generateImages = StartCoroutine(GenerateImages());
    }

    private IEnumerator GenerateImages()
    {
        _continueGenerating = true;
        
        while (_continueGenerating)
        {
            string imageURL = CreateImageURL(_imageIndex);
            yield return StartCoroutine(DownloadImage(imageURL));
            
            CreateImagePrefab();
            _imageIndex++;
            
            
        }
    }

    private GameObject CreateImagePrefab()
    {
        GameObject newImage = Instantiate(_imagePrefab, transform);
        newImage.GetComponent<ImageConstructor>().SetImage(_image);
        return newImage;
    }

    private string CreateImageURL(int imageIndex)
    {
        string imageURL = _directoryURL + imageIndex + _fileExtension;
        return imageURL;
    }
    
    private IEnumerator DownloadImage(string imageURL)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            SaveImage(((DownloadHandlerTexture)request.downloadHandler).texture);
        else
            StopImageGeneration(request);
    }

    private void SaveImage(Texture2D texture)
    {
        var rect = new Rect(0, 0, texture.width, texture.height);
        var pivot = new Vector2(0.5f, 0.5f);
        _image = Sprite.Create(texture, rect, pivot);
    }

    private void StopImageGeneration(UnityWebRequest request)
    {
        Debug.Log(request.error);
        StopCoroutine(_generateImages);
        _continueGenerating = false;
    }
}
