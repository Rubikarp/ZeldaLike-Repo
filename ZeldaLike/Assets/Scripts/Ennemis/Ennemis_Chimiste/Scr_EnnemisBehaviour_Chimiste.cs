using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemis
{
    public class Scr_EnnemisBehaviour_Chimiste : MonoBehaviour
    {
        [Header("Data")]
        public Transform _mySelf = null;
        public GameObject _puddle = null;
        public Scr_EnnemisLifeSystem _lifeSyst = null;
        public Rigidbody2D _myBody = null;

        [Header("Statistique")]
        public float _movementSpeed = 5f;

        public float _detectionRange = 20f;

        public float _timer = 1f;

        public float orbitDistance = 10f;
        public float orbitDegreesPerSec = 45f;

        [Header("Parameter")]
        public bool _haveDetect = false;
        public bool _startRevolving = false;
        public bool _makePuddles = false;

        [Header("Target")]
        [SerializeField] private Transform _target = null;

        private Vector2 _targetDirection = Vector2.zero;
        private float _targetDistance = 0;
        public Vector3 relativeDistance = Vector3.zero;

        void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _mySelf = this.transform;
            _myBody = this.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            #region Variables Actualisée

            _targetDirection = (_target.position - _mySelf.position);
            _targetDistance = Vector2.Distance(_mySelf.position, _target.position);
            _haveDetect = PlayerInEnnemyRange(_targetDistance, _detectionRange);
            _startRevolving = EnemyInOrbitRange(_targetDistance);

            #endregion Variables Actualisée
        }

        private void FixedUpdate()
        {
            if (!_lifeSyst._isDead)
            {
                if (_haveDetect && !_lifeSyst._isTakingDamage)
                {
                    if (_startRevolving)
                    {
                        Orbit();
                        _makePuddles = true;
                    }

                    if (!_startRevolving)
                    {
                        _myBody.velocity = _targetDirection.normalized * _movementSpeed;
                    }
                }
                else
                {
                    StopCoroutine("inOrbit");
                }
            }

            if (_makePuddles)
            {
                _timer -= Time.deltaTime;
            }

            if(_timer <= 0)
            {
                Instantiate(_puddle);
                _timer = 1f;
            }
        }

        void Orbit()
        {
            if(_target != null)
            {
                _mySelf.position = _target.position + relativeDistance;
                transform.RotateAround(_target.position, Vector2.up, orbitDegreesPerSec * Time.deltaTime);
            }
        }

        protected bool PlayerInEnnemyRange(float playerDistance, float testedRange)
        {
            if (playerDistance <= testedRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool EnemyInOrbitRange(float playerDistance)
        {
            if (playerDistance > 10 && playerDistance < 15)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
