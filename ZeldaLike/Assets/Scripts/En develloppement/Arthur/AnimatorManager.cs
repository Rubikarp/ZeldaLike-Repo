using UnityEngine;
using Game;

namespace Management
{
    public class AnimatorManager : MonoBehaviour
    {
        [Header("OUTPUT")]

        public Animator _humanAnimator = null;
        public SpriteRenderer _humanSprRender = null;
        [Space(5)]
        public Animator _agileAnimator = null;
        public SpriteRenderer _agileSprRender = null;
        [Space(5)]
        public Animator _heavyAnimator = null;
        public SpriteRenderer _heavySprRender = null;

        [Space(20)]

        [Header("INPUT")]

        [SerializeField] InputManager input = null;


        void Start()
        {
            
        }
        
        void Update()
        {

            if(input._CharacterDirection.x < 0)
            {
                _humanSprRender.flipX = true;
            }
            else if (input._CharacterDirection.x > 0)
            {
                _humanSprRender.flipX = false;
            }

            _humanAnimator.SetFloat("StickY", input._CharacterDirection.y);
        }
        
        
        
    }
}