using System.Collections;
using UnityEngine;

namespace Ennemis
{
    public class Scr_EnnemisBehaviour_Soldat : MonoBehaviour
    {
        [Header("Data")]
        public Transform _mySelf = null;
        public Scr_EnnemisLifeSystem _lifeSyst = null;
        public Rigidbody2D _myBody = null;

        [Header("Statistique")]
        public float _detectionShootingRange = 10f;
        public float _detectionChargeRange = 2f;
        public float _shootingRepos = 1f;
        public float _shootingCooldown = 0.7f;
        public float _chargeSpeed = 7f;
        public float _chargeCooldown = 3f;

        [Header("Parameter")]
        public bool _haveDetected = false;

        public bool _canShoot = true;
        public bool _isShooting = false;

        public bool _canCharge = false;
        public bool _isCharging = false;

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
            _targetDirection = (_target.position - _mySelf.position);
            _targetDistance = Vector2.Distance(_mySelf.position, _target.position);
            _haveDetected = PlayerInShootingRange(_targetDistance, _detectionShootingRange);
        }

        private void FixedUpdate()
        {
            if(_haveDetected && !_lifeSyst._isTakingDamage)
            {
                if (_canShoot)
                {
                    StartCoroutine(Shoot(_targetDirection, _shootingRepos, _shootingCooldown));
                }
            }
        }

        protected bool PlayerInShootingRange(float playerDistance, float testedShootingRange)
        {
            if(playerDistance <= testedShootingRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerator Shoot(Vector2 shootDirection, float _shootingRepos, float _shootingCooldown)
        {
            _canShoot = false;
            _isShooting = true;

            yield return new WaitForSeconds(_shootingRepos);

            _isShooting = false;

            yield return new WaitForSeconds(_shootingCooldown);

            _canShoot = true;
        }
    }
}
