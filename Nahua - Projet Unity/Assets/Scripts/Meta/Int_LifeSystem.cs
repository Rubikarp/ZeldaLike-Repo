using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface Int_LifeSystem
    {
        IEnumerator TakingDamage(int damage, Rigidbody2D body, Vector2 knockBackDirection, float knockbackSpeed, float stunDuration);
    }
}