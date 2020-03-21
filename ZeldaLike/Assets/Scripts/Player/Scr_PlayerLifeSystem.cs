using System.Collections;
using UnityEngine;

namespace Game
{
    public class Scr_PlayerLifeSystem : MonoBehaviour
    {
        [Header("Component")]
        public GameObject Avatar = null;

        public Rigidbody2D body = null;
        public Animation _deathAnim = null;

        [Header("Statistiques")]
        public string[] _playerAttack;

        public int _life = 5;
        public bool _isTakingDamage = false;
        public float knockbackSensibility = 1f;

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

                float knockbackSpeed = knockbackSensibility * attackData.KnockbackPower;

                if (!_isTakingDamage)
                {
                    StartCoroutine(TakingDamage(attackData.Damage, body, knockBackDirection, knockbackSpeed, attackData.StunDuration));
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
            if (_life <= 0)
            {
                _deathAnim.Play();
                Destroy(Avatar, _deathAnim.clip.length);
            }
        }
    }
}