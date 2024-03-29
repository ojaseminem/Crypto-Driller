using Gameplay.Misc;
using Gameplay.Player;
using ScriptBoy.Digable2DTerrain;
using UnityEngine;

namespace Gameplay.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager instance;
        private void Awake() => instance = this;

        #endregion

        #region Variables

        [SerializeField] private CameraHandler camHandler;
        [SerializeField] private PlayerController pc;

        [SerializeField] private GameObject tutorialMenu;

        [SerializeField] private KillMachine killMachine;
        
        #endregion

        private void Start() 
        {
            Time.timeScale = 1f;
            ChangeState(GameState.Beginning);
        }

        public void ChangeState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Beginning:
                    Beginning();
                    break;
                case GameState.Tutorial:
                    Tutorial();
                    break;
                case GameState.Gameplay:
                    Gameplay();
                    break;
                case GameState.GameOver:
                    GameOver();
                    break;
            }
        }

        private void Beginning()
        {
            SaveLoadManager.LoadGame();
            TimeCalculator.instance.BeginTimer();

            var rand = Random.Range(0, 2);
            switch (rand)
            {
                case 0:
                    AudioManager.instance.PlaySound("Menu_BG_1");
                    break;
                case 1:
                    AudioManager.instance.PlaySound("Menu_BG_2");
                    break;
            }
        }

        private void Tutorial()
        {
            if(SaveLoadManager.CurrentSaveData.tutorialCompleted) ChangeState(GameState.Gameplay);
            else
            {
                Time.timeScale = 0f;
                tutorialMenu.SetActive(true);
            }
        }

        private void Gameplay()
        {
            var rand = Random.Range(0, 3);
            switch (rand)
            {
                case 0:
                    AudioManager.instance.PlaySound("Game_BG");
                    break;
                case 1:
                    AudioManager.instance.PlaySound("Game_BG_2");
                    break;
                case 2:
                    AudioManager.instance.PlaySound("Game_BG_3");
                    break;
            }

            camHandler.canMove = true;
            AudioManager.instance.PlaySound("Drill");
            pc.canMove = true;
            pc.CanTouchMove();
            killMachine.GameBegan();
        }

        private void GameOver()
        {
            killMachine.GameOver();
            TimeCalculator.instance.EndTimer();
            GameMenuManager.instance.GameOver();
            SaveGame();
        }

        private void SaveGame()
        {
            SaveLoadManager.CurrentSaveData.coinCount += GameMenuManager.coins;
            SaveLoadManager.CurrentSaveData.diamondCount += GameMenuManager.diamonds;
            var elapsedTime = TimeCalculator.instance.elapsedTime;
            SaveLoadManager.CurrentSaveData.timePlayed += elapsedTime;
            SaveLoadManager.SaveGame();
        }
    }

    public enum GameState
    {
        Beginning,
        Tutorial,
        Gameplay,
        GameOver
    }
}
