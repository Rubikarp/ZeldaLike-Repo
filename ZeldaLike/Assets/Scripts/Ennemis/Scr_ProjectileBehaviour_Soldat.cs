using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemis
{
    public class Scr_ProjectileBehaviour_Soldat : MonoBehaviour
    {
        [Header("Data")]
        public Transform _projectile = null;
        public Scr_EnnemisBehaviour_Soldat _behavSoldat = null;
        public Rigidbody2D _rb2d = null;

        [Header("Statistiques")]
        public float _mySpeed = 10f;
        public float _timer = 10f;

        private void Start()
        {
            _projectile = this.transform;
            _rb2d = this.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            /*if (_behavSoldat._isShooting)
            {
                transform.position = 
            }*/
        }
    }
}
