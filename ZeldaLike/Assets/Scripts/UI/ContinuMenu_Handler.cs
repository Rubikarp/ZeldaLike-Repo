using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class ContinuMenu_Handler : MonoBehaviour
    {
        [SerializeField] private string _BeginingScene = "Scn_Game_Exterieur";
        [SerializeField] private string _DongeonE1Scene = "Scn_Game_DongeonE1";
        [SerializeField] private string _DongeonE2Scene = "Scn_Game_DongeonE2";

        public void GoTO_Beginning()
        {
            SceneManager.LoadScene(_BeginingScene);
        }

        public void GoTO_DongeonE1()
        {
            SceneManager.LoadScene(_DongeonE1Scene);
        }

        public void GoTO_DongeonE2()
        {
            SceneManager.LoadScene(_DongeonE2Scene);
        }
    }
}