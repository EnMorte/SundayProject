using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ImageDownloader : MonoBehaviour
{
    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private int imagesCount;
    [SerializeField] private string directoryURL = "http://data.ikppbb.com/test-task-unity-data/pics/";
    [SerializeField] private string fileExtension = ".jpg";
    
    private Sprite _image;
    private int _imageIndex = 1;
    private Coroutine _generateImages;
    private Dictionary<GameObject, UnityWebRequest> _imagesAndRequests = new();

    private void Start()
    {
        CreateImagesAndRequests(imagesCount);
        _generateImages = StartCoroutine(SendRequests());
    }
    
    private void CreateImagesAndRequests(int requestsCount)
    {
        for (var i = 0; i < requestsCount; i++)
        {
            string imageURL = CreateImageURL(_imageIndex);
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL);
            _imagesAndRequests.Add(CreateImagePrefab(), request);
            _imageIndex++;
        }
        
        Debug.Log(_imagesAndRequests.Count);
    }

    private IEnumerator SendRequests()
    {
        foreach ((GameObject prefab, UnityWebRequest request) in _imagesAndRequests)
        {
            yield return request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
                SaveImage(((DownloadHandlerTexture)request.downloadHandler).texture);
            else
                StopImageGeneration(request);
            
            SetImage(prefab);
        }
    }

    private string CreateImageURL(int imageIndex)
    {
        string imageURL = directoryURL + imageIndex + fileExtension;
        return imageURL;
    }
    

    private GameObject CreateImagePrefab()
    {
        GameObject newImagePrefab = Instantiate(imagePrefab, transform);
        return newImagePrefab;
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
    }

    private void SetImage(GameObject prefab)
    {
        prefab.GetComponent<ImageConstructor>().SetImage(_image);
    }
}
