using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemies
{
    public class Scr_BossGrenadeBehavior : MonoBehaviour
    {
        private Vector3 _grenadeTarget;
        public float _grenadeSpeed;
        public Transform _mySelf;
        private bool _targetAttained;
        public float _explosionDelay;
        public float _explosionLife;
        public GameObject _Explosion;
        private SoundManager sound; //Le son

        // Start is called before the first frame update
        void Start()
        {
            _grenadeTarget = GameObject.FindGameObjectWithTag("Player").transform.position;
            _targetAttained = false;
        }
        void Awake()
        {
            sound = SoundManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            if (_targetAttained == false)
            {
                _mySelf.position = Vector2.MoveTowards(_mySelf.position, _grenadeTarget, _grenadeSpeed * Time.deltaTime);

                if (_mySelf.position == _grenadeTarget)
                {
                    _targetAttained = true;
                }
            }
            else if (_targetAttained == true)
            {
                if (_explosionDelay > 0)
                {
                    _explosionDelay -= Time.deltaTime;
                }
                else if (_explosionDelay <= 0)
                {
                    _Explosion.SetActive(true);
                    sound.PlaySound("Explosion Bombe");
                }

                if (_explosionLife > 0 && _explosionDelay <= 0)
                {
                    _explosionLife -= Time.deltaTime;
                }
                else if (_explosionLife <= 0 && _explosionDelay <= 0)
                {
                    Destroy(gameObject);
                }
            }


        }

    }

}
