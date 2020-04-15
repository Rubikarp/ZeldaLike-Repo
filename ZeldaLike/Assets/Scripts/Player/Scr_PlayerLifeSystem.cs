using System.Collections;
using UnityEngine;
using Management;

namespace Game
{
    public class Scr_PlayerLifeSystem : MonoBehaviour
    {
        [Header("Component")]
        public GameObject Avatar = null;
        public Rigidbody2D body = null;
        public AnimatorManager_Player _animator = null;
        public ScreenShake _scrShake = null;
        public InputManager _input = null;
        private bool dead = false;

        [Header("Statistiques")]
        public string[] _playerAttack;

        public int _life = 5;
        public bool _isTakingDamage = false;
        public float knockbackSensibility = 1f;

        private void Start()
        {
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
        }

        private void Update()
        {
            Living();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ennemis"))
            {
                Vector2 knockBackDirection = -(collision.transform.position - this.transform.position).normalized;

                Int_Damage attackData = collision.gameObject.GetComponent<Int_Damage>();
                
                float knockbackSpeed;
                if (attackData == null)
                {
                    knockbackSpeed = knockbackSensibility;
                }
                else
                {
                    knockbackSpeed = knockbackSensibility * attackData.KnockbackPower;
                }

                if (!_isTakingDamage)
                {
                    StartCoroutine(TakingDamage(attackData == null ? 1 :attackData.Damage, body, knockBackDirection, knockbackSpeed, attackData == null? 1f :attackData.StunDuration));
                }
            }

            if (collision.gameObject.CompareTag("Attack"))
            {
                bool isMyAttack = false;

                for (int i = 0; i < _playerAttack.Length; i++)
                {
                    if (collision.gameObject.name == _playerAttack[i] || collision.gameObject.name == _playerAttack[i] +"(Clone)")
                    {
                        isMyAttack = true;
                    }
                }

                if (!isMyAttack)
                {
                    Vector2 knockBackDirection = -(collision.transform.position - this.transform.position).normalized;

                    Int_Damage attackData = collision.gameObject.GetComponent<Int_Damage>();

                    float knockbackSpeed = knockbackSensibility * attackData.KnockbackPower;

                    if (!_isTakingDamage)
                    {
                        StartCoroutine(TakingDamage(attackData.Damage, body, knockBackDirection, knockbackSpeed, attackData.StunDuration));
                    }
                }
            }
        }

        public IEnumerator TakingDamage(int damage, Rigidbody2D body, Vector2 knockBackDirection, float knockbackSpeed, float stunDuration)
        {
            _isTakingDamage = true;
            _life -= damage;
            _scrShake.trauma = 0.5f;

            while (0 < stunDuration) // boucle durant la durée du dash
            {
                stunDuration -= Time.deltaTime;

                body.velocity = knockBackDirection * knockbackSpeed;

                // Retour à la prochaine frame
                yield return new WaitForEndOfFrame();
            }

            body.velocity = Vector2.zero;
            _isTakingDamage = false;
        }

        private void Living()
        {
            if (_life <= 0 && !dead)
            {
                _animator.TriggerDeath();
                _input.DesactivateControl();

                dead = true;
                //Destroy(Avatar, /*_deathAnim.clip.length*/ 10f);
            }
        }
    }
}