using System.Collections;
using UnityEngine;
using Management;

using UnityEngine.Events;


namespace Game
{
    public class Actif_HeavyAttack : MonoBehaviour
    {
        [SerializeField] private Movement_2D_TopDown _PlMovement = null;
        [SerializeField] private InputManager _input = null;
        [SerializeField] private AnimatorManager_Player _animator = null;
        [SerializeField] private SoundManager sound;

        public GameObject _attackObj;
        public Transform _attackContainer;
        public Transform _attackPos;
        [SerializeField] private float _attackDur = 0.2f;

        public bool _canAttack;

        public float _attackCooldown;
        public float _attackDelay;

        void Awake()
        {
            sound = SoundManager.Instance;
        }

        void Start()
        {
            _canAttack = true;
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();

        }

        void Update()
        {
            if (_input._attack && _canAttack == true)
            {
                _canAttack = false;
                _PlMovement._canMove = false;

                StartCoroutine(Attaque(_attackDur));

                sound.PlaySound("AttackDino");

            }
        }

        IEnumerator Attaque(float _attackDur)
        {
            _animator.TriggerAttack();

            _input.DesactivateControl();
            _PlMovement.Immobilize();

            yield return new WaitForSeconds(_attackDelay);

            Instantiate(_attackObj, _attackPos.position, _attackPos.rotation, _attackPos.transform);

            yield return new WaitForSeconds(_attackDur);

            _input.ReActivateControl();

            yield return new WaitForSeconds(_attackCooldown);

            _canAttack = true;

        }

        private void OnEnable()
        {
            _canAttack = true;
        }
    }
}