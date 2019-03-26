using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController sharedInstance;

    public float jumpForce = 30f;

    public float runningSpeed = 1.5f;

    public Animator animator;

    private Rigidbody2D rigidbody;

    private Vector3 startPosition;




    private void Awake() {

        sharedInstance = this;
        rigidbody = GetComponent<Rigidbody2D>();
        //Le damos al objeto startPosition la posicion del personaje. Al hacer esto en el awake nos devulve la posicion inicial del Player
        startPosition = this.transform.position;

    }


   
    public void StartGame() {

        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        //Cada vez que reiniciamos ponemos al personaje en la posicion inicial
        this.transform.position = startPosition;

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


    bool IsTouchingTheGround() {

        if(Physics2D.Raycast(this.transform.position,//trazamos el rayo desde la posicion del jugador 
                             Vector2.down,//direccion hacia el suelo
                             0.2f//hasta un maximo de 20 cm
                             )) {
            return true;

        } else {

            return false;
        }
    }

    public void Kill() {

        GameManager.sharedInstance.GameOver();
        this.animator.SetBool("isAlive", false);
    }
}
