using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class ContinuMenu_Handler : MonoBehaviour
    {
        public void GoTO(string Scene)
        {
            SceneManager.LoadScene(Scene);
        }
    }
}