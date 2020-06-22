using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class EndOfGame : MonoBehaviour
    {
        [SerializeField] private string _EnteringScene = "Scn_Game_Exterieur";

        private void OnEnable()
        {
            SceneManager.LoadScene(_EnteringScene);

        }
    }
}