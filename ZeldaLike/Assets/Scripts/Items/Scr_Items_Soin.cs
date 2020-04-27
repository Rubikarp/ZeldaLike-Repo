using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Game
{
    public class Scr_Items_Soin : MonoBehaviour
    {
        public GameObject _healthItems;
        public Scr_PlayerLifeSystem _playerLife;
        public int _regenerationCapacity = 1;

        private void Start()
        {
            _healthItems = this.gameObject;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.transform.parent.parent.CompareTag("Player") && _playerLife._life < _playerLife._maxlife)
            {
                _playerLife._life = _playerLife._life + _regenerationCapacity;
                Destroy(_healthItems);
            }
            else if(collision.gameObject.transform.parent.parent.CompareTag("Player") && _playerLife._life == 6)
            {
                Destroy(_healthItems);
            }
        }
    }
}
