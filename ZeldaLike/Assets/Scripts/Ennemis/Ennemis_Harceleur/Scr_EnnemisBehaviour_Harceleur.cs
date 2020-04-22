using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Ennemis
{
    public class Scr_EnnemisBehaviour_Harceleur : MonoBehaviour
    {
        [Header("Data")]
        public Transform _mySelf = null;
        public GameObject _attackZone = null;
        public Scr_EnnemisLifeSystem _lifeSyst = null;
        public Rigidbody2D _myBody = null;
        private InputManager _input = null;
        public bool Debug = false;


        [Header("Statistique")]
        public float _movementSpeed = 5f;

        public float _detectionRange = 20f;

        public float _backTeleportRange = 3f;
        public float _attackRange = 4f;

        public float _attackPrep = 1.2f;
        public float _attackDur = 0.5f;
        public float _attackCooldown = 2f;

        public float _distFar = 18f;
        public float _distNear = 13f;


        [Header("Parameter")]
        public bool _haveDetect = false;
        public bool _canAttack = true;
        public bool _isAttacking = false;
        public bool _canTeleport = false;

        [Header("Target")]
        [SerializeField] private Transform _target = null;

        private Vector2 _targetDirection = Vector2.zero;
        private float _targetDistance = 0;

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _mySelf = this.transform;
            _myBody = this.GetComponent<Rigidbody2D>();
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();

        }
        private void Update()
        {
            #region Variables Actualisée

            _targetDirection = (_target.position - _mySelf.position);
            _targetDistance = Vector2.Distance(_mySelf.position, _target.position);
            _haveDetect = PlayerInEnnemyRange(_targetDistance, _detectionRange);
            _canTeleport = EnemyInTeleportingRange(_targetDistance);

            #endregion Variables Actualisée
        }
        private void FixedUpdate()
        {
            if (!_lifeSyst._isDead)
            {
                if (_haveDetect && !_lifeSyst._isTakingDamage)
                {
                    if (_canTeleport && _canAttack)
                    {
                        StopAllCoroutines();
                        StartCoroutine(teleportedAttack(_targetDirection, _attackPrep, _attackDur, _attackCooldown));
                    }

                    if (_targetDistance < _distNear & !_isAttacking)
                    {
                        _myBody.velocity = -_targetDirection.normalized * _movementSpeed;
                    }
                    else if (_targetDistance > _distFar & !_isAttacking)
                    {
                        _myBody.velocity = _targetDirection.normalized * _movementSpeed;
                    }
                }
                else
                {
                    StopCoroutine("teleportedAttack");
                }
            }
        }

        protected bool PlayerInEnnemyRange(float playerDistance, float testedRange)
        {
            //Est ce que je detecte le joueur ?
            if (playerDistance <= testedRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool EnemyInTeleportingRange(float playerDistance)
        {
            if(playerDistance > _distNear && playerDistance < _distFar)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        IEnumerator teleportedAttack(Vector2 playerDirection, float attackCharge, float _attackDur, float attackCooldown)
        {
            _canAttack = false;
            _isAttacking = true;
            _canTeleport = false;

            _myBody.position = new Vector2(_target.position.x, _target.position.y) - (_backTeleportRange * new Vector2(_input._CharacterDirection.x, _input._CharacterDirection.y));
            _myBody.velocity = Vector2.zero;

            yield return new WaitForSeconds(attackCharge);

            Instantiate(_attackZone, new Vector2(transform.position.x, transform.position.y) + _targetDirection.normalized * _attackRange, transform.rotation);

            yield return new WaitForSeconds(_attackDur);

            _isAttacking = false;

            yield return new WaitForSeconds(attackCooldown);

            _canAttack = true;
        }

        private void OnDrawGizmos()
        {
            if (Debug)
            {
                Gizmos.color = new Color(1, 0, 0, 0.2f);
                Gizmos.DrawSphere(transform.position, _distFar);

                Gizmos.color = new Color(0, 0, 1, 0.2f);
                Gizmos.DrawSphere(transform.position, _distNear);
            }
        }
    }
}
