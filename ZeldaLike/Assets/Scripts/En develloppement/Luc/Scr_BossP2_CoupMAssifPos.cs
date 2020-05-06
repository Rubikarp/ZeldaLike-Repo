using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
    public class Scr_BossP2_CoupMAssifPos : MonoBehaviour
    {
        [HideInInspector] public float _rotZ;
        private Vector2 _bossDir;
        [HideInInspector] public bool _attackSet;
        public float _timerOrigin;
        private float _timer;

        // Start is called before the first frame update
        void Start()
        {
            _attackSet = false;
            _timer = _timerOrigin;
        }

        // Update is called once per frame
        void Update()
        {
            if (_attackSet == false)
            {
                _bossDir = GetComponentInParent<Scr_BossP2_Behavior>()._bossDirection;

                _rotZ = Mathf.Atan2(_bossDir.y, _bossDir.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0f, 0f, _rotZ - 90);
            }

            if (_attackSet == true && _timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else if (_attackSet == true && _timer <= 0)
            {
                _attackSet = false;
                _timer = _timerOrigin;
            }

        }
    }
}
