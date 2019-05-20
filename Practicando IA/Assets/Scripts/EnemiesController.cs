using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour{

    public GameObject startPoint;
    public GameObject endPoint;

    private Animator e_Animator;
    
    public float enemySpeed;
    public float enemyDeathTime;
    public float visionRadius;
    public float attackRadius;
    private float enemyHealth = 100f;

    private Vector3 target;
    private bool isGoingRight;
    private bool isFacingRight;
    private bool isTargetting;
    private bool isInAttackZone;

    //Para que solo cuente la muerte en el momento que se produce
    bool oneTime = true;

    private void Awake() {

        isFacingRight = true;

        this.transform.position = startPoint.transform.position;

    }
    // Start is called before the first frame update
    void Start() { 

        e_Animator = GetComponent<Animator>();
        isFacingRight = true;

        this.transform.position = startPoint.transform.position;
    }

    private void Update() {

        if (GameManager.sharedInstance.currentGameState == GameState.gameOver) {

            if (isAlive()) {

                Targeted();
            }
        }

        if (GameManager.sharedInstance.currentGameState == GameState.gameOver) {

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate(){

        if(GameManager.sharedInstance.currentGameState == GameState.inGame) {

            if (isAlive()) {

                Patrol();
                GetAnimations("isWalking");

                if (isTargetting) {

                    Persecute();
                    GetAnimations("isWalking");
                }

                if (isInAttackZone) {

                    GetAnimations("isAttacking");
                }
            }

            if (!isAlive()) {

                GetAnimations("Diying");
                this.GetComponent<Rigidbody2D>().Sleep();
                this.GetComponent<CapsuleCollider2D>().enabled = false;
                Destroy(gameObject, enemyDeathTime);
                if (oneTime) {

                    GameManager.sharedInstance.EnemyDie();
                    oneTime = false;
                }
                
            }
        }
    }
    
    void Patrol() {

        if (!isGoingRight) {

            this.transform.position = Vector3.MoveTowards(this.transform.position, endPoint.transform.position, enemySpeed * Time.deltaTime);
        }

        if (isGoingRight) {

            this.transform.position = Vector3.MoveTowards(this.transform.position, startPoint.transform.position, enemySpeed * Time.deltaTime);
        }

        if (this.transform.position == endPoint.transform.position || this.transform.position == startPoint.transform.position) {

            isGoingRight = !isGoingRight;
            Flip();
        }
    }

    void Persecute() {

        this.transform.position = Vector3.MoveTowards(this.transform.position, PlayerController.sharedInstance.transform.position, enemySpeed * Time.deltaTime);

        if (PlayerController.sharedInstance.transform.position.x < this.transform.position.x && isFacingRight == true) {

            Flip();
            isFacingRight = false;
        }
        if (PlayerController.sharedInstance.transform.position.x > this.transform.position.x && isFacingRight == false) {

            Flip();
            isFacingRight = true;

        }
        
    }

    void Targeted() {

        target = PlayerController.sharedInstance.transform.position;
        //Comprobamos un Raycast del enemigo hasta el jugador
        RaycastHit2D hit = Physics2D.Raycast(
            new Vector3(this.transform.position.x, this.transform.position.y),
            target - this.transform.position,
            visionRadius,
            //Poner el Boss en una layer distinta a Default para evitar el raycast
            //Tambien poner los objetos de ataque en una capa distinta para que no los detecte
            1 << LayerMask.NameToLayer("Default")
         );

        //Para debuguear el Raycast
        Vector3 forward = transform.TransformDirection(target - this.transform.position);
        Debug.DrawRay(new Vector3(this.transform.position.x, this.transform.position.y), forward, Color.red);

        //Si el Raycast encuentra al jugador lo ponemos de target
        if (hit.collider != null) {

            if (hit.collider.tag == "Player") {
                isTargetting = true;
            }
        }

        //Calculamos la distancia hasta el target
        float distance = Vector3.Distance(target, this.transform.position);

        //Calculamos la direccion
        Vector3 direction = (target - this.transform.position).normalized;

        //Definimos si esta en la zona de ataque
        if (distance < attackRadius) {

            isInAttackZone = true;
        } else {

            isInAttackZone = false;
        }
        //Para saber hacia donde esta mirando
        if (direction.x > 0 && !isFacingRight) {

            Flip();
            isFacingRight = true;
        } else if (direction.x < 0 && isFacingRight) {

            Flip();
            isFacingRight = false;
        }
    }

    void Flip() {

        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;

        this.transform.localScale = theScale;
    }


    bool isAlive() {

        if(enemyHealth > 0) {

            return true;
        } else {
            
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider) {

        if (otherCollider.CompareTag("BallFire")) {

            enemyHealth -= PlayerController.sharedInstance.GetRangePlayerDamage();
        }
        
    }

    private void GetAnimations(string state) {

        e_Animator.SetBool("isIdle", false);
        e_Animator.SetBool("isWalking", false);
        e_Animator.SetBool("isAttacking", false);

        e_Animator.SetBool(state, true);

    }

    public void SetEnemyHealth(float health) {

        this.enemyHealth = health;
    }
    
}
