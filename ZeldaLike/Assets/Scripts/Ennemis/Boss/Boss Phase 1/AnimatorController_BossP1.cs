using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemis
{
    public class AnimatorController_BossP1 : MonoBehaviour
    {
        [SerializeField] private Scr_BossPhase1 BossP1 = null;
        [SerializeField] private Scr_BossLifeSystem lifeSyst = null;
        [SerializeField] private SpriteRenderer _spritRend = null;
        [SerializeField] private Rigidbody2D _rgd2D = null;
        public Animator animator = null;
        
        void Update()
        {
            if (_rgd2D.velocity.x < 0)
            {
                _spritRend.flipX = true;
            }
            else if (_rgd2D.velocity.x > 0)
            {
                _spritRend.flipX = false;
            }

            animator.SetFloat("Orientation X", _rgd2D.velocity.x);
            animator.SetFloat("Orientation Y", _rgd2D.velocity.y);
            animator.SetFloat("Speed", _rgd2D.velocity.magnitude);

            animator.SetBool("isTakingDamage", lifeSyst._isTakingDamage);

        }

        public void TriggerDeath()
        {
            animator.SetTrigger("Dying");
        }

    }
}