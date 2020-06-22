using UnityEngine;

namespace Ennemis
{
    public class AnimatorHandler_Chimiste : MonoBehaviour
    {
        [SerializeField] private Scr_EnnemisBehaviour_Chimiste chimiste = null;
        [SerializeField] private Scr_EnnemisLifeSystem lifeSyst = null;
        [SerializeField] private SpriteRenderer _spritRend = null;
        [SerializeField] Rigidbody2D _rgd2D = null;
        public Animator animator = null;

        void Update()
        {
            //Flip Horizontal
            if (_rgd2D.velocity.x < 0)
            {
                _spritRend.flipX = true;
            }
            else if (_rgd2D.velocity.x > 0)
            {
                _spritRend.flipX = false;
            }

            animator.SetFloat("OrientationX", _rgd2D.velocity.x);
            animator.SetFloat("OrientationY", _rgd2D.velocity.y);

            animator.SetFloat("Speed", _rgd2D.velocity.magnitude) ;

            animator.SetBool("isTakingDamage", lifeSyst._isTakingDamage);
        }

        public void TriggerDeath()
        {
            animator.SetTrigger("Dying");
        }
    }
}