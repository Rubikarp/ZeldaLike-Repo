using System.Collections;
using System.Collections.Generic;
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
        private Rigidbody2D _rgb = null;
        private Transform _myTranfo = null;
        public float _jumpRange = 25f;

        [Header("Attaque data")]
        public GameObject _attackObj;
        public Transform _attackContainer;
        public Transform _attackPos;
        [SerializeField] private bool _canAttack = true;

        [Header("Bond")]
        public GameObject _bondObj;
        public GameObject _playerHurtbox;
        public GameObject lastTarget;
        [SerializeField] RaycastHit2D isBleedingEnnemis;

        [Space(10)]
        [SerializeField] bool _isEnnemis = false;
        [Space(10)]

        public float _bondSpeed = 30;
        public float _invulnerabiltyTimer = 1f;
        public float _invulnerabiltyTime;

        public float _bondCooldown = 1;
        public float _attackCooldown = 1;

        void Start()
        {
            _myTranfo = this.transform;
            _rgb = _Avatar.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            isBleedingEnnemis = Physics2D.Raycast(_rgb.position + _input._CharacterDirection * 5, _input._CharacterDirection, _jumpRange);

            //Debug.Log(isBleedingEnnemis.transform.gameObject.name);

            if (Input.GetButtonDown("Attack"))
            {
                lastTarget = isBleedingEnnemis.transform.gameObject;
                _isEnnemis = lastTarget.CompareTag("Ennemis");

                if (_isEnnemis && _canAttack)
                {
                    _canAttack = false;

                    bool _isBleeding = false;
                    _isBleeding = isBleedingEnnemis.collider.gameObject.GetComponentInChildren<Int_EnnemisLifeSystem>().IsBleeding;

                    if (_isBleeding)
                    {
                        StartCoroutine(Bond(_input._CharacterDirection, isBleedingEnnemis.collider.transform.position, _bondSpeed, 3));                     

                        StartCoroutine(AttaqueDelay(_bondCooldown));
                    }
                }
                else if(_canAttack)
                {
                    _canAttack = false;

                    //attaque classique
                    Instantiate(_attackObj, _attackPos.position, _attackPos.rotation, _attackContainer);
                    StartCoroutine(AttaqueDelay(_attackCooldown));
                }
            }
            else
            {
                //invulnerability timer
                _invulnerabiltyTimer -= Time.deltaTime;
                if (_invulnerabiltyTimer <= 0)
                {
                    _HurtBox.SetActive(true);
                }
            }
        }

        IEnumerator AttaqueDelay(float Cooldown)
        {
            yield return new WaitForSeconds(Cooldown);
            _canAttack = true;
        }

        IEnumerator Bond(Vector2 direction, Vector3 cible, float speed, float maxDuration)
        {
            float distance = Vector2.Distance(_rgb.position, cible);

            GameObject bond = Instantiate(_bondObj, _attackPos.position, _attackPos.rotation, _Avatar.transform);

            _HurtBox.SetActive(false);
            _invulnerabiltyTimer = _invulnerabiltyTime;

            while ( 2 < distance) // boucle durant la durée du dash
            {
                _rgb.position += direction * speed * Time.deltaTime;

                distance = Vector2.Distance(_rgb.position, cible);
                maxDuration -= Time.deltaTime;
                //Debug.Log("distance = " + distance + "maxDuration = " + maxDuration);

                if(0 > maxDuration)
                {
                    break;
                }


                // Retour à la prochaine frame
                yield return new WaitForEndOfFrame();
            }

            yield return null;

            Destroy(bond);
            _canAttack = true;

        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(_myTranfo.position, _input._CharacterDirection.normalized.normalized * _jumpRange, Color.blue);
        }

        private void OnDisable()
        {
            _HurtBox.SetActive(true);
        }

        private void OnEnable()
        {
            _canAttack = true;
        }
    }
}