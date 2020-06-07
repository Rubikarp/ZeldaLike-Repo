using System.Collections;
using UnityEngine;
using Game;

namespace Ennemis
{
    public class Scr_EnnemisLifeSystem : MonoBehaviour, Int_EnnemisLifeSystem
    {
        [Header("Marked")]
        public GameObject _logoMarked = null;
        public bool IsBleeding
        {
            get { return _isMarked; }
            set { _isMarked = value; }
        }
        public bool _isMarked = false;
        public float _markedDuration = 2f;
        private float _markedCooldown = 0f;

        [Header("Data")]
        public GameObject Ennemis = null;
        public Rigidbody2D body = null;
        public GameObject _healObject;
        public SpriteRenderer SpriteRenderer;

        [Header("Statistiques")]
        public int _life = 5;
        public bool _isDead = false;
        public bool _isTakingDamage = false;
        public float _dyingDuration = 1f;
        public float knockbackSensibility = 1f;
        [Range(0, 100)] public float _healSpawnChance = 25f;
        private bool haveLoot = false;

        private SoundManager sound;

        private void Awake()
        {
            sound = SoundManager.Instance;
        }

        private void Update()
        {
            if (_life <= 0)
            {
                _isDead = true;

                if(Random.Range(0, 100) < _healSpawnChance && !haveLoot)
                {
                    Instantiate(_healObject, transform.position, transform.rotation);
                    haveLoot = true;
                }

                Destroy(Ennemis, _dyingDuration);
            }

            if (_isMarked && _markedCooldown <= 0)
            {
                _isMarked = false;
                _logoMarked.SetActive(false);
            }

            _markedCooldown -= Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Attack") || collision.gameObject.CompareTag("Knife"))
            {
                if (collision.gameObject.CompareTag("Knife"))
                {
                    GetMarked();
                    sound.PlaySound("Mark");
                }

                Vector2 knockBackDirection = -(collision.transform.position - this.transform.position).normalized;

                Int_Damage attackData = collision.gameObject.GetComponent<Int_Damage>();

                float knockbackSpeed = knockbackSensibility * attackData.KnockbackPower;

                if(!_isTakingDamage)
                {
                    if(collision.name == "HeavyAttack" && _isMarked)
                    {
                        StartCoroutine(TakingDamage(attackData.Damage * 2, body, knockBackDirection, knockbackSpeed, attackData.StunDuration));
                    }

                    StartCoroutine(TakingDamage(attackData.Damage, body, knockBackDirection, knockbackSpeed, attackData.StunDuration));
                }

            }
        }
 
        public void GetMarked()
        {
            _isMarked = true;
            _markedCooldown = _markedDuration;
            _logoMarked.SetActive(true);

        }

        public IEnumerator TakingDamage(int damage, Rigidbody2D body, Vector2 knockBackDirection, float knockbackSpeed, float stunDuration)
        {
            _isTakingDamage = true;
            _life -= damage;
            SpriteRenderer.color = Color.red;

            while (0 < stunDuration) // boucle durant la durée du dash
            {
                stunDuration -= Time.deltaTime;

                body.velocity = knockBackDirection * knockbackSpeed;

                // Retour à la prochaine frame
                yield return new WaitForEndOfFrame();
            }

            body.velocity = Vector2.zero;
            _isTakingDamage = false;
            SpriteRenderer.color = Color.white;
        }
    }
}