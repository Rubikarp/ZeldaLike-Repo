using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Ennemies
{
    public class Scr_BossPhase1 : MonoBehaviour
    {
        private bool _actionActive;
        public float _delayBetweenActions;
        private float _delay;
        private bool _canGoDelay;
        private int _randomAction;
        public Transform _mySelf;
        private GameObject _player;

        [Header("Renforts")]
        public List<GameObject> _renforts;
        public List<Transform> _renfortsSpawns;

        [Header("Tir de Couverture")]
        public GameObject _bullet;
        public float _couvertureSpeed;
        private float _couvertureDuration;
        public float _couvertureDurationOrigin;
        public Transform _bulletContainer;
        public float _shootingAllonge;
        public float _delayBetweenShotsOrigin;
        private float _delayBetweenShots;
        public List<Vector3> _bulletFury;
        [HideInInspector] public Vector3 _currentTarget = Vector2.zero;


        [Header("Attaque au CaC")]
        public float _attackRange;
        public float _attackCast;
        private Rigidbody2D _playerBody;
        public float _knockBackSpeed;
        public float _stunDuration;
        public GameObject _attackZone;


        [Header("Grenade")]
        public GameObject _grenade;
        public Vector3 _grenadeTarget;


        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerBody = _player.GetComponent<Rigidbody2D>();
            _actionActive = false;
            _delay = _delayBetweenActions;
            _canGoDelay = false;
            _couvertureDuration = _couvertureDurationOrigin;
            _delayBetweenShots = _delayBetweenShotsOrigin;

        }

        // Update is called once per frame
        void Update()
        {
            if (_canGoDelay == true)
            {
                if (_delay > 0)
                {
                    _delay -= Time.deltaTime;
                }
                else if (_delay <= 0)
                {
                    _delay = _delayBetweenActions;
                    _canGoDelay = false;
                    _actionActive = false;
                }
            }

            if (_actionActive == false)
            {
                _randomAction = Random.Range(1, 6);

                switch (_randomAction)
                {
                    case 1:
                        Renforts();
                        _actionActive = true;
                        Debug.Log("Renforts");
                        break;

                    case 2:
                        TirDeCouverture();
                        _actionActive = true;
                        Debug.Log("TirDeCouverture");
                        break;

                    case 3:
                        Grenade();
                        _actionActive = true;
                        Debug.Log("Grenade");
                        break;

                    case 4:
                        StartCoroutine(AttaqueCaC());
                        _actionActive = true;
                        Debug.Log("AttaqueCaC");
                        break;

                    case 5:
                       StartCoroutine(FouDeLaGachette());
                        _actionActive = true;
                        Debug.Log("FouDeLaGachette");
                        break;
                }
            }
        }

        private void Renforts()
        {
            for (int i = 0; i < _renforts.Count; i++)
            {
                Instantiate(_renforts[i], _renfortsSpawns[i]);
            }

            _canGoDelay = true;
        }

        private void TirDeCouverture()
        {
            _currentTarget = (_player.transform.position - _mySelf.position);

            Instantiate(_bullet, _mySelf.position + _currentTarget.normalized * _shootingAllonge, _mySelf.rotation, _bulletContainer);

            while (_couvertureDuration > 0)
            {
                _couvertureDuration -= Time.deltaTime;

                _mySelf.position = Vector2.MoveTowards(_mySelf.position, _player.transform.position, -_couvertureSpeed * Time.deltaTime);

                new WaitForEndOfFrame();
            }

            _couvertureDuration = _couvertureDurationOrigin;

            _canGoDelay = true;
        }

        private void Grenade()
        {
            _grenadeTarget = _player.transform.position;

            Instantiate(_grenade, _mySelf.position + _grenadeTarget.normalized * _shootingAllonge, _mySelf.rotation, _bulletContainer);

            _canGoDelay = true;
        }

        private IEnumerator AttaqueCaC()
        {
            yield return new WaitForSeconds(_attackCast);

            Instantiate(_attackZone, _mySelf.position + _player.transform.position.normalized, _mySelf.rotation, _mySelf);

            _canGoDelay = true;
        }

        private IEnumerator FouDeLaGachette()
        {
            for (int i = 0; i < _bulletFury.Count; i++)
            {
                _currentTarget = (_bulletFury[i] - _mySelf.position);
                Instantiate(_bullet, _mySelf.position + _currentTarget.normalized * _shootingAllonge, _mySelf.rotation, _bulletContainer);
                yield return new WaitForSeconds(_delayBetweenShots);
            }

            _canGoDelay = true;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_mySelf.position, _attackRange);
        }


    }
}

