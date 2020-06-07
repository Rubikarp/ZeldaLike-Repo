using UnityEngine;

namespace Game
{
    public class Scr_Items_BoostHP : MonoBehaviour
    {
        public PlayerLife playerLife;
        private SoundManager sound; //Le son

        void Awake()
        {
            sound = SoundManager.Instance;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player/HurtBox"))
            {
                Destroy(this.gameObject);

                if(playerLife.maxlife < 9)
                {
                    playerLife.maxlife = playerLife.maxlife + 1;
                    playerLife.life = playerLife.maxlife;
                    sound.PlaySound("Amélioration Vie");
                }
            }
        }
    }
}
