using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType {

    melee,
    ranger
}

public class NpcController : MonoBehaviour{

    private Animator npcAnimator;
    public CharacterType characterType;

    public float health;
    public float speed;
    public float visionRange;
    public float attackRange;
    public float enemyDeathTime;

    private bool isTargetting;
    private bool isInAttackZone;
    private bool isFacingRight;

    public GameObject gameObjectTarget;
    private Vector3 target;

    //Para que solo cuente la muerte en el momento que se produce
    private bool oneTime = true;

    // Start is called before the first frame update
    private void Start(){

        npcAnimator = GetComponent<Animator>();
        isFacingRight = false;
        isTargetting = false;
        isInAttackZone = false;
        health = 100;

    }

    // Update is called once per frame
    private void Update(){

        Locating();

        if (!IsAlive()) {

            //GetAnimations("Diying");
            this.GetComponent<Rigidbody2D>().Sleep();
            this.GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(gameObject, enemyDeathTime);
            if (oneTime) {

                GameManager.sharedInstance.EnemyDie();
                oneTime = false;
            }

        }
    }

    private void Locating() {

        target = gameObjectTarget.transform.position;

        //Comprobamos un Raycast del enemigo hasta el jugador
        RaycastHit2D hit = Physics2D.Raycast(
            new Vector3(this.transform.position.x, this.transform.position.y), //Posicion origen
            target - this.transform.position, //Direccion
            visionRange, //Distancia
            //Poner el NPS en una layer distinta a Default para evitar el raycast
            //Tambien poner los objetos de ataque en una capa distinta para que no los detecte
            1 << LayerMask.NameToLayer("Default")
         );

        //Para debuguear el Raycast
        Debug.DrawRay(this.transform.position, target - this.transform.position, Color.red);

        //Si el Raycast encuentra al jugador lo ponemos de target
        if (hit.collider != null) {

            if (hit.collider.tag == "Player") {
                isTargetting = true;
                //Para debuguear el targeting
                Debug.DrawRay(this.transform.position, target - this.transform.position, Color.green);
            }
        } else {

            isTargetting = false;
        }

        //Calculamos la distancia hasta el target
        float distance = Mathf.Abs(target.x - this.transform.position.x);

        //Calculamos la direccion
        Vector3 direction = (target - this.transform.position).normalized;

        //Definimos si esta en la zona de ataque
        if (distance < attackRange) {

            isInAttackZone = true;
        } else {

            isInAttackZone = false;
        }

        //Para saber hacia donde esta mirando
        if (direction.x > 0 && !isFacingRight) {

            Flip();
        } else if (direction.x < 0 && isFacingRight) {

            Flip();
        }
    }

    private void Flip() {

        isFacingRight = !isFacingRight;
        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;

        this.transform.localScale = theScale;
    }

    private bool IsAlive() {

        if (health > 0) {

            return true;
        } else {

            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.collider.gameObject.tag == "Projectile") {

            this.health -= 30f;
        }
    }

    private void OnDrawGizmosSelected() {

        //Esferas con los rangos de vision y ataque
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(this.transform.position.x, this.transform.position.y), visionRange);
        Gizmos.DrawWireSphere(new Vector3(this.transform.position.x, this.transform.position.y), attackRange);
    }

    public void SetAnimation(string status) {

        npcAnimator.SetBool("isAttacking", false);
        npcAnimator.SetBool("isWalking", false);

        npcAnimator.SetBool(status, true);
    }

    public CharacterType GetCharacterType() {

        return this.characterType;
    }

    public void SetCharacterType(CharacterType type) {

        this.characterType = type;
    }

    public bool GetIsTargeting() {

        return this.isTargetting;
    }

    public bool GetIsInAttackZone() {

        return this.isInAttackZone;
    }

    public Vector3 GetTarget() {

        return this.target;
    }
}
