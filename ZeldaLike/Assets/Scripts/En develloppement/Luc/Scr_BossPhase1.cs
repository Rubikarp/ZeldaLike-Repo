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


        [Header("Attaque au CaC")]
        public float _attackRange;
        public float _attackCastOrigin;
        private float _attackCast;
        private Rigidbody2D _playerBody;
        public float _knockBackSpeed;
        public float _stunDuration;

        [Header("Grenade")]
        public GameObject _grenade;
        public Vector3 _grenadeTarget;

        [Header("Fou de la Gachette")]
        public GameObject _bulletsFury;
        public float _delayBetweenShotsOrigin;
        private float _delayBetweenShots;
        public Vector3 _currentTarget;


        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _playerBody = _player.GetComponent<Rigidbody2D>();
            _actionActive = false;
            _delay = _delayBetweenActions;
            _canGoDelay = false;
            _couvertureDuration = _couvertureDurationOrigin;
            _attackCast = _attackCastOrigin;
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
                        AttaqueCaC();
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
            Instantiate(_bullet, _mySelf.position + _player.transform.position.normalized * _shootingAllonge, _mySelf.rotation, _bulletContainer);

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

        private void AttaqueCaC()
        {
            while (_attackCast > 0)
            {
                _attackCast -= Time.deltaTime;
            }

            _attackCast = _attackCastOrigin;

            Collider2D[] playerToHit = Physics2D.OverlapCircleAll(_mySelf.position, _attackRange);

            for (int k = 0; k < playerToHit.Length; k++)
            {
                if (playerToHit[k].gameObject.transform.parent.parent.CompareTag("Player"))
                {
                    Vector2 _knockBackDirection = _player.transform.position - _mySelf.position;
                    playerToHit[k].gameObject.transform.parent.GetComponentInChildren<Scr_PlayerLifeSystem>().TakingDamage(1, _playerBody, _knockBackDirection, _knockBackSpeed, _stunDuration);
                }
            }

            _canGoDelay = true;
        }

        private IEnumerator FouDeLaGachette()
        {
            Instantiate(_bulletsFury, _mySelf.position, _mySelf.rotation, _bulletContainer);

            yield return new WaitForSeconds(2);

            _canGoDelay = true;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_mySelf.position, _attackRange);
        }


    }
}

