using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour{

    private Vector3 target;
    private NpcController myself;
    private Animator animator;

    // Start is called before the first frame update
    void Start(){

        myself = GetComponent<NpcController>();
        animator = GetComponent<Animator>();
        target = myself.GetTarget();
    }

    // Update is called once per frame
    void Update(){

        target = myself.GetTarget();

        if (myself.GetIsTargeting() && !myself.GetIsInAttackZone()) {

            transform.position = Vector2.MoveTowards(transform.position, target, myself.speed * Time.deltaTime);
            myself.SetAnimation("isWalking");
        }
    }
}
