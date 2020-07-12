using UnityEngine;

namespace DrawRace
{
    public class HumanRacer : MonoBehaviour
    {
        private Vector3 _position;

        private void Start()
        {
            _position = transform.position;
        }

        private void FixedUpdate()
        {
            _position = transform.position;
        }

        public Vector3 Position()
        {
            return _position;
        }
    }
}