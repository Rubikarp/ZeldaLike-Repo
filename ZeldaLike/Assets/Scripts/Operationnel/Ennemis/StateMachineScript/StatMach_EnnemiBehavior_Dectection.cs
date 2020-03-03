using UnityEngine;

namespace Ennemis
{
    public class StatMach_EnnemiBehavior_Dectection : StateMachineBehaviour
    {
        public Ennemis_Fondation _my = null;
        public Transform _player;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _my = animator.gameObject.GetComponent<Ennemis_Fondation>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if ((_player.transform.position - _my.transform.position).magnitude < _my._detectionRange)
            {
                _my.target = _player;
                animator.SetBool("PlayerFound", true);
            }
        }

    }
}
