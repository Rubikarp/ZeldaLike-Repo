using UnityEngine;

namespace Ennemis
{
    public class AnimatorHandler_Chimiste : MonoBehaviour
    {
        [SerializeField] private Scr_EnnemisBehaviour_Chimiste chimiste = null;
        [SerializeField] private Scr_EnnemisLifeSystem lifeSyst = null;
        public Animator animator = null;

        void Update()
        {
            animator.SetFloat("Speed", chimiste._movementSpeed);

            animator.SetBool("isTakingDamage", lifeSyst._isTakingDamage);
        }

        public void TriggerDeath()
        {
            animator.SetTrigger("Dying");
        }
    }
}