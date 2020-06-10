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
        private float _rotZ;
        public Animator _anim;
        private bool _isDying;

        [Header("Statistiques")]
        public Vector2 _BulletDir = Vector2.zero;
        public float _mySpeed = 10f;
        public float _timer = 1f;

        private void Start()
        {
            _isDying = false;
            _projectile = this.transform;
            _rb2d = this.GetComponent<Rigidbody2D>();
            _behavBoss = GameObject.Find("AvatarBossP1");
            _BulletDir = _behavBoss.GetComponent<Scr_BossPhase1>()._currentTarget.normalized;
            _rotZ = Mathf.Atan2(_BulletDir.y, _BulletDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, _rotZ + 90);

            Destroy(this.gameObject, _timer);
        }

        private void Update()
        {
            if (!_isDying)
            {
                _rb2d.position += _BulletDir * _mySpeed * Time.deltaTime;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player/HurtBox"))
            {
                StartCoroutine(BulletDestroy());
            }
            else if (collision.gameObject.CompareTag("Environment"))
            {
                StartCoroutine(BulletDestroy());
            }
        }

        private IEnumerator BulletDestroy()
        {
            _isDying = true;
            _anim.SetTrigger("Destroy");
            _rb2d.velocity = Vector2.zero;
            yield return new WaitForSeconds(0.25f);
            Destroy(gameObject);
        }

    }
}


