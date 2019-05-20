using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehaviours : StateMachineBehaviour{

    private Transform playerPosition;
    private BossController Boss;
    private float timer;
    public float minTime;
    public float maxTime;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        Boss = GameObject.Find("WereWolf").GetComponent <BossController> ();
        timer = Random.Range(minTime,maxTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){

        if (timer <= 0) {

            animator.SetTrigger("Jump");
        } else {

            timer -= Time.deltaTime;
        }

        animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPosition.position, Boss.speed * Time.deltaTime);

        if (Boss.GetIsInAttackZone()) {

            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){

    }



    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
