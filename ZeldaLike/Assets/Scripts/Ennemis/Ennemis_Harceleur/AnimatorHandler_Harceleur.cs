using UnityEngine;

namespace Ennemis
{
    public class AnimatorHandler_Harceleur : MonoBehaviour
    {
        [SerializeField] private Scr_EnnemisBehaviour_Soldat soldat = null;
        [SerializeField] private Scr_EnnemisLifeSystem lifeSyst = null;
        public Animator animator = null;
        
        void Update()
        {
            animator.SetFloat("OrientationX", soldat._targetDirection.x);
            animator.SetFloat("OrientationY", soldat._targetDirection.y);

            animator.SetBool("isShooting", soldat._isShooting);
            animator.SetBool("isAttacking", soldat._isCharging);

            animator.SetBool("isTakingDamage", lifeSyst._isTakingDamage);
        }

        public void TriggerDeath()
        {
            animator.SetTrigger("Dying");
        }
        public void TriggerTP()
        {
            animator.SetTrigger("Teleporting");
        }
    }
}