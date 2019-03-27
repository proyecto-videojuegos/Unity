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

    private int healthPoints, manaPoints;

    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 15, MAX_HEALTH = 150, MAX_MANA = 25;

    public const int MIN_HEALTH = 10, MIN_MANA = 0;

    public const float MIN_SPEED = 2.5f, HEALTH_TIME_DECREASE = 1.0f;

    public const int SUPER_JUMP_COST = 3;

    public const float SUPER_JUMP_FORCE = 1.5f;




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

        this.healthPoints = INITIAL_HEALTH;
        this.manaPoints = INITIAL_MANA;

        //Llamamos a la corrutina
        StartCoroutine("TirePlayer");

    }

    //Corrutina
    IEnumerator TirePlayer() {

        while(this.healthPoints > MIN_HEALTH) {

            this.healthPoints--;
            yield return new WaitForSeconds(HEALTH_TIME_DECREASE);
        }
        yield return null;
    }

    // Update is called once per frame
    void Update() {

        if (GameManager.sharedInstance.currentGameState == GameState.inGame) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                Jump(false);
 
            }

            if (Input.GetMouseButtonDown(1)) {
                Jump(true);

            }
            //Le decimos a la animacion si esta tocando el suelo o no con nuestro metodo IsTouchingTheGround()
            animator.SetBool("isGrounded", IsTouchingTheGround());
        }

    }

    void FixedUpdate() {

        if (GameManager.sharedInstance.currentGameState == GameState.inGame) {

            Move();
        }

            
    }

    void Jump(bool isSuperJump) {

        
            if (IsTouchingTheGround()) {
                //Usamos el mana para hacer super saltos
                if(isSuperJump && this.manaPoints >= SUPER_JUMP_COST) {

                    manaPoints -= SUPER_JUMP_COST;

                    //F = m * a =======> a = F/m
                    rigidbody.AddForce(Vector2.up * jumpForce * SUPER_JUMP_FORCE, ForceMode2D.Impulse);
                } else {

                    rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
                
            }

    }

    void Move() {

        if (Input.GetKey(KeyCode.D)) {

            float currentSpeed = (runningSpeed - MIN_SPEED) * this.healthPoints / 100f;

            if (rigidbody.velocity.x < currentSpeed) {

                rigidbody.velocity = new Vector2(runningSpeed,//Velocidad en el eje de las X
                                                 rigidbody.velocity.y//Velocidad en el eje de las y
                                                 );

            }
        }else if (Input.GetKey(KeyCode.A)) {

            float currentSpeed = (runningSpeed - MIN_SPEED) * this.healthPoints / 100f;

            if (rigidbody.velocity.x < currentSpeed) {

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

        float currentMaxScore = PlayerPrefs.GetFloat("maxscore", 0);

        if(currentMaxScore < this.GetDistance()) {

            PlayerPrefs.SetFloat("maxscore", this.GetDistance());
        }

        StopCoroutine("TirePlayer");
    }

    public float GetDistance() {

        float travelledDistance = Vector2.Distance(new Vector2(startPosition.x, 0),
                                                   new Vector2(this.transform.position.x, 0)
                                                   );

        return travelledDistance;//Posicion final - posicion inicial en el eje x
    }

    public void CollectHealth(int points) {

        this.healthPoints += points;
        //Para no superar la vida maxima
        if (this.healthPoints >= MAX_HEALTH) {

            this.healthPoints = MAX_HEALTH;
        }
        

    }

    public void CollectMana(int points) {

        this.manaPoints += points;
        //Para no superar el mana maximo
        if(this.manaPoints >= MAX_MANA) {

            this.manaPoints = MAX_MANA;
        }


    }

    public int GetHealth() {

        return this.healthPoints;
    }

    public int GetMana() {

        return this.manaPoints;
    }
}
