using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Ennemis
{
    public class Scr_BossPhase1 : MonoBehaviour
    {
        [Header("General")]
        private bool _actionActive; //Pour savoir si une action est en cours.
        public float _delayBetweenActions;  //Délai avant une nouvelle action.
        private float _delay;
        private bool _canGoDelay; //Permet de savoir si on lance le délai entre deux actions.
        private int _randomAction;  //Détermine l'aléatoire des actions.
        public Transform _mySelf;  //Le Boss.
        private GameObject _player; //Le PJ.
        public float _fightDistance;  //Distance à laquelle le Boss arrête d'avancer.
        public float _moveSpeed;  //Vitesse de déplacement du Boss.
        public Vector3 _bossDirection;  //Direction du Boss vers le PJ.
        public float _retreatDistance;  //Distance à laquelle le Boss recule.

        public AnimatorController_BossP1 b = null;

        [Header("Renforts")]
        public List<GameObject> _renforts;  //Liste des ennemis à faire spawn.
        public List<Transform> _renfortsSpawns;  //Liste des emplacements de spawn des ennemis.

        [Header("Tir de Couverture et Fou de la Gachette")]
        public GameObject _bullet; //Le projectile.
        public float _couvertureSpeed;  //La vitesse de déplacement du Boss lors de la couverture.
        private float _couvertureDuration; //La durée du déplacement du Boss lors de la couverture.
        public float _couvertureDurationOrigin;
        public Transform _bulletContainer;  //Parent des projectiles du Boss.
        public float _shootingAllonge;  // Distance à laquelle apparaissent les projectile (pour pas qu'ils sortent du bide du boss).
        public float _delayBetweenShotsOrigin;  
        private float _delayBetweenShots;  // Délai entre les balles de Fou de la Gachette.
        public List<Vector3> _bulletFury;  // Liste des trajectoires des projectiles de Fou de la Gachette.
        [HideInInspector] public Vector3 _currentTarget = Vector2.zero;  //Direction actuellement ciblée par le Boss lors de Fou de la Gachette.
        private bool _couverture;

        [Header("Attaque au CaC")]
        public float _attackCast;  //Délai avant l'attaque.
        public GameObject _attackZone; //Zone touchée par l'attaque.
        public GameObject _attackPos;  //Point d'apparition de l'attaque.

        [Header("Grenade")]
        public GameObject _grenade;  //Projectile de Grenade.
        public Vector3 _grenadeTarget; //Point ciblé par la Grenade.


        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _actionActive = false;
            _delay = _delayBetweenActions;
            _canGoDelay = false;
            _couvertureDuration = _couvertureDurationOrigin;
            _delayBetweenShots = _delayBetweenShotsOrigin;
            _couverture = false;

        }

        // Update is called once per frame
        void Update()
        {
            _bossDirection = (_player.transform.position - _mySelf.position);

            //Déplacement du Boss entre les actions.
            if (_canGoDelay == true)
            {
                if (_delay > 0)
                {
                    _delay -= Time.deltaTime;

                    if (Vector2.Distance(_mySelf.position, _player.transform.position) > _fightDistance)
                    {
                        _mySelf.position = Vector2.MoveTowards(_mySelf.position, _player.transform.position, _moveSpeed * Time.deltaTime);
                    }
                    else if (Vector2.Distance(_mySelf.position, _player.transform.position) < _retreatDistance && Vector2.Distance(_mySelf.position, _player.transform.position) < _fightDistance)
                    {
                        _mySelf.position = Vector2.MoveTowards(_mySelf.position, _player.transform.position, -_moveSpeed * Time.deltaTime);
                    }
                    else if (Vector2.Distance(_mySelf.position, _player.transform.position) < _fightDistance && Vector2.Distance(_mySelf.position, _player.transform.position) > _retreatDistance)
                    {
                        _mySelf.position = _mySelf.position;
                    }
                }
                else if (_delay <= 0)
                {
                    _delay = _delayBetweenActions;
                    _canGoDelay = false;
                    _actionActive = false;
                }
            }

            //Choix et application des actions.
            if (_actionActive == false)
            {
                _randomAction = Random.Range(1, 6);

                switch (_randomAction)
                {
                    case 1:
                        b.animator.SetBool("isShooting", false);
                        b.animator.SetBool("isGrenading", false);
                        b.animator.SetBool("isAttacking", false);
                        b.animator.SetBool("IsGunSlinger", false);
                        b.animator.SetBool("isCallingHelp", true);
                        Renforts();
                        _actionActive = true;
                        Debug.Log("Renforts");
                        break;

                    case 2:
                        b.animator.SetBool("isShooting", true);
                        b.animator.SetBool("isGrenading", false);
                        b.animator.SetBool("isAttacking", false);
                        b.animator.SetBool("IsGunSlinger", false);
                        b.animator.SetBool("isCallingHelp", false);
                        TirDeCouverture();
                        _actionActive = true;
                        Debug.Log("TirDeCouverture");
                        break;

                    case 3:
                        b.animator.SetBool("isShooting", false);
                        b.animator.SetBool("isGrenading", true);
                        b.animator.SetBool("isAttacking", false);
                        b.animator.SetBool("IsGunSlinger", false);
                        b.animator.SetBool("isCallingHelp", false);
                        Grenade();
                        _actionActive = true;
                        Debug.Log("Grenade");
                        break;

                    case 4:
                        b.animator.SetBool("isShooting", false);
                        b.animator.SetBool("isGrenading", false);
                        b.animator.SetBool("isAttacking", true);
                        b.animator.SetBool("IsGunSlinger", false);
                        b.animator.SetBool("isCallingHelp", false);
                        StartCoroutine(AttaqueCaC());
                        _actionActive = true;
                        Debug.Log("AttaqueCaC");
                        break;


                    case 5:
                        b.animator.SetBool("isShooting", false);
                        b.animator.SetBool("isGrenading", false);
                        b.animator.SetBool("isAttacking", false);
                        b.animator.SetBool("IsGunSlinger", true);
                        b.animator.SetBool("isCallingHelp", false);
                        StartCoroutine(FouDeLaGachette());
                        _actionActive = true;
                        Debug.Log("FouDeLaGachette");
                        break;
                }
            }

            //Déplacement de "Tir de couverture".
            if (_couverture == true && _couvertureDuration > 0)
            {
                _couvertureDuration -= Time.deltaTime;

                _mySelf.position = Vector2.MoveTowards(_mySelf.position, _player.transform.position, -_couvertureSpeed * Time.deltaTime);
            }
            else if (_couverture == true && _couvertureDuration <= 0)
            {
                _couvertureDuration = _couvertureDurationOrigin;
                _couverture = false;
                _canGoDelay = true; 
            }
        }

        //Effet de "Renforts".
        private void Renforts()
        {
            for (int i = 0; i < _renforts.Count; i++)
            {
                Instantiate(_renforts[i], _renfortsSpawns[i]);
            }

            _canGoDelay = true;
        }

        //Effet de "Tir de couverture".
        private void TirDeCouverture()
        {
            _currentTarget = (_player.transform.position - _mySelf.position);

            Instantiate(_bullet, _mySelf.position + _currentTarget.normalized * _shootingAllonge, _mySelf.rotation, _bulletContainer);

            _couverture = true; //Permet de lancer le déplacement de "Tir de Couverture" qui se trouve dans l'Update.
        }

        //Effet de "Grenade".
        private void Grenade()
        {
            _grenadeTarget = _player.transform.position;

            Instantiate(_grenade, _mySelf.position + _grenadeTarget.normalized * _shootingAllonge, _mySelf.rotation, _bulletContainer);

            _canGoDelay = true;
        }

        //Effet de "AttaqueCaC".
        private IEnumerator AttaqueCaC()
        {
            yield return new WaitForSeconds(_attackCast);

            Instantiate(_attackZone, _attackPos.transform.position + _bossDirection.normalized * 2, _attackPos.transform.rotation, _attackPos.transform);
            _attackPos.GetComponent<Scr_BossP1AttackPos>()._attackSet = true;

            yield return new WaitForSeconds(0.5f);

            _canGoDelay = true;
        }

        //Effet de "Fou de la Gachette".
        private IEnumerator FouDeLaGachette()
        {
            for (int i = 0; i < _bulletFury.Count; i++)
            {
                _currentTarget = (_bulletFury[i] - _mySelf.position);
                Instantiate(_bullet, _mySelf.position + _currentTarget.normalized * _shootingAllonge, _mySelf.rotation, _bulletContainer);
                yield return new WaitForSeconds(_delayBetweenShots);
            }

            _canGoDelay = true;
        }


    }
}

