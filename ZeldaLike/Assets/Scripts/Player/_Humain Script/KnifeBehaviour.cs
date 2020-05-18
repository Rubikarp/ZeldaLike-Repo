using System.Collections;
using UnityEngine;
using Management;

namespace Game
{
    public class KnifeBehaviour : MonoBehaviour
    {
        [Header("Component")]
        public Rigidbody2D _Body;
        private GameObject _player;

        [Header("Variable")]
        public float _Speed = 35f;
        public float _Damage = 2f;
        [Space(10)]
        public bool _ephemerate = true;
        public float _Lifetime = 0.4f;
        [Space(10)]
        public float _angleCorrection = 0;
        public Vector2 _playerOrientation;

        private SoundManager sound;

        private void Awake()
        {
            sound = SoundManager.Instance;
        }

        private void Start()
        {
            _Body = GetComponent<Rigidbody2D>();
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerOrientation = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>()._CharacterDirection;
            FaceShootingDirection(_playerOrientation);
        }

        private void Update()
        {
            if (_ephemerate)
            {
                Destroy(gameObject, _Lifetime);
                _ephemerate = false;
            }
        }

        private void FixedUpdate()
        {
            _Body.velocity = _Speed * _playerOrientation.normalized;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ennemis/HurtBox") || collision.gameObject.CompareTag("Environment"))
            {
                Destroy(gameObject);
                sound.PlaySound("KnifeImpact");
            }
        }

        public void FaceShootingDirection(Vector2 shootingDirection)
        {
            //calcul l'angle pour faire face au joueur
            float rotZ = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg + _angleCorrection;
            //oriente l'object pour faire face au joueur
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }
}