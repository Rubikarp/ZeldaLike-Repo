using System.Collections;
using UnityEngine;
using Management;

namespace Game
{
    public class KnifeBehaviour : MonoBehaviour
    {
        public float _Speed = 35f;
        public float _Damage = 2f;
        public float _angleCorrection = 0;
        private Rigidbody2D _Body;
        private bool _ephemerate;
        public float _Lifetime = 0.4f;
        private GameObject _player;
        private Vector2 _playerOrientation;

        private void Start()
        {
            _Body = GetComponent<Rigidbody2D>();
            _ephemerate = true;
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerOrientation = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>()._CharacterDirection;
            FaceShootingDirection(_playerOrientation);
        }

        private void Update()
        {
            _Body.velocity = _Speed * 1000 * _playerOrientation.normalized * Time.deltaTime;

            if (_ephemerate == true)
            {
                _ephemerate = false;
                StartCoroutine(LimitedLifetime(_Lifetime));
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ennemis"))
            {
                DestroyKnife(gameObject);
            }
        }

        private IEnumerator LimitedLifetime(float Lifetime)
        {
            yield return new WaitForSeconds(Lifetime);
            DestroyKnife(gameObject);
        }

        private void DestroyKnife(GameObject knife)
        {
            Destroy(knife);
        }

        private void FaceShootingDirection(Vector2 shootingDirection)
        {
            //calcul l'angle pour faire face au joueur
            float rotZ = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg + _angleCorrection;
            //oriente l'object pour faire face au joueur
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }
}