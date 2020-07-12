using System;
using UnityEngine;
using UnityEngine.UI;

namespace DrawRace
{
    public class World : MonoBehaviour
    {

        public Text raceStatus;
        public Text minimap;

        private HumanRacer _humanRacer;
        private AiRacer _aiRacer;

        private void Start()
        {
            _humanRacer = FindObjectOfType<HumanRacer>();
            _aiRacer = FindObjectOfType<AiRacer>();
        }

        private void Update()
        {
            if (_humanRacer.Position().x >= _aiRacer.Position().x)
            {
                raceStatus.text = "1";
            }
            else
            {
                raceStatus.text = "2";
            }
        }
    }
}