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
        public Scr_BossP3_AnimatorManager _myAnimator;

        [Space(10)]

        [Header("Variable à Lire")]
        public bool _inPattern = false;

        //ComboEclair
        public bool _willLightningRush = false;
        public bool _isLightningRush = false;
        private Vector2 _lightningRushDirection = Vector2.zero;
        private float _rushRotation;


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

        //AngleMort
        public bool _willTPonPlayer = false;


        [Space(10)]
        [Header("Variable à Tweek")]
        public float _movementSpeed = 5f;

        //ComboEclair
        [SerializeField] private Transform UpLeftCorner = null;
        [SerializeField] private Transform DownRightCorner = null;
        public float _lightningRushSpeed = 15f;
        public int _numberOflightningRush = 3;

        //Execution
        public GameObject _projectile = null;
        public float _shootingAllonge = 5f;

        //Acharnement
        public float _RushSpeed = 15f;
        public int _numberOfRush = 3;

        //AngleMort
        [SerializeField] private GameObject _angleMortAttack = null;

        //AngleMort
        [SerializeField] private GameObject _DegageAttack = null;

        [Space(10)]

        [Header("Target")]
        [SerializeField] private Transform _player = null;
        public Vector2 _playerDirection = Vector2.zero;
        private Vector2 _preShootDirection = Vector2.zero;
        private float _preShootDist = 6f;
        private float _playerDistance = 0;

        private void Start()
        {
            _mySelf = this.transform;
            _myBody = this.GetComponent<Rigidbody2D>();
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _myAnimator = this.GetComponent<Scr_BossP3_AnimatorManager>();
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
                        NextDirection(_actualPattern);
                        _inPattern = false;

                        break;

                    case Pattern.Execution:

                        _inPattern = true;
                        StartCoroutine(Execution(1.5f, 3));
                        Debug.Log("Pattern Execution");
                        break;

                    case Pattern.ComboEclair:

                        _inPattern = true;
                        StartCoroutine(ComboEclair(_lightningRushSpeed, _numberOflightningRush, UpLeftCorner.position, DownRightCorner.position));
                        Debug.Log("Pattern ComboEclair");
                        break;

                    case Pattern.Acharnement:

                        _inPattern = true;
                        StartCoroutine(Acharnement(_RushSpeed, _numberOfRush));
                        Debug.Log("Pattern Acharnement");
                        break;

                    case Pattern.AngleMort:

                        _inPattern = true;
                        StartCoroutine(AngleMort(_player.position));
                        Debug.Log("Pattern AngleMort");
                        break;

                    case Pattern.TirDansLeDos:
                        
                        _inPattern = true;
                        StartCoroutine(TirDansLeDos());
                        Debug.Log("Pattern TirDansLeDos");
                        break;

                    case Pattern.Degage:

                        _inPattern = true;
                        StartCoroutine(Degage());
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
            yield return new WaitForSeconds(timeBtwShoot);

            _willShoot = false;
            _isShooting = true;

            _myAnimator._animator.SetTrigger("IsSoldatShooting");
            yield return new WaitForSeconds(0.5f);
            //balle basique
            GameObject bullet = Instantiate(_projectile, _mySelf.position + new Vector3(_playerDirection.x, _playerDirection.y).normalized * _shootingAllonge, _mySelf.rotation);
            bullet.GetComponent<Scr_BossP3_Bullet>().BulletSetDir(_playerDirection.normalized);            
            
            _isShooting = false;

            do
            {
                NumberOfBullet--;
                _willShoot = true;
                yield return new WaitForSeconds(timeBtwShoot);
                _willShoot = true;
                _isShooting = true;

                _myAnimator._animator.SetTrigger("IsIngénieurShooting");
                yield return new WaitForSeconds(0.5f);
                //Les balles anticipée
                bullet = Instantiate(_projectile, _mySelf.position + new Vector3(_preShootDirection.x, _preShootDirection.y).normalized * _shootingAllonge, _mySelf.rotation);
                bullet.GetComponent<Scr_BossP3_Bullet>().BulletSetDir(_preShootDirection.normalized);

                _isShooting = false;

            } while (NumberOfBullet > 0);


            yield return new WaitForSeconds(1f);

            //fin du pattern
            _inPattern = false;
            NextDirection(_actualPattern);

        }

        public IEnumerator Acharnement(float rushSpeed, int NumberOfIteration)
        {
            Vector3 targetPos;

            do
            {
                NumberOfIteration--;
                _lifeSyst._isVunerable = false;
                targetPos = _player.position;
                _willRush = true;

                _myAnimator._animator.SetBool("isPersisting", true);
                yield return new WaitForSeconds(0.5f);

                _willRush = false;
                _isRushing = true;

                _rushDirection = (targetPos - _mySelf.position).normalized;
                _spotDistance = Vector2.Distance(_mySelf.position, targetPos);
                _myAnimator._canDir = false;

                while (_spotDistance > 0.5f) // boucle durant la durée du dash
                {
                    _rushDirection = (targetPos - _mySelf.position).normalized;
                    _spotDistance = Vector2.Distance(_mySelf.position, targetPos);

                    _myBody.velocity = _rushDirection * rushSpeed;

                    _spotDistance = Vector2.Distance(_mySelf.position, targetPos);

                    if(Vector2.Distance(_mySelf.position, _player.position) < 1f)
                    {
                        NumberOfIteration = 0;
                    }

                    // Retour à la prochaine frame
                    yield return new WaitForEndOfFrame();
                }

                _myAnimator._animator.SetBool("isPersisting", false);
                _myAnimator._canDir = true;
                _myBody.velocity = Vector2.zero;
                _lifeSyst._isVunerable = true;
                _isRushing = false;
                yield return new WaitForSeconds(0.125f);
            } while (NumberOfIteration > 0);

            yield return new WaitForSeconds(1f);

            //fin du pattern
            _inPattern = false;
            NextDirection(_actualPattern);

        }

        public IEnumerator ComboEclair(float lightningRushSpeed, int NumberOfIteration, Vector3 UpLeft, Vector3 DownRight)
        {
            Vector3 targetPos;
            bool touchLimit;

            do
            {
                NumberOfIteration--;
                _willLightningRush = true;

                _myAnimator._animator.SetBool("IsComboEclair", true);
                yield return new WaitForSeconds(0.75f);
                targetPos = _player.position;

                _willLightningRush = false;
                _isLightningRush = true;
                touchLimit = false;

                _lightningRushDirection = (targetPos - _mySelf.position).normalized;
                _rushRotation = Mathf.Atan2(_lightningRushDirection.y, _lightningRushDirection.x) * Mathf.Rad2Deg;
                _mySelf.rotation = Quaternion.Euler(0f, 0f, _rushRotation + 90);

                while (!touchLimit) // boucle durant la durée du dash
                {

                    _myBody.velocity = _lightningRushDirection * lightningRushSpeed;

                    if (_mySelf.position.x < UpLeft.x && _lightningRushDirection.x < 0)
                    {
                        touchLimit = true;
                    }
                    else if (_mySelf.position.x > DownRight.x && _lightningRushDirection.x > 0)
                    {
                        touchLimit = true;
                    }
                    else if (_mySelf.position.y > UpLeft.y && _lightningRushDirection.y > 0)
                    {
                        touchLimit = true;
                    }
                    else if (_mySelf.position.y < DownRight.y && _lightningRushDirection.y < 0)
                    {
                        touchLimit = true;
                    }

                    // Retour à la prochaine frame
                    yield return new WaitForEndOfFrame();
                }

                _mySelf.rotation = Quaternion.Euler(0f, 0f, 0f);
                _myBody.velocity = Vector2.zero;
                _myAnimator._animator.SetBool("IsComboEclair", false);
                _isLightningRush = false;
                yield return new WaitForSeconds(0.75f);

            } while (NumberOfIteration > 0);

            yield return new WaitForSeconds(1f);

            //fin du pattern
            _inPattern = false;
            NextDirection(_actualPattern);

        }

        public IEnumerator AngleMort(Vector3 PlayerPos)
        {
            _willTPonPlayer = true;

            yield return new WaitForSeconds(1.5f);

            _willTPonPlayer = false;
            _myAnimator._animator.SetTrigger("isAngleMort");

            _myBody.position = PlayerPos;
            yield return new WaitForSeconds(0.25f);
            Instantiate(_angleMortAttack, this.transform.position, this.transform.rotation);

            yield return new WaitForSeconds(1f);

            //fin du pattern
            _inPattern = false; 
            NextDirection(_actualPattern);

        }

        public IEnumerator TirDansLeDos()
        {
            _myAnimator._animator.SetTrigger("isBackShooting");
            yield return new WaitForSeconds(1.175f);

            //TP derrière le joueur
            _mySelf.position = _player.position - (new Vector3(_input._CharacterDirection.x, _input._CharacterDirection.y, 0) * 3) ;

            yield return new WaitForSeconds(0.5f);

            _myAnimator._animator.SetTrigger("IsIngénieurShooting");

            yield return new WaitForSeconds(0.5f);

            //Les balles anticipée
            GameObject bullet = Instantiate(_projectile, _mySelf.position + new Vector3(_preShootDirection.x, _preShootDirection.y).normalized * _shootingAllonge, _mySelf.rotation);
            bullet.GetComponent<Scr_BossP3_Bullet>().BulletSetDir(_preShootDirection.normalized);

            yield return new WaitForSeconds(1f);

            //fin du pattern
            _inPattern = false;
            NextDirection(_actualPattern);
        }

        public IEnumerator Degage()
        {
            _myAnimator._animator.SetTrigger("isDegaging");
            yield return new WaitForSeconds(1.25f);

            Instantiate(_DegageAttack, this.transform.position, this.transform.rotation);

            yield return new WaitForSeconds(1f);

            //fin du pattern
            _inPattern = false;
            NextDirection(_actualPattern);

        }

        private void NextDirection(Pattern previousPattern)
        {
            int diceRoll = 0;

            
                diceRoll = Random.Range(0, 11); //il y a 6 patterns

                if (diceRoll < 2)
                {
                    _actualPattern = Pattern.TirDansLeDos;
                }
                else if (diceRoll >= 2 && diceRoll < 4)
                {
                    _actualPattern = Pattern.Acharnement;
                }
                else if (diceRoll >= 4 && diceRoll < 5)
                {
                    _actualPattern = Pattern.AngleMort;
                }
                else if (diceRoll >= 5 && diceRoll < 6)
                {
                    _actualPattern = Pattern.Degage;
                }
                else if (diceRoll >= 6 && diceRoll < 8)
                {
                    _actualPattern = Pattern.Execution;
                }
                else if (diceRoll >= 8 && diceRoll < 10)
                {
                    _actualPattern = Pattern.ComboEclair;
                }
                else //  diceRoll < 20
                {
                    _actualPattern = Pattern.Rien;
                }
        }

        private void OnDrawGizmos()
        {
            //Zone de combat
            Vector3 test = UpLeftCorner.position - DownRightCorner.position;

            Debug.DrawRay(UpLeftCorner.position, Vector3.down * test.y, Color.green);
            Debug.DrawRay(UpLeftCorner.position, Vector3.left * test.x, Color.green);
            Debug.DrawRay(DownRightCorner.position, Vector3.up * test.y, Color.green);
            Debug.DrawRay(DownRightCorner.position, Vector3.right * test.x, Color.green);
        }
    }
}