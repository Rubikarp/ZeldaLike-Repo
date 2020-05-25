using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemis
{
    public class Scr_Harceleur_AttackRotPos : MonoBehaviour
    {
        private float _rotZ;
        private Vector2 _enemyDir;

        // Start is called before the first frame update
        void Start()
        {
            _enemyDir = GetComponentInParent<Scr_EnnemisBehaviour_Harceleur>()._targetDirection;

            _rotZ = Mathf.Atan2(_enemyDir.y, _enemyDir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, _rotZ - 90);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

