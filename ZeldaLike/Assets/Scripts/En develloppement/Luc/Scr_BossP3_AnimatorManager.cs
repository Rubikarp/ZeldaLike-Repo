using UnityEngine;

namespace Ennemis
{
    public class Scr_BossP3_AnimatorManager : MonoBehaviour
    {
        [SerializeField] private Scr_BossBehavior_Phase3 _bossP3;
        public SpriteRenderer _sprite;
        public Animator _animator = null;
        public Vector2 _bossDirection;
        public bool _canDir;

        // Start is called before the first frame update
        void Start()
        {
            _canDir = true;
        }

        // Update is called once per frame
        void Update()
        {
            _bossDirection = _bossP3._playerDirection;

            /*if (_bossDirection.x < 0)
            {
                _sprite.flipX = true;
            }
            else if (_bossDirection.x > 0)
            {
                _sprite.flipX = false;
            }*/

            if (_canDir == true)
            {
                _animator.SetFloat("Orientation X", _bossDirection.x);
                _animator.SetFloat("Orientation Y", _bossDirection.y);
            }
        }
       
    }
}
