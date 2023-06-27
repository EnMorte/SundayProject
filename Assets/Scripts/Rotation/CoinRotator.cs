using UnityEngine;

public class CoinRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    
    private void Update()
    {
        transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
    }
}
