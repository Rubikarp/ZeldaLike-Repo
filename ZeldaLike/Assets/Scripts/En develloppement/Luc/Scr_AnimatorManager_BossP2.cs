using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management
{
    public class Scr_AnimatorManager_BossP2 : MonoBehaviour
    {
        public Animator _animator;

        public void LaserTrigger(bool state)
        {
            _animator.SetBool("IsFiringLaser", state);
        }

        public void CoupMassifTrigger(bool state)
        {
            _animator.SetBool("IsMassiveThumping", state);
        }

        public void RenfortsTrigger()
        {
            _animator.SetTrigger("isCallingHelp");
        }

        public void MonArmeeTrigger(bool state)
        {
            _animator.SetBool("isCallingArmy", state);
        }

        public void JetDeDebrisTrigger()
        {
            _animator.SetTrigger("IsThrowing");
        }

        public void AspirationTrigger(bool state)
        {
            _animator.SetBool("IsSuckingUp", state);
        }

        public void Death()
        {
            _animator.SetTrigger("Dying");
        }

    }
}