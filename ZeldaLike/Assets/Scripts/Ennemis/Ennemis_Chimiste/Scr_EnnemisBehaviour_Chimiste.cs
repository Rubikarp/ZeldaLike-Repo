using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemis
{
    public class Scr_EnnemisBehaviour_Chimiste : MonoBehaviour
    {
        [Header("Data")]
        public Transform _mySelf = null;
        public Rigidbody2D _myBody = null;
        public Scr_EnnemisLifeSystem _lifeSyst = null;
        [Space(5)]
        public GameObject _puddle = null;

        [Header("Statistique")]
        public float _movementSpeed = 5f;
        public float _detectionRange = 20f;
        public float _circleRange = 3f;
        public float _timer = 0.3f;


        [Header("Parameter")]
        public bool _haveDetect = false;

        [Header("Target")]
        [SerializeField] private Transform _target = null;

        private float _targetDistance = 0;
        private Vector2 _targetDirection = Vector2.zero;
        private Vector2 _movingPos = Vector2.zero;
        private Vector2 _movingDirection = Vector2.zero;

        void Start()
        {
            _mySelf = this.transform;
            _myBody = this.GetComponent<Rigidbody2D>();
            
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update()
        {
            #region Variables Actualisée

            _targetDirection = (_target.position - _mySelf.position).normalized;
            _targetDistance = Vector2.Distance(_mySelf.position, _target.position);
            _haveDetect = PlayerInEnnemyRange(_targetDistance, _detectionRange);

            _movingPos = new Vector2(_target.position.x, _target.position.y) + (-_targetDirection * _circleRange) + (Vector2.Perpendicular(-_targetDirection) * _circleRange);
            _movingDirection = (_movingPos - _myBody.position).normalized;

            #endregion Variables Actualisée
        }

        private void FixedUpdate()
        {
            if (!_lifeSyst._isDead)
            {
                if (_haveDetect && !_lifeSyst._isTakingDamage)
                {
                    Orbit();

                    _timer -= Time.deltaTime;

                    if (_timer <= 0)
                    {
                        Instantiate(_puddle, transform.position, transform.rotation);
                        _timer = 0.3f;
                    }
                }
            }

        }

        private void Orbit()
        {
            _myBody.velocity = _movingDirection * _movementSpeed;
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

        private void OnDrawGizmos()
        {
            Debug.DrawLine(_movingPos, _mySelf.position, Color.red);
        }

    }
}
