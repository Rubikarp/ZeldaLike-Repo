using System.Collections;
using UnityEngine;
using Management;
using Ennemis;

namespace Game
{
    public class Actif_AgileAttack : MonoBehaviour
    {
        [SerializeField] private InputManager _input = null;
        [SerializeField] private GameObject _Avatar = null;
        [SerializeField] private GameObject _HurtBox = null;
        [SerializeField] private Scr_PlayerLifeSystem lifeSystPlayer = null;
        [SerializeField] private AnimatorManager_Player _animator = null;
        [SerializeField] private Movement_2D_TopDown _PlMovement = null;
        [SerializeField] private Bond_zone _bondDetecZone = null;
        [SerializeField] private SoundManager sound;

        private Rigidbody2D _rgb = null;

        [Header("Attaque data")]
        public GameObject _attackObj;
        [SerializeField] private float _attackDur = 0.2f;
        public Transform _attackContainer;
        public Transform _attackPos;
        public bool _canAttack = true;
        [HideInInspector] public bool _stopJump;
        public Collider2D _hardCollider;
        public Collider2D _jumpCollider;

        [Header("Bond")]
        public GameObject _bondObj;
        public float _jumpRange = 25f;
        public float _jumpLargeur = 2f;
        private bool _isBleeding = false;

        public float _bondSpeed = 30;
        public float _invulnerabiltyTimer = 1f;
        public float _invulnerabiltyTime;

        public float _canAttackTimer = 0.2f;
        public float _canAttackTime;

        public float _bondMaxDur = 1;
        public float _bondMaxDist = 3;
        public float _attackCooldown = 1;

        void Awake()
        {
            sound = SoundManager.Instance;
        }

        private void Start()
        {
            _rgb = _Avatar.GetComponent<Rigidbody2D>();
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
            _stopJump = false;
        }

        private void Update()
        {
            if (_input._attack && _canAttack)
            {
                if (_bondDetecZone._detectedEnnemisList.Count != 0)
                {
                    _isBleeding = false;

                    foreach (GameObject ennemis in _bondDetecZone._detectedEnnemisList)
                    {
                        if (ennemis.GetComponent<Int_EnnemisLifeSystem>().IsBleeding)
                        {
                            _isBleeding = true;
                        }
                    }

                    if (_isBleeding)
                    {
                        _canAttackTime = _canAttackTimer;
                        _canAttack = false;

                        StartCoroutine(Bond(_Avatar.transform.position, _input._CharacterDirection, _bondSpeed, _bondMaxDur));
                        sound.PlaySound("BondFeline");
                        _isBleeding = false;
                    }
                    else
                    {
                        StartCoroutine(AttackClassique(_attackDur));
                        sound.PlaySound("AttackFeline");
                    }

                }
                else
                {
                    StartCoroutine(AttackClassique(_attackDur));
                    sound.PlaySound("AttackFeline");
                }            
            }
            else
            {
                //invulnerability timer
                _invulnerabiltyTime -= Time.deltaTime;
                if (_invulnerabiltyTime <= 0)
                {
                    _HurtBox.SetActive(true);
                }

                //canAttack timer
                if (_canAttackTime <= 0)
                {
                    _canAttack = true;
                }
                else
                {
                    _canAttackTime -= Time.deltaTime;
                }
            }
        }

        private IEnumerator Bond(Vector3 me, Vector2 attaqueDir, float speed, float maxDuration)
        {
            float distanceMade = Vector2.Distance(me, _rgb.position);
            attaqueDir = attaqueDir.normalized;

            GameObject bond = Instantiate(_bondObj, _attackPos.position, _attackPos.rotation, _attackPos.transform);

            lifeSystPlayer._isVunerable = false;
            _HurtBox.SetActive(false);
            _invulnerabiltyTime = _invulnerabiltyTimer;
            _hardCollider.enabled = false;
            _jumpCollider.enabled = true;

            while (distanceMade < _bondMaxDist) // boucle durant la durée du dash
            {
                _rgb.velocity += attaqueDir * speed; //* Time.deltaTime;

                distanceMade = Vector2.Distance(me, _rgb.position);
                maxDuration -= Time.deltaTime;

                if (0 > maxDuration)
                {
                    _rgb.velocity = Vector2.zero;
                    lifeSystPlayer._isVunerable = true;
                    break;
                }

                if( _stopJump == true)
                {
                    _rgb.velocity = Vector2.zero;
                    lifeSystPlayer._isVunerable = true;
                    break;
                }

                yield return new WaitForEndOfFrame();  
            }

            yield return null;

            _hardCollider.enabled = true;
            _jumpCollider.enabled = false;
            _stopJump = false;
            lifeSystPlayer._isVunerable = true;
            _rgb.velocity = Vector2.zero;
            Destroy(bond);
        }

        private IEnumerator AttackClassique(float attackDur)
        {
            _animator.TriggerAttack();
            _canAttackTime = _canAttackTimer;
            _canAttack = false;
            _input.DesactivateControl();

            Instantiate(_attackObj, _attackPos.position, _attackPos.rotation, _attackPos.transform);
            
            while(attackDur > 0)
            {
                _PlMovement.Immobilize();
                attackDur -= Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            _input.ReActivateControl();
        }

        private void OnDisable()
        {
            _HurtBox.SetActive(true);
            _canAttack = true;
            lifeSystPlayer._isTakingDamage = false;
            lifeSystPlayer._isVunerable = true;
        }

    }
}