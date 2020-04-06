using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

namespace Ennemis
{
    public class Scr_BossBehavior_Phase3 : MonoBehaviour
    {
        enum Pattern { Rien, Execution, ComboEclair, Acharnement, AngleMort, TirDansLeDos, Degage };
        [SerializeField] private Pattern _actualPattern = 0;


        [Header("Components")]
        public Transform _mySelf = null;
        public Rigidbody2D _myBody = null;
        public Scr_BossLifeSystem _lifeSyst = null;
        public InputManager _input = null;
        //public Animator _myAnimator = null;
        [Space(10)]

        [Header("Variable à Lire")]
        public bool _inPattern = false;
        //Execution
        public bool _willShoot = false;
        public bool _isShooting = false;

        //Acharnement
        public bool _willRush = false;
        public bool _isRushing = false;
        private float _spotDistance = 0;
        private Vector2 _rushDirection = Vector2.zero;
        public Transform _position
        {
            get { return _mySelf; }
            set { _mySelf = value; }
        }

        [Space(10)]

        [Header("Variable à Tweek")]
        public float _movementSpeed = 5f;

        //Execution
        public GameObject _projectile = null;
        public float _shootingAllonge = 5f;

        //Acharnement
        public float _RushSpeed = 5f;
        public int _numberOfRush = 3;

        [Space(10)]


        [Header("Target")]
        [SerializeField] private Transform _player = null;
        private Vector2 _playerDirection = Vector2.zero;
        private Vector2 _preShootDirection = Vector2.zero;
        private float _preShootDist = 6f;
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

            //pré-shoot
            _preShootDirection = ((_player.position + new Vector3(_input._CharacterDirection.x, _input._CharacterDirection.y) * _preShootDist ) - _mySelf.position);

        }

        private void FixedUpdate()
        {
            if (!_inPattern)
            {
                switch (_actualPattern)
                {
                    case Pattern.Rien:

                        _inPattern = true;
                        Debug.Log("Pattern nothing");
                        break;

                    case Pattern.Execution:

                        _inPattern = true;
                        StartCoroutine(Execution(_RushSpeed, _numberOfRush));
                        Debug.Log("Pattern Execution");
                        break;

                    case Pattern.ComboEclair:

                        _inPattern = true;
                        Debug.Log("Pattern ComboEclair");
                        break;

                    case Pattern.Acharnement:

                        _inPattern = true;
                        StartCoroutine(Acharnement(_RushSpeed, _numberOfRush));
                        Debug.Log("Pattern Acharnement");
                        break;

                    case Pattern.AngleMort:

                        _inPattern = true;
                        Debug.Log("Pattern AngleMort");
                        break;

                    case Pattern.TirDansLeDos:
                        
                        _inPattern = true;
                        Debug.Log("Pattern TirDansLeDos");
                        break;

                    case Pattern.Degage:

                        _inPattern = true;
                        Debug.Log("Pattern Degage");
                        break;

                    default:
                        Debug.LogError("Incorrect Pattern");
                        break;
                }
            }
        }

        public IEnumerator Execution(float timeBtwShoot, int NumberOfBullet)
        {
            _willShoot = true;
            yield return new WaitForSeconds(1f);
            _willShoot = true;
            _isShooting = true;
            //première Balle
            Instantiate(_projectile, _mySelf.position + new Vector3(_playerDirection.x, _playerDirection.y) * _shootingAllonge, _mySelf.rotation, _mySelf);
            _isShooting = false;

            do
            {
                NumberOfBullet--;
                _willShoot = true;
                yield return new WaitForSeconds(1f);
                _willShoot = true;
                _isShooting = true;
                //Les balles anticipée
                Instantiate(_projectile, _mySelf.position + new Vector3(_preShootDirection.x, _preShootDirection.y) * _shootingAllonge, _mySelf.rotation, _mySelf);
                _isShooting = false;

            } while (NumberOfBullet > 0);

            //fin du pattern
            _inPattern = true;
        }

        public IEnumerator Acharnement(float rushSpeed, int NumberOfIteration)
        {
            Vector3 targetPos;

            do
            {
                NumberOfIteration--;
                targetPos = _player.position;
                _willRush = true;

                yield return new WaitForSeconds(0.75f);

                _willRush = false;
                _isRushing = true;

                _rushDirection = (targetPos - _mySelf.position).normalized;
                _spotDistance = Vector2.Distance(_mySelf.position, targetPos);

                while (_spotDistance > 2) // boucle durant la durée du dash
                {
                    _rushDirection = (targetPos - _mySelf.position).normalized;
                    _spotDistance = Vector2.Distance(_mySelf.position, targetPos);

                    _myBody.velocity = _rushDirection * rushSpeed;

                    _spotDistance = Vector2.Distance(_mySelf.position, targetPos);

                    if(Vector2.Distance(_mySelf.position, _player.position) < 2f)
                    {
                        NumberOfIteration = 0;
                    }

                    // Retour à la prochaine frame
                    yield return new WaitForEndOfFrame();
                }

                _myBody.velocity = Vector2.zero;
                _isRushing = false;
            } while (NumberOfIteration > 0);

            //fin du pattern
            _inPattern = true;
        }


    }
}