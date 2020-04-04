using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

namespace Game
{
    public class MainMenu_Handler : MonoBehaviour
    {
        [SerializeField] private string _StartingScene = "Scn_Game_Exterieur";

        public void StartGame()
        {
            SceneManager.LoadScene(_StartingScene);
        }

        public void QuitGame()
        {
            Debug.LogError("Vous venez de quitter le jeu");
            Application.Quit();
        }

    }
}