using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    private Animator m_Animator;

    float horizontalMove = 0f;

    private void Start() {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        horizontalMove = Input.GetAxisRaw("Horizontal") * PlayerController.sharedInstance.speed;

        m_Animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") && PlayerController.sharedInstance.isGrounded) {

            m_Animator.SetTrigger("Jump");
            PlayerController.sharedInstance.Jump();
        }
    }

    void FixedUpdate() {
        //Mueve al jugador
        PlayerController.sharedInstance.Move(horizontalMove * Time.fixedDeltaTime);
    }
}
