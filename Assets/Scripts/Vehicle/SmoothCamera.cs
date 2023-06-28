using UnityEngine;

namespace Vehicle
{
    public class SmoothCamera : MonoBehaviour
    {
        [SerializeField] private GameObject car;
        [SerializeField] private GameObject cameraRig;
        [SerializeField] private float smoothSpeed;
        [SerializeField] private float rotationSpeed;
    
        private Vector3 _cameraRigPosition;
        private Vector3 _carPosition;
    
        private void Start()
        {
            SetupRigPosition();
        }

        private void SetupRigPosition()
        {
            cameraRig.transform.position = transform.position;
        }

        private void FixedUpdate()
        {
            FollowRigPosition();
            LookAtCar();
        }

        private void FollowRigPosition()
        {
            transform.position = Vector3.Lerp(
                transform.position, 
                cameraRig.transform.position, 
                smoothSpeed * Time.deltaTime);
        }

        private void LookAtCar()
        {
            Quaternion lookAtCarRotation = Quaternion.LookRotation(car.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(
                transform.rotation, 
                lookAtCarRotation, 
                rotationSpeed * Time.deltaTime);
        }
    }
}
