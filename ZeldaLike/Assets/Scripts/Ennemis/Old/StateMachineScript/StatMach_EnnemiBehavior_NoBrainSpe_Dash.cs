using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ennemis
{
    public class StatMach_EnnemiBehavior_NoBrainSpe_Dash : StateMachineBehaviour
    {
        public Ennemis_Fondation _my = null;
        public Ennemis_NoBrain _noBrain = null;
        public StatMach_EnnemiBehavior_MoveToward _moveToward;

        public bool _isDashing;
        public Vector2 _targetDirection = Vector2.zero;
        public float _targetDistance = 0;

        [SerializeField] private float _timer = 0;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _my = animator.gameObject.GetComponent<Ennemis_Fondation>();
            _moveToward = animator.GetBehaviour<StatMach_EnnemiBehavior_MoveToward>();
            _noBrain = animator.gameObject.GetComponent<Ennemis_NoBrain>();
            _timer = _noBrain._dashDelay;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _timer -= Time.deltaTime;

            if(_timer <= 0 && !_isDashing)
            {
                _isDashing = true;

                Dash(_noBrain._dashDuration, _targetDirection.normalized, _noBrain._dashSpeed);

                _isDashing = false;
                _noBrain.enabled.Equals(true);
                _timer = _noBrain._dashDelay;
            }

            //defini à chaque frame dans quel direction est le joueur
            _targetDirection = (_my.target.position - _my.transform.position);
            //calcul la distance entre le GameObject et le joueur
            _targetDistance = Vector2.Distance(_my.transform.position, _my.target.position);

            if (_targetDistance > _my._nearRange)
            {
                _my.body.velocity += _targetDirection.normalized * _my._movementSpeed * Time.deltaTime;
            }
        }
       
        void Dash(float duration, Vector2 Direction, float dashSpeed)
        {
            float timer = duration;
            _noBrain.enabled.Equals(false);

            while (timer <= 0)
            {
                _my.body.velocity = Direction * dashSpeed;

                timer -= Time.deltaTime;
            }
        }

    }
}