using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
    public class Scr_BossP1BulletBehavior : MonoBehaviour
    {
        private Vector3 _target;
        public float _speed;
        public float _lifeTime;
        public float _destroyAnimation;

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform.position;
        }

        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

            if (_lifeTime > 0)
            {
                _lifeTime -= Time.deltaTime;
            }
            else if (_lifeTime <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.transform.parent.parent.CompareTag("Player"))
            {
                if (_destroyAnimation > 0)
                {
                    _destroyAnimation -= Time.deltaTime;
                }
                else if (_destroyAnimation <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
