﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemis
{
    public class Scr_BossP3_AnimatorManager : MonoBehaviour
    {
        [SerializeField] private Scr_BossBehavior_Phase3 _bossP3;
        public SpriteRenderer _sprite;
        public Animator _animator = null;
        public Vector3 _bossDirection;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_bossDirection.x < 0)
            {
                _sprite.flipX = true;
            }
            else if (_bossDirection.x > 0)
            {
                _sprite.flipX = false;
            }

            _animator.SetFloat("Orientation X", _bossDirection.x);
            _animator.SetFloat("Orientation Y", _bossDirection.y);
        }
       
    }
}