using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Ennemies
{
    public class Scr_BossGrenadeBehavior : MonoBehaviour
    {
        private Vector3 _grenadeTarget;
        public float _grenadeSpeed;
        public Transform _mySelf;
        private bool _targetAttained;
        public float _explosionRange;
        public float _knockBackSpeed;
        public float _stunDuration;
        public float _explosionDelay;

        // Start is called before the first frame update
        void Start()
        {
            _grenadeTarget = FindObjectOfType<Scr_BossPhase1>().gameObject.GetComponent<Scr_BossPhase1>()._grenadeTarget;
            _targetAttained = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (_targetAttained == false)
            {
                _mySelf.position = _grenadeTarget * _grenadeSpeed * Time.deltaTime;

                if (_mySelf.position == _grenadeTarget)
                {
                    _targetAttained = true;
                }
            }
            else if (_targetAttained == true)
            {
                while(_explosionDelay > 0)
                {
                    _explosionDelay -= Time.deltaTime;
                    new WaitForEndOfFrame();
                }

                Collider2D[] playerToHit = Physics2D.OverlapCircleAll(_mySelf.position, _explosionRange);

                for (int i = 0; i < playerToHit.Length; i++)
                {
                    if (playerToHit[i].gameObject.transform.parent.parent.CompareTag("Player"))
                    {
                        Vector2 _knockBackDirection = playerToHit[i].gameObject.transform.parent.parent.position - _mySelf.position;
                        playerToHit[i].gameObject.transform.parent.GetComponentInChildren<Scr_PlayerLifeSystem>().TakingDamage(1, playerToHit[i].gameObject.transform.parent.parent.GetComponent<Rigidbody2D>(), _knockBackDirection, _knockBackSpeed, _stunDuration);
                    }
                }

                Destroy(gameObject);
            }


        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_mySelf.position, _explosionRange);
        }

    }

}
