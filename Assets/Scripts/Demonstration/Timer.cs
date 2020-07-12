using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace DrawRace
{
    public class Timer : MonoBehaviour
    {
        public Text timerText;

        private Stopwatch _stopwatch;

        private void Start()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        private void Update()
        {
            var ts = _stopwatch.Elapsed;

            var elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            timerText.text = elapsedTime;
        }
    }
}