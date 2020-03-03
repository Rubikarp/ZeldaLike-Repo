using UnityEngine;
using System.Collections;

namespace Ennemis
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ennemis_Fondation : MonoBehaviour
    {
        [HideInInspector]
        public Rigidbody2D body;

        [Header("Stat")]
        public int life = 3;
        public float _movementSpeed = 5f;

        [Space(10)]

        [Header("Target")]
        public Transform target = null;
        public float _detectionRange = 20f;
        public float _farRange = 10f;
        public float _attackRange = 8f;
        public float _nearRange = 5f;

        private void Start()
        {
            body = this.gameObject.GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            Living(life);
        }

        void Living(int life)
        {
            if(life <= 0)
            {
                StartCoroutine(Dying());
            }
        }


        IEnumerator Dying()
        {
            //Death anim

            yield return new WaitForSeconds(1.5f);

            Destroy(this.gameObject);

            yield return 0;
        }
    }

}
