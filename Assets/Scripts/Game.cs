using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DrawRace
{
    public class Game : MonoBehaviour
    {
        public Text roundFinishText;
        public Text raceFinishText;

        // it constant for mvp
        private int _roundCount = 5;

        // it is bool for mvp: true for human, false for ai
        private List<bool> _roundResults = new List<bool>();

        // it is bool for mvp: true for finished, false for not
        private bool _roundFinished;
        private bool _raceFinished;

        public static Game Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (_raceFinished)
            {
                var humanWonCount = _roundResults.Count(x => x);

                raceFinishText.text = humanWonCount > Mathf.CeilToInt(_roundCount / 2.0f)
                    ? "Human Won"
                    : "AI Won";
                return;
            }

            if (_roundResults.Count == _roundCount)
            {
                _raceFinished = true;
            }
        }

        public void HumanWon()
        {
            if (_roundFinished)
            {
                return;
            }

            _roundFinished = true;
            _roundResults.Add(true);
            roundFinishText.text = "Human Wins the Round. \nLoading Next Round!..";
            StartCoroutine(nameof(WaitAndRestartRound));
        }

        public void AiWon()
        {
            if (_roundFinished)
            {
                return;
            }

            _roundFinished = true;
            _roundResults.Add(false);
            roundFinishText.text = "AI Wins the Round. \nLoading Next Round!..";
            StartCoroutine(nameof(WaitAndRestartRound));
        }

        private IEnumerator WaitAndRestartRound()
        {
            yield return new WaitForSeconds(5);
            _roundFinished = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}