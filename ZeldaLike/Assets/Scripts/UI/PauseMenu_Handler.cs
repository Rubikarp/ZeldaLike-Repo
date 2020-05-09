using Management;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class PauseMenu_Handler : MonoBehaviour
    {
        [SerializeField] private string _MainMenuScene = "Scn_Game_Exterieur";
        [SerializeField] private GameObject _PauseCanvas = null;
        private InputManager _input = null;
        private bool _inPause = false;

        private void Start()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
        }

        private void Update()
        {
            if (_input._pause)
            {
                if (_inPause)
                {
                    _inPause = false;
                    BackToGame();
                }
                else
                {
                    _inPause = true;
                    StopGame();
                }
            }
        }

        public void StopGame()
        {
            Time.timeScale = 0;
            _input.DesactivateControl();
            _PauseCanvas.SetActive(true);
        }

        public void BackToGame()
        {
            Time.timeScale = 1;
            _input.ReActivateControl();
            _PauseCanvas.SetActive(false);
        }

        public void BackToMainMenue()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(_MainMenuScene);
        }

        public void QuitGame()
        {
            Debug.LogError("Vous venez de quitter le jeu");
            Application.Quit();
        }
    }
}