using UnityEngine;
using Game;

namespace Management
{
    public class AnimatorManager_Player : MonoBehaviour
    {
        [Header("Auto Components")]
        [SerializeField] SpriteRenderer _spritRend = null;
        [SerializeField] Animator _playerAnimator = null;
        [SerializeField] Animator _fxAnimator = null;
        [SerializeField] InputManager _input = null;
        [SerializeField] Movement_2D_TopDown _movement = null;
        [SerializeField] Rigidbody2D _rgb = null;

        [Header("Components")]
        [SerializeField] Scr_PlayerLifeSystem _playerLife = null;
        [SerializeField] Scr_FormeHandler _formeManager = null;

        [Space(10)]
        //Humain
        public bool _isHumain = false;
        //lourd
        public bool _isAgile = false;
        //Agile
        public bool _isLourd = false;

        void Start()
        {
            _spritRend = this.gameObject.GetComponent<SpriteRenderer>();
            _playerAnimator = this.gameObject.GetComponent<Animator>();
            _input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
        }
        
        void Update()
        {
            //Flip Horizontal
            if(_input._CharacterDirection.x < 0)
            {
                _spritRend.flipX = true;
            }
            else if (_input._CharacterDirection.x > 0)
            {
                _spritRend.flipX = false;
            }

            //Valeur des actions
            _playerAnimator.SetFloat("MouvY", _input._CharacterDirection.y);
            _playerAnimator.SetFloat("MouvSpeed", _rgb.velocity.magnitude);
            _playerAnimator.SetBool("IsTakingDamage", _playerLife._isTakingDamage);

            //Bool pour la forme
            if(_formeManager._switchForm == Scr_FormeHandler.Forme.Heavy && !_isLourd)
            {
                _playerAnimator.SetBool("IsLourd", true);
                _playerAnimator.SetBool("IsAgile", false);
                _playerAnimator.SetBool("IsHuman", false);

                _isHumain = false;
                _isAgile = false;
                _isLourd = true;

                _playerAnimator.SetTrigger("GoLourd");
                _fxAnimator.SetTrigger("GoLourd");
            }
            else
            if (_formeManager._switchForm == Scr_FormeHandler.Forme.Agile && !_isAgile)
            {
                _playerAnimator.SetBool("IsLourd", false);
                _playerAnimator.SetBool("IsAgile", true);
                _playerAnimator.SetBool("IsHuman", false);

                _isAgile = true;
                _isHumain = false;
                _isLourd = false;

                _playerAnimator.SetTrigger("GoAgile");
                _fxAnimator.SetTrigger("GoAgile");

            }
            else
            if(_formeManager._switchForm == Scr_FormeHandler.Forme.Humain && !_isHumain)
            {
                _playerAnimator.SetBool("IsLourd", false);
                _playerAnimator.SetBool("IsAgile", false);
                _playerAnimator.SetBool("IsHuman", true);

                _isHumain = true;
                _isAgile = false;
                _isLourd = false;

                _playerAnimator.SetTrigger("GoHumain");
                _fxAnimator.SetTrigger("GoHumain");

            }
            _fxAnimator.SetBool("IsChargingForward",_movement._isBoosted);
        }

        //AttackTrigger
        public void TriggerAttack()
        {
            _playerAnimator.SetTrigger("Attack");
        }

        public void TriggerDeath()
        {
            _playerAnimator.SetTrigger("Die");
        }
        public void TriggerTP()
        {
            _fxAnimator.SetTrigger("OnTP");
        }

    }
}