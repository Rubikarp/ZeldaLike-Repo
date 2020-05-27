using UnityEngine;

namespace Game
{
    public class Scr_Items_BoostHP : MonoBehaviour
    {
        public Scr_PlayerLifeSystem _playerLife;
        private SoundManager sound; //Le son

        void Awake()
        {
            sound = SoundManager.Instance;
        }

        private void Start()
        {
            _playerLife = GameObject.FindGameObjectWithTag("Player/HurtBox").GetComponent<Scr_PlayerLifeSystem>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player/HurtBox"))
            {
                Destroy(this.gameObject);

                if(_playerLife._maxlife < 9)
                {
                    _playerLife._maxlife = _playerLife._maxlife + 1;
                    _playerLife._life = _playerLife._maxlife;
                    sound.PlaySound("Amélioration Vie");
                }
            }
        }
    }
}
