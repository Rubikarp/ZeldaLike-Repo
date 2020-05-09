using System.Collections;
using UnityEngine;
using Management;
using UnityEngine.Events;

namespace Game
{
    public class Actif_AgileAttack : MonoBehaviour
    {
        [SerializeField] private InputManager _input = null;
        [SerializeField] private GameObject _Avatar = null;
        [SerializeField] private GameObject _HurtBox = null;
        [SerializeField] private AnimatorManager_Player _animator = null;
        [SerializeField] private Movement_2D_TopDown _PlMovement = null;
        [SerializeField] private Bond_zone _bondDetecZone = null;
        [SerializeField] private SoundManager sound;

        public UnityEvent Actif;
        public UnityEvent SpeBond;

        private Rigidbody2D _rgb = null;
        private Transform _myTranfo = null;

        [Header("Attaque data")]
        public GameObject _attackObj;
        [SerializeField] private float _attackDur = 0.2f;
        public Transform _attackContainer;
        public Transform _attackPos;
        public bool _canAttack = true;

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
        public float _bondEndDist = 3;
        public float _attackCooldown = 1;

        void Awake()
        {
            sound = SoundManager.Instance;
        }

        private void Start()
        {
            _myTranfo = this.transform;
            _rgb = _Avatar.GetComponent<Rigidbody2D>();
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
        }

        private void Update()
        {
            if (_input._attack && _canAttack)
            {
                GameObject Target = _bondDetecZone.NearestEnnemis();

                if(Target == null)
                {
                    StartCoroutine(AttackClassique(_attackDur));
                    sound.PlaySound("AttackFeline");

                }
                else
                {
                    _isBleeding = Target.GetComponent<Int_EnnemisLifeSystem>().IsBleeding;

                    if (_isBleeding)
                    {
                        _canAttackTime = _canAttackTimer;
                        _canAttack = false;

                        StartCoroutine(Bond(_Avatar.transform.position, Target, _bondSpeed, _bondMaxDur));
                        sound.PlaySound("BondFeline");
                        _isBleeding = false;
                    }
                    else
                    {
                        StartCoroutine(AttackClassique(_attackDur));
                        sound.PlaySound("AttackFeline");

                    }
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

                //invulnerability timer
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

        private IEnumerator Bond(Vector3 me, GameObject cible, float speed, float maxDuration)
        {
            float distance = Vector2.Distance(_rgb.position, cible.transform.position);
            Vector2 direction = (cible.transform.position - me).normalized;

            GameObject bond = Instantiate(_bondObj, _attackPos.position, _attackPos.rotation, _attackPos.transform);

            _HurtBox.SetActive(false);
            _invulnerabiltyTime = _invulnerabiltyTimer;

            while (_bondEndDist < distance) // boucle durant la durée du dash
            {
                _rgb.position += direction * speed * Time.deltaTime;

                if (cible == null)
                {
                    _rgb.velocity = Vector2.zero;
                    break;
                }

                distance = Vector2.Distance(_rgb.position, cible.transform.position);
                direction = (cible.transform.position - me).normalized;

                maxDuration -= Time.deltaTime;

                if (0 > maxDuration)
                {
                    _rgb.velocity = Vector2.zero;
                    break;
                }

                yield return new WaitForEndOfFrame();  
            }

            yield return null;

            _rgb.velocity = Vector2.zero;
            Destroy(bond);
        }

        private IEnumerator AttackClassique(float attackDur)
        {
            _animator.TriggerAttack();
            _canAttackTime = _canAttackTimer;
            _canAttack = false;

            Instantiate(_attackObj, _attackPos.position, _attackPos.rotation, _attackPos.transform);
            
            while(attackDur > 0)
            {
                _PlMovement.Immobilize();
                attackDur -= Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }
        }

        private void OnDisable()
        {
            _HurtBox.SetActive(true);
            _canAttack = true;
        }

    }
}