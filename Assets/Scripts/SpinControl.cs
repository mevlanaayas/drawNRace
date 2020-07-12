using UnityEngine;

namespace DrawRace
{
    public class SpinControl : MonoBehaviour
    {
        public WheelCollider wheel;
        public float motorTorque = 0.05f;

        private Transform _visualWheel;
        // public Vector3 velocity;
        // private Rigidbody _rigidbody;

        private void Awake()
        {
            if (wheel.transform.childCount != 0)
            {
                Debug.LogError("No visual found for wheel.");
            }

            if (wheel.transform.childCount == 0)
            {
                Debug.LogError("No visual found for wheel.");
            }

            _visualWheel = wheel.transform.GetChild(0).transform;
            // _rigidbody = GetComponent<Rigidbody>();
        }

        public void FixedUpdate()
        {
            wheel.motorTorque = motorTorque;
            ApplyLocalPositionToVisuals();
            // _rigidbody.velocity = velocity;
        }

        private void ApplyLocalPositionToVisuals()
        {
            wheel.GetWorldPose(out var position, out var rotation);

            position.z = _visualWheel.position.z;
            _visualWheel.position = position;
            _visualWheel.rotation = rotation;
        }
    }
}