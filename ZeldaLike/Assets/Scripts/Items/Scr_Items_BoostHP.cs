using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Game
{
    public class Scr_Items_BoostHP : MonoBehaviour
    {
        public GameObject _boostItems;
        public Scr_PlayerLifeSystem _playerLife;

        private void Start()
        {
            _boostItems = this.gameObject;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.transform.parent.parent.CompareTag("Player") && _playerLife._maxlife <= 9)
            {
                _playerLife._maxlife = _playerLife._maxlife + 1;
                Destroy(_boostItems);
            }
        }
    }
}
