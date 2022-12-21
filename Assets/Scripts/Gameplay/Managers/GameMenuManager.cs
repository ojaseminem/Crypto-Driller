using System;
using Gameplay.Misc;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.Managers
{
    public class GameMenuManager : MonoBehaviour
    {
        #region Singleton

        public static GameMenuManager instance;
        private void Awake() => instance = this;
        
        #endregion

        public static int coins;
        public static int diamonds;
        
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject statsMenu;
        [SerializeField] private GameObject uiMenu;
        [SerializeField] private GameObject gameOverMenu;
        
        //Stats Variables
        [SerializeField] private TextMeshProUGUI coinCount;
        [SerializeField] private TextMeshProUGUI diamondCount;
        [SerializeField] private TextMeshProUGUI timePlayed;

        //UI Variables
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI diamondText;
        
        //Game Over Variables
        [SerializeField] private TextMeshProUGUI coinTextGameOver;
        [SerializeField] private TextMeshProUGUI diamondTextGameOver;
        [SerializeField] private TextMeshProUGUI timePlayedTextGameOver;
        
        public void Play()
        {
            GameManager.instance.ChangeState(GameState.Tutorial);
            coinText.text = coins.ToString();
            diamondText.text = diamonds.ToString();
        }

        public void Stats()
        {
            statsMenu.SetActive(true);
            var saveData = SaveLoadManager.CurrentSaveData;
            coinCount.text = saveData.coinCount.ToString();
            diamondCount.text = saveData.diamondCount.ToString();
            var elapsedTime = SaveLoadManager.CurrentSaveData.timePlayed;
            var timeSpan = TimeSpan.FromSeconds(elapsedTime);
            timePlayed.text = timeSpan.ToString("mm' : 'ss' . 'ff");
        }

        public void Pause()
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }

        public void Resume()
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }

        public void Restart()
        {
            SceneManager.LoadScene("GameplayScene");
        }

        public void Volume(bool toggle)
        {
            AudioListener.volume = toggle ? 1 : 0;
        }

        public void ResetTutorial()
        {
            SaveLoadManager.CurrentSaveData.tutorialCompleted = false;
            SaveLoadManager.SaveGame();
        }

        public void IncrementCoin()
        {
            coins++;
            coinText.text = coins.ToString();
        }

        public void IncrementDiamond()
        {
            diamonds++;
            diamondText.text = diamonds.ToString();
        }
        
        public void TutorialDone()
        {
            Time.timeScale = 1f;
            SaveLoadManager.CurrentSaveData.tutorialCompleted = true;
            SaveLoadManager.SaveGame();
            GameManager.instance.ChangeState(GameState.Tutorial);
            uiMenu.SetActive(true);
        }

        public void GameOver()
        {
            gameOverMenu.SetActive(true);
            coinTextGameOver.text = coins.ToString();
            diamondTextGameOver.text = diamonds.ToString();
            var elapsedTime = TimeCalculator.instance.elapsedTime;
            var timeSpan = TimeSpan.FromSeconds(elapsedTime);
            timePlayedTextGameOver.text = timeSpan.ToString("mm' : 'ss' . 'ff");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}