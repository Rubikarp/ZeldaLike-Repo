using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class StunBehaviour : MonoBehaviour
    {
        public float _Damage = 0f;
        public float _LifeTime = 0.3f;

        public float _StunningTime = 0.5f;
        
        void Start()
        {
            StartCoroutine(DestroyGameObjectIn(this.gameObject, _LifeTime));
        }

        IEnumerator DestroyGameObjectIn(GameObject scriptGameobject,float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);

            Destroy(scriptGameobject);
        }

    }
}