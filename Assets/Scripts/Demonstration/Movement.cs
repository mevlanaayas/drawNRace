using UnityEngine;

namespace DrawRace
{
    public class Movement : MonoBehaviour
    {
        public int speed = 1;

        private void Start()
        {
            var velocity = new Vector3(speed, 0, 0);
            GetComponent<Rigidbody>().velocity = velocity;
        }
    }
}
