using UnityEngine;

namespace Ennemis
{
    public class StatMach_EnnemiBehavior_MoveToward : StateMachineBehaviour
    {
        public Ennemis_Fondation _my = null;
        public Vector2 _targetDirection = Vector2.zero;
        public float _targetDistance = 0;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _my = animator.gameObject.GetComponent<Ennemis_Fondation>();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //defini à chaque frame dans quel direction est le joueur
            _targetDirection = (_my.target.position - _my.transform.position);
            //calcul la distance entre le GameObject et le joueur
            _targetDistance = Vector2.Distance(_my.transform.position, _my.target.position);

            /*if (_targetDistance > _my._nearRange)
            {*/
                _my.body.velocity = _targetDirection.normalized * _my._movementSpeed;
            //}
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}