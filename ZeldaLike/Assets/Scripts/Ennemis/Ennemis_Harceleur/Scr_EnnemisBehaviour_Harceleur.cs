using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemis
{
    public class Scr_EnnemisBehaviour_Harceleur : MonoBehaviour
    {
        [Header("Data")]
        public Transform _mySelf = null;
        public GameObject _attackZone = null;
        public Scr_EnnemisLifeSystem _lifeSyst = null;
        public Rigidbody2D _myBody = null;

        [Header("Statistique")]
        public float _movementSpeed = 5f;
        public float _attackSpeed = 5f;
        public float _attackDuration = 2f;
        public float _attackCharge = 2f;
        public float _attackRepos = 2f;
        public float _attackCooldown = 2f;
        public float _detectionRange = 20f;

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
                    if (_canTeleport)
                    {
                        StartCoroutine(teleportedAttack(_targetDirection, _attackSpeed, _attackDuration, _attackCharge, _attackRepos, _attackCooldown));
                    }
                }
                else
                {
                    StopCoroutine("teleportedAttack");
                }
            }

            if(_targetDistance < 10)
            {
                _myBody.velocity = -_targetDirection.normalized * _movementSpeed;
            }
            else if(_targetDistance > 15)
            {
                _myBody.velocity = _targetDirection.normalized * _movementSpeed;
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
            if(playerDistance > 10 && playerDistance < 15)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        IEnumerator teleportedAttack(Vector2 playerDirection, float attackSpeed, float attackDuration, float attackCharge, float attackRepos, float attackCooldown)
        {
            _canAttack = false;
            _isAttacking = true;
            _canTeleport = false;

            yield return new WaitForSeconds(attackCharge);

            while (0 < attackDuration)
            {
                attackDuration -= Time.deltaTime;

                Instantiate(_attackZone, transform.position, transform.rotation);

                yield return new WaitForEndOfFrame();
            }

            _myBody.velocity = Vector2.zero;

            yield return new WaitForSeconds(attackRepos);

            _isAttacking = false;

            yield return new WaitForSeconds(attackCooldown);

            _canAttack = true;
        }
    }
}
