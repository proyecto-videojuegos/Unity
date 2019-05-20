using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviours : StateMachineBehaviour
{
    //public GameObject effect;
    private BossController Boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        Boss = GameObject.Find("WereWolf").GetComponent<BossController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (Boss.GetTargeting()) {

            animator.SetTrigger("Follow");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //ejemplo de crear efectos al acabar la animacion
        //Instantiate(effect, animator.transform.position, Quaternion.identity);
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
