using UnityEngine;

namespace DrawRace
{
    public class FinishLine : MonoBehaviour
    {
        
        private Game _game;

        private void Start()
        {
            _game = FindObjectOfType<Game>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("AI"))
            {
                _game.AiWon();
            }
            else if (other.CompareTag("Human"))
            {
                _game.HumanWon();
            }
        }
    }
}