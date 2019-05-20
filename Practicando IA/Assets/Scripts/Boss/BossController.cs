using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class BossController : MonoBehaviour {
    Animator b_Animator;
    Rigidbody2D b_Rigidbody;

    public GameObject startPoint;
    public float visionRadius;
    public float attackRadius;
    public float speed;
    public float powerJump;
    public float bossDeathTime;
    public int bossStage;

    public Image healthBar;

    private float MAXBOSSHEALTH = 100f;
    public float bossHealth;
    private Vector3 target;
    private Vector3 oneFrameAgo;

    private bool isFacingRight;
    private bool isTargetting;
    private bool isInAttackZone;

    //Para que solo cuente la muerte en el momento que se produce
    bool oneTime = true;

    // Start is called before the first frame update
    void Start() {

        b_Animator = GetComponent<Animator>();
        b_Rigidbody = GetComponent<Rigidbody2D>();

        this.healthBar.fillAmount = MAXBOSSHEALTH / 100f;
        this.bossHealth = MAXBOSSHEALTH;
        //Le indicamos la posicion inicial
        this.transform.position = startPoint.transform.position;

        isFacingRight = false;
        isTargetting = false;
        isInAttackZone = false;

        bossStage = 1;
    }

    void Update() {

        if (GameManager.sharedInstance.currentGameState == GameState.inGame) {

            if (IsAlive()) {

                HealthBarChanger();
                Targeted();
            }

            if (bossHealth < 35f) {

                b_Animator.SetTrigger("SecondStage");
                bossStage = 2;

            }

            if (!IsAlive()) {

                b_Animator.SetTrigger("Die");
                this.GetComponent<Rigidbody2D>().Sleep();
                this.GetComponent<CapsuleCollider2D>().enabled = false;
                Destroy(gameObject, bossDeathTime);
                if (oneTime) {

                    GameManager.sharedInstance.EnemyDie();
                    oneTime = false;
                }

            }
        }
    }

    void Targeted() {

        target = PlayerController.sharedInstance.transform.position;
        //Comprobamos un Raycast del enemigo hasta el jugador
        RaycastHit2D hit = Physics2D.Raycast(
            new Vector3(this.transform.position.x, this.transform.position.y + 5),
            target - this.transform.position,
            visionRadius,
            //Poner el Boss en una layer distinta a Default para evitar el raycast
            //Tambien poner los objetos de ataque en una capa distinta para que no los detecte
            1 << LayerMask.NameToLayer("Default")
         );

        //Para debuguear el Raycast
        Vector3 forward = transform.TransformDirection(target - this.transform.position);
        Debug.DrawRay(new Vector3(this.transform.position.x, this.transform.position.y + 5), forward, Color.red);

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

    bool IsAlive() {

        if (bossHealth > 0) {

            return true;
        } else {

            return false;
        }
    }

    void HealthBarChanger() {

        float healthAmount = (bossHealth / 100f);
        healthBar.fillAmount = healthAmount;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider) {

        if (otherCollider.CompareTag("Sword")) {

            bossHealth -= PlayerController.sharedInstance.GetMelePlayerDamage();
        } else if (otherCollider.CompareTag("BallFire")) {

            bossHealth -= PlayerController.sharedInstance.GetRangePlayerDamage();         
        }
    }

    private void OnDrawGizmosSelected() {

        //Esferas con los rangos de vision y ataque
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(this.transform.position.x, this.transform.position.y + 5), visionRadius);
        Gizmos.DrawWireSphere(new Vector3(this.transform.position.x, this.transform.position.y + 5), attackRadius);
    }

    public bool GetTargeting() {

        return isTargetting;
    }

    public bool GetIsInAttackZone() {

        return isInAttackZone;
    }

}
