using UnityEngine;

namespace Ennemis
{
    public class AnimatorHandler_Harceleur : MonoBehaviour
    {
        [SerializeField] private Scr_EnnemisBehaviour_Harceleur harceleur = null;
        [SerializeField] private Scr_EnnemisLifeSystem lifeSyst = null;
        [SerializeField] private SpriteRenderer _spritRend = null;
        [SerializeField] Rigidbody2D _rgd2D = null;
        public Animator animator = null;
        private bool _canFlip;

        private void Start()
        {
            _canFlip = true;
        }

        void Update()
        {
            //Flip Horizontal
            if (_canFlip == true)
            {
                if (harceleur._targetDirection.x > 0)
                {
                    _spritRend.flipX = true;
                }
                else if (harceleur._targetDirection.x < 0)
                {
                    _spritRend.flipX = false;
                }
            }

            animator.SetFloat("MouvementX", harceleur._targetDirection.x);
            animator.SetFloat("MouvementY", harceleur._targetDirection.y);

            animator.SetFloat("MouvementMagnitude", _rgd2D.velocity.magnitude);

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
        public void FlipLock(bool state)
        {
            _canFlip = state;
        }
    }
}