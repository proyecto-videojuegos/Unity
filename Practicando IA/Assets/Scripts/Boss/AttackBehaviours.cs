using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviours : StateMachineBehaviour{

    private BossController Boss;
    private Collider2D Claw1;
    private Collider2D Claw2;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){

        Boss = GameObject.Find("WereWolf").GetComponent<BossController>();
        Claw1 = GameObject.FindGameObjectWithTag("werewolfclaw1").GetComponent<Collider2D>();
        Claw2 = GameObject.FindGameObjectWithTag("werewolfclaw2").GetComponent<Collider2D>();

        //Activo el collider de las garras al entrar en la animacion
        Claw1.enabled = true;
        Claw2.enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){

        if(Boss.bossStage == 1) {

            if (Boss.GetIsInAttackZone() == false) {

                animator.SetTrigger("Follow");
            }
        } else {

            if (Boss.GetIsInAttackZone() == false) {

                animator.SetTrigger("Jump");
            }
        }

        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){

        //Desactivo el collider de las garras durante al acabar la animacion
        Claw1.enabled = false;
        Claw2.enabled = false;

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
