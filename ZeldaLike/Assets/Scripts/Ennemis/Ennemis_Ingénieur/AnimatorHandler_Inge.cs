using UnityEngine;

namespace Ennemis
{
    public class AnimatorHandler_Inge : MonoBehaviour
    {
        [SerializeField] private Scr_EnnemisBehaviour_Ingénieur ingenieur = null;
        [SerializeField] private Scr_EnnemisLifeSystem lifeSyst = null;
        public Animator animator = null;

        void Update()
        {
            animator.SetFloat("Speed", ingenieur._myBody.velocity.magnitude);

            animator.SetBool("IsAttacking", ingenieur._isShooting);
            animator.SetBool("IsFleeing", ingenieur._isRunning);

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