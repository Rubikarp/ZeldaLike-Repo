using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AnimatorManager_PNJ_01 : MonoBehaviour
    {
        [Header("Auto Components")]
        [SerializeField] SpriteRenderer _spritRend = null;
        [SerializeField] Animator _animator = null;

        [Header("Variable")]
        public float MouvX = 0;
        public float MouvY = 0;


        void Start()
        {
            _spritRend = this.gameObject.GetComponent<SpriteRenderer>();
            _animator = this.gameObject.GetComponent<Animator>();
        }

        void Update()
        {
            //Flip Horizontal
            if (MouvX < 0)
            {
                _spritRend.flipX = true;
            }
            else if (MouvX > 0)
            {
                _spritRend.flipX = false;
            }

            //Valeur des actions
            _animator.SetFloat("MouvX", MouvX);
            _animator.SetFloat("MouvY", MouvY);

        }


    }
}