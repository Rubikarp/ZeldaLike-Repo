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
                collision.gameObject.GetComponent<Scr_PlayerLifeSystem>().MaxHeal();

                Destroy(this.gameObject);
            }
        }
    }
}
