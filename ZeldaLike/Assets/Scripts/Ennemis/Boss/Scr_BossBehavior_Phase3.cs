using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Ennemis
{
    public class Scr_BossBehavior_Phase3 : MonoBehaviour
    {
        enum Pattern { Rien, Execution, ComboEclair, Acharnement, AngleMort, TirDansLeDos, Degage };
        private Pattern _actualPattern = 0;

        [Header("Components")]
        public Transform _mySelf = null;
        public Rigidbody2D _myBody = null;
        public Scr_BossLifeSystem _lifeSyst = null;
        public InputManager _input = null;
        //public Animator _myAnimator = null;
        [Space(10)]

        [Header("Variable à Lire")]

        //Acharnement
        public bool _isRushing = false;
        private Vector2 _spotDirection = Vector2.zero;
        private float _spotDistance = 0;


        [Space(10)]

        [Header("Variable à Tweek")]
        public float _movementSpeed = 5f;

        //Acharnement
        public float _RushSpeed = 5f;

        [Space(10)]


        [Header("Target")]
        [SerializeField] private Transform _player = null;
        public Transform _position
        {
            get { return _mySelf; }
            set { _mySelf = value; }
        }
        private Vector2 _playerDirection = Vector2.zero;
        private float _playerDistance = 0;

        private void Start()
        {
            _mySelf = this.transform;
            _myBody = this.GetComponent<Rigidbody2D>();
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            //_myAnimator = this.GetComponent<Animator>();

        }

        private void Update()
        {
            //defini à chaque frame dans quel direction est le joueur
            _playerDirection = (_player.position - _mySelf.position);
            //calcul la distance entre le GameObject et le joueur
            _playerDistance = Vector2.Distance(_mySelf.position, _player.position);
        }

        private void FixedUpdate()
        {
            switch (_actualPattern)
            {
                case Pattern.Rien:

                    Debug.Log("Pattern nothing");
                    break;

                case Pattern.Execution:

                    Debug.Log("Pattern nothing");
                    break;

                case Pattern.ComboEclair:

                    Debug.Log("Pattern nothing");
                    break;

                case Pattern.Acharnement:

                    Debug.Log("Pattern nothing");
                    break;

                case Pattern.AngleMort:

                    Debug.Log("Pattern nothing");
                    break;

                case Pattern.TirDansLeDos:

                    Debug.Log("Pattern nothing");
                    break;

                case Pattern.Degage:

                    Debug.Log("Pattern nothing");
                    break;

                default:
                    Debug.LogError("Incorrect Pattern");
                    break;
            }
        }

        public IEnumerator Rush(Vector3 targetPos, float rushSpeed, float dashDuration, float dashRepos, float dashCooldown)
        {
            _isRushing = true;
            _spotDirection = (targetPos - _mySelf.position);
            _spotDistance = Vector2.Distance(_mySelf.position, targetPos);

            while (_spotDistance > 2) // boucle durant la durée du dash
            {
                _myBody.velocity = _spotDirection.normalized * _RushSpeed;

                _spotDistance = Vector2.Distance(_mySelf.position, targetPos);

                // Retour à la prochaine frame
                yield return new WaitForEndOfFrame();
            }

            _myBody.velocity = Vector2.zero;
            _isRushing = false;
        }
    
    
    }
}