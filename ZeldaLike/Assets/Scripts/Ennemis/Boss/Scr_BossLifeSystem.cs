using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Game;

namespace Ennemis
{
    public class Scr_BossLifeSystem : MonoBehaviour, Int_EnnemisLifeSystem
    {
        [Header("Componnents")]
        public GameObject _Boss = null;
        public Rigidbody2D _body = null;
        public GameObject _logoMarked = null;
        public Slider _lifeBar = null;
        [Space(10)]

        [Header("Variable à lire")]
        public bool _isTakingDamage = false;
        public bool _isMarked = false;
        public bool _isVunerable = true;
        public bool IsBleeding
        {
            get { return _isMarked; }
            set { _isMarked = value; }
        }
        private float _markedTimer;
        [Space(10)]

        [Header("Variable à Tweek")]
        public int _life = 5;
        public float _dyingDuration = 0f;
        public float knockbackSensibility = 0.5f;
        public float _markedDuration = 2f;
        private int _maxLife;
        [SerializeField] private Vector3 _startingPos;

        private void Start()
        {
            _maxLife = _life;
            _lifeBar.minValue = 0;
            _lifeBar.maxValue = _life;
            _startingPos = _Boss.transform.position;
        }

        private void Update()
        {
            if (_life <= 0)
            {
                _lifeBar.value = 0;
                Destroy(_Boss, _dyingDuration);
            }
            else
            {
                _lifeBar.value = _life;
            }

            if (_isMarked && _markedTimer <= 0)
            {
                _isMarked = false;
                _logoMarked.SetActive(false);
            }
            else
            {
                _markedTimer -= Time.deltaTime;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isVunerable)
            {
                if (collision.gameObject.CompareTag("Attack") || collision.gameObject.CompareTag("Knife"))
                {
                    if (collision.gameObject.CompareTag("Knife"))
                    {
                        GetMarked();
                    }

                    Vector2 knockBackDirection = -(collision.transform.position - this.transform.position).normalized;
                    _body.velocity = Vector2.zero;

                    Int_Damage attackData = collision.gameObject.GetComponent<Int_Damage>();

                    float knockbackSpeed = knockbackSensibility * attackData.KnockbackPower;

                    if (!_isTakingDamage)
                    {
                        StartCoroutine(TakingDamage(attackData.Damage, _body, knockBackDirection, knockbackSpeed, attackData.StunDuration));
                    }
                }
            }
        }

        public void GetMarked()
        {
            _isMarked = true;
            _markedTimer = _markedDuration;
            _logoMarked.SetActive(true);

        }

        public IEnumerator TakingDamage(int damage, Rigidbody2D body ,Vector2 knockBackDirection, float knockbackSpeed, float stunDuration)
        {
            _isTakingDamage = true;
            _life -= damage;

            while (0 < stunDuration) // boucle durant la durée du dash
            {
                stunDuration -= Time.deltaTime;

                _body.velocity = knockBackDirection * knockbackSpeed;

                // Retour à la prochaine frame
                yield return new WaitForEndOfFrame();
            }

            _body.velocity = Vector2.zero;
            _isTakingDamage = false;
        }

        public void ResetBoss()
        {
            _life = _maxLife;
            _Boss.transform.position = _startingPos;
        }

    }
}