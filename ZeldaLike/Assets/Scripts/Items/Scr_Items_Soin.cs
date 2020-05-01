using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Game
{
    public class Scr_Items_Soin : MonoBehaviour
    {
        public GameObject _healthItems;
        public int _regenerationCapacity = 1;

        private void Start()
        {
            _healthItems = this.gameObject;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player/HurtBox"))
            {
                if (collision.gameObject.GetComponent<Scr_PlayerLifeSystem>()._life < collision.gameObject.GetComponent<Scr_PlayerLifeSystem>()._maxlife)
                {
                    collision.gameObject.GetComponent<Scr_PlayerLifeSystem>()._life += _regenerationCapacity;
                }
                Destroy(_healthItems);
            }
        }
    }
}
