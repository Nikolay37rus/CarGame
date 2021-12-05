using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] 
        private Button _buttonStart;

        [SerializeField]
        private Button _buttonDailyReward;

        [SerializeField]
        private Button _buttonExitGame;

        public void Init(UnityAction startGame, UnityAction dailyReward)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonDailyReward.onClick.AddListener(dailyReward);
            _buttonExitGame.onClick.AddListener(ExitGame);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        protected void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonDailyReward.onClick.RemoveAllListeners();
            _buttonExitGame.onClick.RemoveAllListeners();
        }
    }
}

