using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemis
{
    public class AnimatorController_BossP1 : MonoBehaviour
    {
        [SerializeField] private Scr_BossPhase1 BossP1 = null;
        [SerializeField] private Scr_BossLifeSystem lifeSyst = null;
        [SerializeField] public SpriteRenderer _spritRend = null;
        [SerializeField] private Rigidbody2D _rgd2D = null;
        public Animator animator = null;
        public Vector3 _bossDirection;
        public bool _canFlip = true;
        
        void Update()
        {
            _bossDirection = BossP1._bossDirection;

            if (_canFlip == true)
            {
                if (_bossDirection.x < 0)
                {
                    _spritRend.flipX = true;
                }
                else if (_bossDirection.x > 0)
                {
                    _spritRend.flipX = false;
                }
            }

            animator.SetFloat("Orientation X", _bossDirection.x);
            animator.SetFloat("Orientation Y", _bossDirection.y);
            animator.SetFloat("Speed", _rgd2D.velocity.magnitude);

            animator.SetBool("isTakingDamage", lifeSyst._isTakingDamage);

        }

        public void TriggerDeath()
        {
            animator.SetTrigger("Dying");
        }

        public void SpriteFlip(bool state)
        {
           if (state == true)
           {
                if (_spritRend.flipX == true)
                {
                    _spritRend.flipX = false;
                }
                else if (_spritRend.flipX == false)
                {
                    _spritRend.flipX = true;
                }
           }
        }

    }
}