using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface Int_LifeSystem
    {
        void TakingDamage(int damage);

        IEnumerator GetKnockBack(float knockbackForce, Vector2 AttackPos, float StunDuration);

    }
}