using UnityEngine;

namespace Ennemis
{
    public class AnimatorHandler_Soldat : MonoBehaviour
    {
        [SerializeField] private Scr_EnnemisBehaviour_Soldat soldat = null;
        [SerializeField] private Scr_EnnemisLifeSystem lifeSyst = null;
        public Animator animator = null;

        void Update()
        {

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