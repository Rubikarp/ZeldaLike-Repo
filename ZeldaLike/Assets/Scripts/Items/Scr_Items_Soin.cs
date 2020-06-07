using UnityEngine;

namespace Game
{
    public class Scr_Items_Soin : MonoBehaviour
    {
        public int _regenerationCapacity = 1;

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

                if (playerLife.life < playerLife.maxlife)
                {
                    playerLife.life += _regenerationCapacity;
                    sound.PlaySound("Coeur ramassé");
                }
            }
        }
    }
}
