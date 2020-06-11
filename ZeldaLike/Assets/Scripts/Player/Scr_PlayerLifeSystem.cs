using System.Collections;
using UnityEngine;
using Management;
using UnityEngine.Rendering;

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
        public Scr_FormeHandler _forme = null;
        public Volume lowLife = null;
        private SoundManager sound;
        public PlayerLife playerLife;
        public Scr_Player_HUD hud;

        [Header("Statistiques")]
        public Vector2 _respawnPoint = Vector2.zero;
        public string[] _playerAttack;

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
            if (Time.timeScale != 0)
            {
                hud.lifeBarUpdate(playerLife.life);
            }
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
                    sound.PlaySound("Perte Vie");
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
            playerLife.life -= damage;

            if (Time.timeScale != 0)
            {
                hud.lifeBarUpdate(playerLife.life);
            }

            if (_scrShake != null)
            {
                _scrShake.trauma = 0.5f;
            }

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
            if (playerLife.life <= 0 && !Dying)
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

            if (playerLife.life == 2)
            {
                lowLife.weight = 0.5f;
            }
            else
            if (playerLife.life < 2)
            {
                lowLife.weight = 1f;
            }
            else
            {
                lowLife.weight = 0f;
            }
        }
        
        private void WillDie()
        {
            isDead = true;
        }

        public void Respawn()
        {
            playerLife.life = playerLife.maxlife;
            isDead = false;
            Dying = false;

            _animator.Respawn();
            _input.ReActivateControl();
            body.position = _respawnPoint;
        }

        public void Heal()
        {
            if (playerLife.life < playerLife.maxlife)
            {
                playerLife.life ++;
                sound.PlaySound("Coeur ramassé");
            }

            if (Time.timeScale != 0)
            {
                hud.lifeBarUpdate(playerLife.life);
            }
        }

        public void MaxHeal()
        {
            if (playerLife.maxlife < 9)
            {
                playerLife.maxlife = playerLife.maxlife + 1;
                playerLife.life = playerLife.maxlife;
                sound.PlaySound("Amélioration Vie");
            }

            if (Time.timeScale != 0)
            {
                hud.lifeBarUpdate(playerLife.life);
            }
        }
    }
}