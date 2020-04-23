using Management;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class DeathMenue_Handler : MonoBehaviour
    {
        [SerializeField] private string _MainMenuScene = "Scn_Game_Exterieur";
        [SerializeField] private GameObject _DeathCanvas = null;

        [SerializeField] private Scr_PlayerLifeSystem lifeSyst = null;
        private InputManager _input = null;

        private void Start()
        {
            _input = InputManager.Instance;
        }

        private void Update()
        {
            if (lifeSyst.isDead)
            {
                new WaitForSeconds(1f);
                StopGame();
            }
        }

        public void StopGame()
        {
            Time.timeScale = 0;
            _input.DesactivateControl();
            _DeathCanvas.SetActive(true);
        }

        public void BackToGame()
        {
            Time.timeScale = 1;
            _input.ReActivateControl();
            _DeathCanvas.SetActive(false);
        }

        public void BackToMainMenue()
        {
            SceneManager.LoadScene(_MainMenuScene);
        }

        public void QuitGame()
        {
            Debug.LogError("Vous venez de quitter le jeu");
            Application.Quit();
        }

    }
}