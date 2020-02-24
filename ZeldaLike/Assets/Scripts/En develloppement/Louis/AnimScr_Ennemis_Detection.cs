using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemis;

public class AnimScr_Ennemis_Detection : StateMachineBehaviour
{
    public Ennemis_Fondation mySelf = null;
    public Transform player;
    private Animator Animator;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mySelf = GameObject.Find("Bot").GetComponent<Ennemis_Fondation>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((player.transform.position - mySelf.transform.position).magnitude < mySelf._detectionRange)
        {
            mySelf.target = player;
            animator.SetBool("isFollowing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        // Implement code that processes and affects root motion
    //}

}
