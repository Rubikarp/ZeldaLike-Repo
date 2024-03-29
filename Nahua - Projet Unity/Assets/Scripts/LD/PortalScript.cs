﻿using System.Collections;
using UnityEngine;

namespace Game
{
    public class PortalScript : MonoBehaviour
    {
        public GameObject _otherPortal = null;
        public GameObject Player = null;
        public bool _canTP = true;
        public bool _canTPBlocks;
        public float _TPDelay = 1f;
        public float _TPBlocksDelay;
        public bool _isBig;
        private SoundManager sound; //Le son

        void Start()
        {
            _canTP = true;
            _canTPBlocks = true;
            _TPDelay = _otherPortal.GetComponent<PortalScript>()._TPDelay;
            Player = GameObject.Find("Avatar");
        }
        void Awake()
        {
            sound = SoundManager.Instance;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && _canTP == true)
            {
                sound.PlaySound("Entrée Portail");
                Player.transform.position = _otherPortal.transform.position;
                sound.PlaySound("Sortie Portail");
                _otherPortal.GetComponent<PortalScript>()._canTP = false;
                StartCoroutine(OtherPortalLockPlayer());
            }
            else if (collision.gameObject.CompareTag("Environment") && _canTPBlocks == true)
            {
                if (collision.gameObject.GetComponent<Scr_HeavyMovable>()._isBig == false)
                {
                    collision.gameObject.transform.position = _otherPortal.transform.position;
                    _otherPortal.GetComponent<PortalScript>()._canTPBlocks = false;
                    StartCoroutine(OtherPortalLockBlocks());
                }

                else if (collision.gameObject.GetComponent<Scr_HeavyMovable>()._isBig == true && _isBig == true)
                {
                    collision.gameObject.transform.position = _otherPortal.transform.position;
                    _otherPortal.GetComponent<PortalScript>()._canTPBlocks = false;
                    StartCoroutine(OtherPortalLockBlocks());
                }
            }

        }

        IEnumerator OtherPortalLockPlayer()
        {
            yield return new WaitForSeconds(_TPDelay);
            _otherPortal.GetComponent<PortalScript>()._canTP = true;
        }

        IEnumerator OtherPortalLockBlocks()
        {
            yield return new WaitForSeconds(_TPBlocksDelay);
            _otherPortal.GetComponent<PortalScript>()._canTPBlocks = true;
        }
    }
}

