using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Actif_HeavyAttack : MonoBehaviour
    {
        [SerializeField] private Movement_2D_TopDown _PlMovement = null;

        public GameObject _attackObj;
        public Transform _attackContainer;
        public Transform _attackPos;

        [SerializeField] private bool _canAttack;

        public float _attackCooldown;

        void Start()
        {
            _canAttack = true;
        }

        void Update()
        {
            if (Input.GetButtonDown("Attack") && _canAttack == true)
            {
                _canAttack = false;
                _PlMovement._canMove = false;

                _PlMovement.Immobilize();

                Instantiate(_attackObj, _attackPos.position, _attackPos.rotation, _attackContainer);

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