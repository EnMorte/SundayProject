using UnityEngine;

public class SpinnerRotation : MonoBehaviour
{
    [SerializeField] private float spinnerRotationSpeed;
    private Transform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        RotateSpinner();
    }

    private void RotateSpinner()
    {
        _rectTransform.Rotate(Vector3.forward * (spinnerRotationSpeed * Time.deltaTime));
    }
}
