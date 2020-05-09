using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]

    public class OnEnterChangingScene : MonoBehaviour
    {
        [SerializeField] private string _EnteringScene = "Scn_Game_Exterieur";

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                SceneManager.LoadScene(_EnteringScene);
            }
        }
    }
}