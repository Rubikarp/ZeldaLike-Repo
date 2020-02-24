using UnityEngine;
using System.Collections;

namespace Ennemis
{
    public class Ennemis_Fondation : MonoBehaviour
    {
        private Animator animator;
        [Header("HP")]
        public int life = 3;
        [Space(10)]
        [Header("Target")]
        public Transform target = null;
        public float _detectionRange = 10f;
        public float _attackRange = 3f;
        public float _movementSpeed = 5f;
        public float _dashSpeed = 10f;

        private void Start()
        {
            animator = this.gameObject.GetComponent<Animator>();
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
