using System.Collections;
using UnityEngine;
using Management;

namespace Game
{
    public class Actif_HeavyAttack : MonoBehaviour
    {
        [SerializeField] private Movement_2D_TopDown _PlMovement = null;
        [SerializeField] private InputManager _input = null;
        [SerializeField] private AnimatorManager_Player _animator = null;


        public GameObject _attackObj;
        public Transform _attackContainer;
        public Transform _attackPos;

        [SerializeField] private bool _canAttack;

        public float _attackCooldown;

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

                _PlMovement.Immobilize();

                Instantiate(_attackObj, _attackPos.position, _attackPos.rotation, _attackContainer);
                _animator.TriggerAttack();

                StartCoroutine(AttaqueDelay());
            }
        }

        IEnumerator AttaqueDelay()
        {
            yield return new WaitForSeconds(_attackCooldown);
            _canAttack = true;
        }
    }
}