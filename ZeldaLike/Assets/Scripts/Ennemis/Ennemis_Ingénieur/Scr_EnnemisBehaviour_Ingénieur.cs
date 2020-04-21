using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Ennemis
{
    public class Scr_EnnemisBehaviour_Ingénieur : MonoBehaviour
    {
        [Header("Data")]
        public Transform _mySelf = null;
        public GameObject _projectile;
        public GameObject _bomb;
        public Scr_EnnemisLifeSystem _lifeSyst = null;
        public Rigidbody2D _myBody = null;

        [Header("Shoot")]
        public float _detectionShootingRange = 18f;
        public float _shootingAllonge = 2f;
        public float _shootingRepos = 0.3f;
        public float _shootingCooldown = 0.9f;

        [Header("Running Away")]
        public float _detectionRunningRange = 8f;
        public float _runSpeed = 25f;
        public float _runDuration = 4f;
        public float _runRepos = 0.3f;
        public float _runCooldown = 1.9f;

        [Header("Parameter detecting")]
        public bool _haveDetected = false;
        public bool _inDanger = false;

        [Header("Parameter shooting")]
        public float _anticipation = 2f;
        public bool _canShoot = true;
        public bool _isShooting = false;

        [Header("Parameter charging")]
        public bool _canRun = true;
        public bool _isRunning = false;

        [Header("Target")]
        [SerializeField] private Transform _target = null;
        [SerializeField] private InputManager _playerInputs = null;

        [HideInInspector] public Vector3 _targetDirection = Vector2.zero;
        private float _targetDistance = 0;

        void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _mySelf = this.transform;
            _myBody = this.GetComponent<Rigidbody2D>();
            _playerInputs = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
        }

        private void Update()
        {
            _targetDirection = new Vector2(_target.position.x, _target.position.y) + new Vector2(_playerInputs._CharacterDirection.x, _playerInputs._CharacterDirection.y).normalized * _anticipation - new Vector2(_mySelf.position.x, _mySelf.position.y);
            _targetDistance = Vector2.Distance(_mySelf.position, _target.position);
            _haveDetected = PlayerInShootingRange(_targetDistance, _detectionShootingRange, _detectionRunningRange);
            _inDanger = PlayerInRunningRange(_targetDistance, _detectionRunningRange);
        }
        void FixedUpdate()
        {
            if (_haveDetected && !_lifeSyst._isTakingDamage)
            {
                if (_canShoot && !_isRunning)
                {
                    StartCoroutine(Shoot(_targetDirection, _shootingRepos, _shootingCooldown));
                }
            }

            if (_inDanger && !_lifeSyst._isTakingDamage)
            {
                if (_canRun)
                {
                    StartCoroutine(Run(_targetDirection, _runSpeed, _runDuration, _runRepos, _runCooldown));
                }
            }

            if (!_isRunning)
            {
                _myBody.velocity = Vector2.zero;
            }
        }
        public IEnumerator Shoot(Vector2 shootDirection, float _shootingRepos, float _shootingCooldown)
        {
            _canShoot = false;
            _isShooting = true;

            GameObject bullet = Instantiate(_projectile, _mySelf.position + _targetDirection.normalized * _shootingAllonge, _mySelf.rotation, _mySelf);

            bullet.GetComponent<Scr_ProjectileBehaviour_Ingénieur>().BulletSetDir(_targetDirection.normalized);

            yield return new WaitForSeconds(_shootingRepos);

            _isShooting = false;

            yield return new WaitForSeconds(_shootingCooldown);

            _canShoot = true;
        }

        public IEnumerator Run(Vector2 runDirection, float runSpeed, float runDuration, float runRepos, float runCooldown)
        {
            _canRun = false;
            _isRunning = true;
            Instantiate(_bomb, transform.position, transform.rotation);

            while (0 < runDuration)
            {
                runDuration -= Time.deltaTime;

                _myBody.velocity = -runDirection.normalized * runSpeed; //opposé à la position de la target de l'ennemi > Fuite

                yield return new WaitForEndOfFrame();
            }

            _myBody.velocity = Vector2.zero;

            yield return new WaitForSeconds(runRepos);

            _isRunning = false;

            yield return new WaitForSeconds(runCooldown);

            _canRun = true;
        }

        #region Tools

        protected bool PlayerInShootingRange(float playerDistance, float testedShootingRange, float testedChargingRange)
        {
            if (playerDistance <= testedShootingRange && playerDistance >= testedChargingRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected bool PlayerInRunningRange(float playerDistance, float testedRealRunningRange)
        {
            if (playerDistance <= testedRealRunningRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}