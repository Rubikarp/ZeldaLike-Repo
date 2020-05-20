using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemis
{
    public class Scr_BossBulletBehavior : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private GameObject _behavBoss = null;
        public Transform _projectile = null;
        public Rigidbody2D _rb2d = null;

        [Header("Statistiques")]
        public Vector2 _BulletDir = Vector2.zero;
        public float _mySpeed = 10f;
        public float _timer = 1f;

        private void Start()
        {
            _projectile = this.transform;
            _rb2d = this.GetComponent<Rigidbody2D>();
            _behavBoss = GameObject.Find("AvatarBossP1");
            _BulletDir = _behavBoss.GetComponent<Scr_BossPhase1>()._currentTarget.normalized;

            Destroy(this.gameObject, _timer);
        }

        private void Update()
        {
            _rb2d.position += _BulletDir * _mySpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.transform.parent.parent.CompareTag("Player"))
            {  
                Destroy(gameObject);
            }
            else
            {
                //Debug.Log(collision.gameObject.name);
            }
        }

    }
}


