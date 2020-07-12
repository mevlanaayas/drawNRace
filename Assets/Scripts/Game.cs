using UnityEngine;
using UnityEngine.UI;

namespace DrawRace
{
    public class Game : MonoBehaviour
    {

        public Text finishText;
        
        private bool _state;

        public void HumanWon()
        {
            if (_state)
            {
                return;
            }
            _state = true;
            finishText.text = "Human Wins";
        }
        
        public void AiWon()
        {
            if (_state)
            {
                return;
            }
            _state = true;
            finishText.text = "AI Wins";
        }
    }
}