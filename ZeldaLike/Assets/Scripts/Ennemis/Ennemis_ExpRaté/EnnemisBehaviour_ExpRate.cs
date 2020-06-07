using System.Collections;
using UnityEngine;
using Game;
using UnityEngine.Experimental.Rendering.Universal;

namespace Ennemis
{
    public class EnnemisBehaviour_ExpRate : MonoBehaviour
    {
        [Header("Data")]
        public Transform _mySelf = null;
        public Scr_EnnemisLifeSystem _lifeSyst = null;
        public Rigidbody2D _myBody = null;

        public Light2D Gyrophare = null;


        [Header("Statistique")]
        public float _movementSpeed = 5f;

        public float _dashSpeed = 5f;
        public float _dashDuration = 2f;
        public float _dashRepos = 2f;
        public float _dashCooldown = 2f;
        public float _detectionRange = 20f;

        [Header("Parameter")]
        public bool _haveDetect = false;

        public bool _canDash = true;
        public bool _isDashing = false;

        [Header("Target")]
        [SerializeField] private Transform _target = null;
        public Transform _position
        {
            get { return _mySelf; }
            set { _mySelf = value; }
        }

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

            //defini à chaque frame dans quel direction est le joueur
            _targetDirection = (_target.position - _mySelf.position);
            //calcul la distance entre le GameObject et le joueur
            _targetDistance = Vector2.Distance(_mySelf.position, _target.position);
            //calcul si le joueur est detecté
            _haveDetect = PlayerInEnnemyRange(_targetDistance, _detectionRange);

            #endregion Variables Actualisée
            if (_haveDetect && Gyrophare.color != Color.red)
            {
                Gyrophare.color = Color.red;
            }
            else if (!_haveDetect)
            {
                Gyrophare.color = Color.black;
            }
        }

        private void FixedUpdate()
        {
            if (!_lifeSyst._isDead)
            {
                if (_haveDetect && !_lifeSyst._isTakingDamage)
                {
                    if (_canDash)
                    {
                        StartCoroutine(Dash(_targetDirection, _dashSpeed, _dashDuration, _dashRepos, _dashCooldown));
                    }

                    if (!_isDashing)
                    {
                        _myBody.velocity = _targetDirection.normalized * _movementSpeed;
                    }
                }
                else
                {
                    StopCoroutine("Dash");
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

        public IEnumerator Dash(Vector2 dashDirection, float dashSpeed, float dashDuration, float dashRepos, float dashCooldown)
        {
            _canDash = false;
            _isDashing = true;

            while (0 < dashDuration) // boucle durant la durée du dash
            {
                dashDuration -= Time.deltaTime;

                _myBody.velocity = dashDirection.normalized * dashSpeed;

                // Retour à la prochaine frame
                yield return new WaitForEndOfFrame();
            }

            _myBody.velocity = Vector2.zero;

            yield return new WaitForSeconds(dashRepos);

            _isDashing = false;

            yield return new WaitForSeconds(dashCooldown);

            _canDash = true;
        }
    }
}