using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class GallerySetup : MonoBehaviour
{
    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private int imagesCount;
    [SerializeField] private string directoryURL = "http://data.ikppbb.com/test-task-unity-data/pics/";
    [SerializeField] private string fileExtension = ".jpg";
    
    private int _imageIndex = 1;

    private ViewImageButton _viewImageButton;
    public UnityEvent<GameObject> onImageCreated;

    public void CreateEvent()
    {
        onImageCreated ??= new UnityEvent<GameObject>();
    }

    public void CreateImagesAndRequests()
    {
        for (var i = 0; i < imagesCount; i++)
        {
            string imageURL = CreateImageURL(_imageIndex);
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL);
            GameObject prefab = CreateImagePrefab();
            
            SaveRequest(prefab, request);
            onImageCreated.Invoke(prefab);
            
            _imageIndex++;
        }
    }

    private void SaveRequest(GameObject prefab, UnityWebRequest request)
    {
        prefab.GetComponent<ImageConstructor>().SaveRequest(request);
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
}
