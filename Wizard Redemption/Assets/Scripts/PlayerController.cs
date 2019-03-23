using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float jumpForce = 30f;

    public float runningSpeed = 1.5f;

    public Animator animator;

    private Rigidbody2D rigidbody;

    public LayerMask groundLayer;


    private void Awake() {

        rigidbody = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start() {
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);

    }

    // Update is called once per frame
    void Update() {

        if(GameManager.sharedInstance.currentGameState == GameState.inGame) {

            Jump();

            //Le decimos a la animacion si esta tocando el suelo o no con nuestro metodo IsTouchingTheGround()
            animator.SetBool("isGrounded", IsTouchingTheGround());
        }

        
    }

    void FixedUpdate() {

        if (GameManager.sharedInstance.currentGameState == GameState.inGame) {

            Move();
        }

            
    }

    void Jump() {

        if (Input.GetKeyDown(KeyCode.Space)) {

            if (IsTouchingTheGround()) {
                //F = m * a =======> a = F/m
                rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

    }

    void Move() {

        if (Input.GetKey(KeyCode.D)) {

            if (rigidbody.velocity.x < runningSpeed) {

                rigidbody.velocity = new Vector2(runningSpeed,//Velocidad en el eje de las X
                                                 rigidbody.velocity.y//Velocidad en el eje de las y
                                                 );

            }
        }else if (Input.GetKey(KeyCode.A)) {

            if (rigidbody.velocity.x < runningSpeed) {

                rigidbody.velocity = new Vector2(-runningSpeed,//Velocidad en el eje de las X
                                                 rigidbody.velocity.y//Velocidad en el eje de las y
                                                 );

            }
        }

            
    }

    void Walk() {


    }


    bool IsTouchingTheGround() {

        if(Physics2D.Raycast(this.transform.position,//trazamos el rayo desde la posici'on del jugador 
                             Vector2.down,//direccion hacia el suelo
                             0.2f,//hasta un maximo de 20 cm
                             groundLayer//y nos encontramos con la capa del suelo
                             )) {
            return true;

        } else {

            return false;
        }
    }
}
