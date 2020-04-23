using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
    public class Scr_BossP2_CoupMassifTimer : MonoBehaviour
    {
        public float _hitDelay;
        public float _dmgLifeTime;
        public GameObject _hitbox;
        private bool _isActive;

        // Start is called before the first frame update
        void Start()
        {
            _isActive = false;
            GetComponentInParent<Scr_BossP2_CoupMAssifPos>()._attackSet = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (_isActive == false)
            {
                if (_hitDelay > 0)
                {
                    _hitDelay -= Time.deltaTime;
                }
                else if (_hitDelay <= 0)
                {
                    _hitbox.SetActive(true);
                    _isActive = true;
                }
            }
            else if (_isActive == true)
            {
                if (_dmgLifeTime > 0)
                {
                    _dmgLifeTime -= Time.deltaTime;
                }
                else if (_dmgLifeTime <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
