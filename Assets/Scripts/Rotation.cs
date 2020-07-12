using System;
using UnityEngine;

namespace DrawRace
{
    public class Rotation : MonoBehaviour
    {
        public float speed = 100f;
        private Rigidbody _rigidbody;


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _rigidbody.AddTorque(new Vector3(0, speed, 0));
        }
    }
}