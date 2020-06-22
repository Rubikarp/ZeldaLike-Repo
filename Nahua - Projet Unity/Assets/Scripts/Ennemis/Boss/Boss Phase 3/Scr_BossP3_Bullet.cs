using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemis
{
    public class Scr_BossP3_Bullet : MonoBehaviour
    {
        [Header("Data")]
        public Transform _projectile = null;
        public Rigidbody2D _rb2d = null;
        private float _rotZ;
        public Animator _anim;

        [Header("Statistiques")]
        public Vector2 _BulletDir = Vector2.zero;
        public float _mySpeed = 12f;
        public float _timer = 3f;
        private bool _isDying;

        private void Start()
        {
            _isDying = false;
            _projectile = this.transform;
            _rb2d = this.GetComponent<Rigidbody2D>();
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
                Debug.Log(collision.gameObject.name);
                StartCoroutine(BulletDestroy());
            }
        }

        public void BulletSetDir(Vector2 direction)
        {
            _BulletDir = direction;
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