using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour {

    private NpcController myself;
    private Animator animator;

    // Start is called before the first frame update
    void Start(){

        myself = GetComponent<NpcController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){

        switch (myself.GetCharacterType()) {

            case CharacterType.melee:

                if (myself.GetIsInAttackZone()) {

                    myself.SetAnimation("isAttacking");
                }
                break;

            case CharacterType.ranger:

                break;
        }

        
    }
}
