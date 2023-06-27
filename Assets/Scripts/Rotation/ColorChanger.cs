using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Camera _mainCamera;
    private Material _material;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        MouseInput();
        TouchInput();
    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
            DetectObject(Input.mousePosition);
    }

    private void TouchInput()
    {
        if (Input.touchCount <= 0)
            return;
        
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
            DetectObject(touch.position);
    }

    private void DetectObject(Vector3 position)
    {
        Ray ray = _mainCamera.ScreenPointToRay(position);

        if (!Physics.Raycast(ray, out RaycastHit hit))
            return;

        if (hit.collider.gameObject == gameObject)
            ChangeColor();
    }

    private void ChangeColor()
    {
        Color randomColor = Random.ColorHSV();
        _material.color = randomColor;
    }
}
