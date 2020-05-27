using UnityEngine;

namespace Game
{
    public class Scr_Items_Soin : MonoBehaviour
    {
        public int _regenerationCapacity = 1;
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

                if (_playerLife._life < _playerLife._maxlife)
                {
                    _playerLife._life += _regenerationCapacity;
                    sound.PlaySound("Coeur ramassé");
                }
            }
        }
    }
}
