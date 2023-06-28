using UnityEngine;

namespace Vehicle
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private WheelCollider frontLeftCollider, frontRightCollider, rearLeftCollider, rearRightCollider;
        [SerializeField] private Transform frontLeftTransform, frontRightTransform, rearLeftTransform, rearRightTransform;
        [SerializeField] private float accelerationForce;
        [SerializeField] private float reverseForce;
        private float _currentTorque;
    
        [SerializeField] private float brakingForce;
        private float _currentBraking;
    
        [SerializeField] private float maxTurningAngle = 30f;
        [SerializeField] private float steeringSpeed = 1f;
        [SerializeField] private float alignmentSpeed = 1f;
        private float _steerAnglePerSecond;
    
        private Rigidbody _rb;
        [SerializeField] private float centerOfMassY;

        [HideInInspector] public bool inputForward, inputBackward, inputLeft, inputRight;

        private void Start()
        {
            Init();
            UpdateCenterOfMassY();
        }

        private void Init()
        {
            _rb = GetComponent<Rigidbody>();
            _steerAnglePerSecond = maxTurningAngle / steeringSpeed;
        }

        private void UpdateCenterOfMassY()
        {
            Vector3 rbCom = _rb.centerOfMass;
            _rb.centerOfMass = new Vector3(rbCom.x, centerOfMassY, rbCom.z);
        }

        private void HandleInput()
        {
            if (inputForward && !inputBackward)
                Throttle();
            else if (!inputForward && inputBackward)
                ReverseOrBreak();
            else 
                ReleasePedals();
        
            if (inputLeft && !inputRight)
                SteerLeft();
            else if (!inputLeft && inputRight)
                SteerRight();
            else
                AlignSteering();
        }

        private void Throttle()
        {
            _currentTorque = accelerationForce;
        }
    
        private void ReverseOrBreak()
        {
            Vector3 velocity = _rb.velocity;
            float forwardSpeed = Vector3.Dot(velocity, transform.forward);

            switch (forwardSpeed)
            {
                case > 0:
                    Brake();
                    break;
                case <= 0:
                    Reverse();
                    break;
            }
        }

        private void Brake()
        {
            _currentTorque = 0f;
            _currentBraking = brakingForce;
        }
    
        private void Reverse()
        {
            _currentTorque = -reverseForce;
            _currentBraking = 0f;
        }

        private void ReleasePedals()
        {
            _currentBraking = 0f;
            _currentTorque = 0f;
        }

        private void FixedUpdate()
        {
            UpdateTorque();
            UpdateBrake();
            HandleInput();
        
            UpdateVisuals(frontLeftCollider, frontLeftTransform);
            UpdateVisuals(frontRightCollider, frontRightTransform);
            UpdateVisuals(rearLeftCollider, rearLeftTransform);
            UpdateVisuals(rearRightCollider, rearRightTransform);
        }

        private void UpdateTorque()
        {
            rearLeftCollider.motorTorque = _currentTorque;
            rearRightCollider.motorTorque = _currentTorque;
        }
    
        private void UpdateBrake()
        {
            frontLeftCollider.brakeTorque = _currentBraking;
            frontRightCollider.brakeTorque = _currentBraking;
            rearLeftCollider.brakeTorque = _currentBraking;
            rearRightCollider.brakeTorque = _currentBraking;
        }

        private void AlignSteering()
        {
            float t = alignmentSpeed * Time.deltaTime;
            
            if (t > 1)
                return;
            
            frontLeftCollider.steerAngle = Mathf.Lerp(frontLeftCollider.steerAngle, 0, t);
            frontRightCollider.steerAngle = Mathf.Lerp(frontRightCollider.steerAngle, 0, t);
        }

        private void SteerLeft()
        {
            if (frontLeftCollider.steerAngle > -maxTurningAngle)
                frontLeftCollider.steerAngle -= _steerAnglePerSecond * Time.deltaTime;
        
            if (frontRightCollider.steerAngle > -maxTurningAngle)
                frontRightCollider.steerAngle -= _steerAnglePerSecond * Time.deltaTime;
        }
    
        private void SteerRight()
        {
            if (frontLeftCollider.steerAngle < maxTurningAngle)
                frontLeftCollider.steerAngle += _steerAnglePerSecond * Time.deltaTime;
        
            if (frontRightCollider.steerAngle < maxTurningAngle)
                frontRightCollider.steerAngle += _steerAnglePerSecond * Time.deltaTime;
        }

        private void UpdateVisuals(WheelCollider wheel, Transform visual)
        {
            wheel.GetWorldPose(out Vector3 position, out Quaternion rotation);

            visual.position = position;
            visual.rotation = rotation;
        }
    }
}
