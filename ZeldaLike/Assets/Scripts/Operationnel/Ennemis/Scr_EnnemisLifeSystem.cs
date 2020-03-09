using Game;
using System.Collections;
using UnityEngine;

namespace Ennemis
{
    public class Scr_EnnemisLifeSystem : MonoBehaviour
    {
        public GameObject Ennemis = null;
        public Rigidbody2D body = null;
        public float _dyingDuration = 0f;
        public bool _isTakingDamage = false;

        [Header("Statistiques")]
        public int _life = 5;

        public float knockbackSensibility;

        private void Update()
        {
            if (_life <= 0)
            {
                Destroy(Ennemis, _dyingDuration);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Attack") || collision.gameObject.CompareTag("Knife"))
            {
                Vector2 knockBackDirection = -(collision.transform.position - this.transform.position).normalized;

                Int_Damage attackData = collision.GetComponent<Int_Damage>();

                float knockbackSpeed = knockbackSensibility * attackData.KnockbackPower;

                if(!_isTakingDamage)
                {
                    StartCoroutine(TakingDamage(attackData.Damage, body, knockBackDirection, knockbackSpeed, attackData.StunDuration));
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
    }
}