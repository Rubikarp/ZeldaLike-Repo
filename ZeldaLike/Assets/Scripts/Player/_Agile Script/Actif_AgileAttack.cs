using Management;
using System.Collections;
using UnityEngine;

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

        public LayerMask _ennemiLayer;
        private bool _isBleeding = false;

        public float _bondSpeed = 30;
        public float _invulnerabiltyTimer = 1f;
        public float _invulnerabiltyTime;

        public float _bondCooldown = 1;
        public float _attackCooldown = 1;

        private void Start()
        {
            _myTranfo = this.transform;
            _rgb = _Avatar.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Attack") && _canAttack)
            {
                RaycastHit2D hit = Physics2D.Raycast(_rgb.position + _input._CharacterDirection * 5, _input._CharacterDirection, _jumpRange, _ennemiLayer);
                if (hit.collider != null)
                {
                    //Est ce que je touche un ennemi ?
                    if (hit.transform.CompareTag("Ennemis"))
                    {
                        _canAttack = false;
                        _isBleeding = false;

                        GameObject _actualTarget = Physics2D.Raycast(_rgb.position + _input._CharacterDirection * 5, _input._CharacterDirection, _jumpRange, _ennemiLayer).transform.gameObject;
                        _isBleeding = _actualTarget.GetComponentInChildren<Int_EnnemisLifeSystem>().IsBleeding;

                        //saigne t'il?
                        if (_isBleeding)
                        {
                            StartCoroutine(Bond(_input._CharacterDirection, _actualTarget.transform.position, _bondSpeed, 3));

                            StartCoroutine(AttaqueDelay(_bondCooldown));
                        }
                        //attaque classique
                        else
                        {
                            Instantiate(_attackObj, _attackPos.position, _attackPos.rotation, _Avatar.transform);
                            StartCoroutine(AttaqueDelay(_attackCooldown));
                        }
                    }
                }
                //attaque classique dans le vide
                else
                {
                    _canAttack = false;

                    //attaque classique
                    Instantiate(_attackObj, _attackPos.position, _attackPos.rotation, _Avatar.transform);
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

        private IEnumerator AttaqueDelay(float Cooldown)
        {
            yield return new WaitForSeconds(Cooldown);
            _canAttack = true;
        }

        private IEnumerator Bond(Vector2 direction, Vector3 cible, float speed, float maxDuration)
        {
            float distance = Vector2.Distance(_rgb.position, cible);

            GameObject bond = Instantiate(_bondObj, _attackPos.position, _attackPos.rotation, _Avatar.transform);

            _HurtBox.SetActive(false);
            _invulnerabiltyTimer = _invulnerabiltyTime;

            while (2 < distance) // boucle durant la durée du dash
            {
                _rgb.position += direction * speed * Time.deltaTime;

                distance = Vector2.Distance(_rgb.position, cible);
                maxDuration -= Time.deltaTime;
                //Debug.Log("distance = " + distance + "maxDuration = " + maxDuration);

                if (0 > maxDuration)
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