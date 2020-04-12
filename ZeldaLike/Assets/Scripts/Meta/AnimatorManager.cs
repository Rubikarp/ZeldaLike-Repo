using UnityEngine;
using Game;

namespace Management
{
    public class AnimatorManager : MonoBehaviour
    {
        [Header("Auto Components")]
        [SerializeField] SpriteRenderer _spritRend = null;
        [SerializeField] Animator _animator = null;
        [SerializeField] InputManager _input = null;

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
            _animator = this.gameObject.GetComponent<Animator>();
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
            _animator.SetFloat("MouvY", _input._CharacterDirection.y);
            _animator.SetFloat("MouvSpeed", _input._stickDirection.magnitude);
            _animator.SetBool("IsTakingDamage", _playerLife._isTakingDamage);

            //Bool pour la forme
            if(_formeManager._switchForm == Scr_FormeHandler.Forme.Heavy && !_isLourd)
            {
                _spritRend.color = Color.red;

                _animator.SetBool("IsLourd", true);
                _animator.SetBool("IsAgile", false);
                _animator.SetBool("IsHuman", false);

                _isHumain = false;
                _isAgile = false;
                _isLourd = true;

                _animator.SetTrigger("GoLourd");
            }
            else
            if (_formeManager._switchForm == Scr_FormeHandler.Forme.Agile && !_isAgile)
            {
                _spritRend.color = Color.white;

                _animator.SetBool("IsLourd", false);
                _animator.SetBool("IsAgile", true);
                _animator.SetBool("IsHuman", false);

                _isAgile = true;
                _isHumain = false;
                _isLourd = false;

                _animator.SetTrigger("GoAgile");

            }
            else
            if(_formeManager._switchForm == Scr_FormeHandler.Forme.Humain && !_isHumain)
            {
                _spritRend.color = Color.white;

                _animator.SetBool("IsLourd", false);
                _animator.SetBool("IsAgile", false);
                _animator.SetBool("IsHuman", true);

                _isHumain = true;
                _isAgile = false;
                _isLourd = false;

                _animator.SetTrigger("GoHumain");

            }

        }

        //AttackTrigger
        public void TriggerAttack()
        {
            _animator.SetTrigger("Attack");
        }

        public void TriggerDeath()
        {
            _animator.SetTrigger("Die");
        }

    }
}