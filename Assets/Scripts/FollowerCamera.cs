using UnityEngine;

namespace DrawRace
{
    public class FollowerCamera : MonoBehaviour
    {
        
        public GameObject target;

        public float smoothSpeed = 0.125f;
        public Vector3 offset;

        private void Start()
        {
            offset = transform.position;
        }
        public void FixedUpdate ()
        {
            var desiredPosition = target.transform.position + offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target.transform);
        }
    }
}
