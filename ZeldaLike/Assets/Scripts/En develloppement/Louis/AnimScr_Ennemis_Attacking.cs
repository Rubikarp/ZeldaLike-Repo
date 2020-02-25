using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ennemis;

public class AnimScr_Ennemis_Attacking : StateMachineBehaviour
{
    public Ennemis_Fondation mySelf = null;
    public Transform player;
    public float Timer = 6f;
    private Animator animator;
    private float attackSpeed;
    private bool isDashing;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mySelf = GameObject.Find("Bot").GetComponent<Ennemis_Fondation>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackSpeed = mySelf._dashSpeed;
        Timer = 6f;
        isDashing = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Timer -= Time.deltaTime;

        if (Timer <= 0)
        {
            isDashing = false;
            Timer = 6f;
            animator.SetBool("isAttacking", false);
        }

        if (Timer <= 1 && Timer > 0)
        {
            mySelf.transform.position = Vector2.MoveTowards(mySelf.transform.position, player.position, attackSpeed * Time.deltaTime);
            isDashing = true;
            animator.SetBool("isAttacking", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    /*IEnumerator Dash()
    {
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(1f);
        mySelf.transform.position = Vector2.MoveTowards(mySelf.transform.position, player.position, attackSpeed * Time.deltaTime);
    }

    
            /*mySelf.transform.position = Vector2.MoveTowards(mySelf.transform.position, player.position, attackSpeed * Time.deltaTime);
            Timer = 6f;
            animator.SetBool("isAttacking", true);*/
}
