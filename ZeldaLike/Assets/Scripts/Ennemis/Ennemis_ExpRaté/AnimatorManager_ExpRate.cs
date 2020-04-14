using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemis;

namespace Game
{
    public class AnimatorManager_ExpRate : MonoBehaviour
    {
        [Header("Auto Components")]
        [SerializeField] SpriteRenderer _spritRend = null;
        [SerializeField] Animator _animator = null;
        [SerializeField] Rigidbody2D _rgd2D = null;

        [Header("Components")]
        [SerializeField] Scr_EnnemisLifeSystem _lifeSyst = null;
        [SerializeField] EnnemisBehaviour_ExpRate _expRateBehavior = null;

        private bool _isDead = false;
        
        void Start()
        {
            _rgd2D = this.gameObject.GetComponent<Rigidbody2D>();
            _animator = this.gameObject.GetComponent<Animator>();
            _spritRend = this.gameObject.GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            //Flip Horizontal
            if (_rgd2D.velocity.x < 0)
            {
                _spritRend.flipX = false;
            }
            else if (_rgd2D.velocity.x > 0)
            {
                _spritRend.flipX = true;
            }

            //Valeur des actions
            _animator.SetFloat("MouvementX", _rgd2D.velocity.x);
            _animator.SetFloat("MouvementY", _rgd2D.velocity.y);
            _animator.SetFloat("MouvementMagnitude", _rgd2D.velocity.magnitude);

            _animator.SetBool("isDashing", _expRateBehavior._isDashing);
            _animator.SetBool("IsTakingDamage", _lifeSyst._isTakingDamage);

            if (_lifeSyst._life <= 0 && !_isDead)
            {
                _animator.SetTrigger("Dying");
                _isDead = true;
            }

        }
    }
}