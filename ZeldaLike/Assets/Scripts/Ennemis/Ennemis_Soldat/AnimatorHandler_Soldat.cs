using UnityEngine;

namespace Ennemis
{
    public class AnimatorHandler_Soldat : MonoBehaviour
    {
        [SerializeField] private Scr_EnnemisBehaviour_Soldat soldat = null;
        [SerializeField] private Scr_EnnemisLifeSystem lifeSyst = null;
        [SerializeField] private SpriteRenderer _spritRend = null;
        [SerializeField] Rigidbody2D _rgd2D = null;
        public Animator animator = null;

        void Update()
        {
            if (_rgd2D.velocity != Vector2.zero)
            {
                //Flip Horizontal
                if (soldat._targetDirection.x < 0)
                {
                    _spritRend.flipX = false;
                }
                else if (soldat._targetDirection.x > 0)
                {
                    _spritRend.flipX = true;
                }
            }
            else
            {
                //Flip Horizontal
                if (soldat._targetDirection.x < 0)
                {
                    _spritRend.flipX = true;
                }
                else if (soldat._targetDirection.x > 0)
                {
                    _spritRend.flipX = false;
                }
            }

            if (_rgd2D.velocity != Vector2.zero)
            {
                animator.SetFloat("OrientationX", _rgd2D.velocity.x);
                animator.SetFloat("OrientationY", _rgd2D.velocity.y);
            }
            else
            {
                animator.SetFloat("OrientationX", soldat._targetDirection.x);
                animator.SetFloat("OrientationY", soldat._targetDirection.y);
            }

            animator.SetBool("IsTakingDamage", lifeSyst._isTakingDamage);

            if (lifeSyst._isDead)
            {
                TriggerDeath();
            }
        }

        public void TriggerDeath()
        {
            animator.SetTrigger("Dying");
        }
    }
}