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
        public Scr_FormeHandler _forme;
        private SoundManager sound;

        [Header("Statistiques")]
        public Vector2 _respawnPoint = Vector2.zero;
        public string[] _playerAttack;

        public int _life = 6;
        public int _maxlife = 6;
        public bool _isTakingDamage = false;
        public bool _isVunerable = true;
        public float knockbackSensibility = 1f;

        public bool isDead = false;
        public bool Dying = false;

        void Awake()
        {
            sound = SoundManager.Instance;
        }

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

                if (!_isTakingDamage & _isVunerable)
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

                    if (!_isTakingDamage & _isVunerable)
                    {
                        StopAllCoroutines();
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
            if (_life <= 0 && !Dying)
            {
                Dying = true;
                _animator.TriggerDeath();
                _input.DesactivateControl();

                if (_forme._switchForm == Scr_FormeHandler.Forme.Humain)
                {
                    sound.PlaySound("DeathHumain");
                }
                else
                if (_forme._switchForm == Scr_FormeHandler.Forme.Agile)
                {
                    sound.PlaySound("DeathFeline");
                }
                else
                if (_forme._switchForm == Scr_FormeHandler.Forme.Heavy)
                {
                    sound.PlaySound("DeathDino");
                }

                Invoke("WillDie", 0.8f);
            }
        }
        
        private void WillDie()
        {
            isDead = true;

        }

        public void Respawn()
        {
            _life = _maxlife;
            isDead = false;
            Dying = false;

            _animator.Respawn();
            _input.ReActivateControl();
            body.position = _respawnPoint;
        }
    }
}