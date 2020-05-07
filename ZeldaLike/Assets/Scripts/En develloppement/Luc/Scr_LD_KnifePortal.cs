using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scr_LD_KnifePortal : MonoBehaviour
    {
        public GameObject _otherPortal;
        public bool _canTP;
        public float _TPDelay;
        private float _TPDelayActive;

        // Start is called before the first frame update
        void Start()
        {
            _canTP = true;
            _TPDelayActive = _TPDelay;
        }

        // Update is called once per frame
        void Update()
        {
            if (_canTP == false)
            {
                if (_TPDelayActive > 0)
                {
                    _TPDelayActive -= Time.deltaTime;
                }
                else if (_TPDelayActive <= 0)
                {
                    _canTP = true;
                    _TPDelayActive = _TPDelay;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Knife") && _canTP == true)
            {
                _otherPortal.GetComponent<Scr_LD_KnifePortal>()._canTP = false;
                collision.gameObject.GetComponent<KnifeBehaviour>()._playerOrientation = -(collision.gameObject.GetComponent<KnifeBehaviour>()._playerOrientation);
                collision.gameObject.GetComponent<KnifeBehaviour>().FaceShootingDirection(collision.gameObject.GetComponent<KnifeBehaviour>()._playerOrientation);
                collision.transform.position = _otherPortal.transform.position;
            }
        }
    }
}

