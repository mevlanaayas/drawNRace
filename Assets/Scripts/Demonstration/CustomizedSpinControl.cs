using UnityEngine;

namespace DrawRace
{
    public class CustomizedSpinControl : MonoBehaviour
    {
        public GameObject wheelObject;
        private Rigidbody _wheelObjectRigidbody;
        public float speed = 100f;

        private void Awake()
        {
            _wheelObjectRigidbody = wheelObject.GetComponent<Rigidbody>();
        }

        public void Update()
        {
            _wheelObjectRigidbody.AddTorque(new Vector3(0, 0, speed));
            ApplyLocalPositionToVisuals();
        }

        private void ApplyLocalPositionToVisuals()
        {
            // wheelObject.GetWorldPose(out var position, out var rotation);

            // _visualWheel.position = position;
            // _visualWheel.rotation = rotation;
        }
    }
}