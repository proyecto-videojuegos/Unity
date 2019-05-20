using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]


public class PlayerController : MonoBehaviour{

    public static PlayerController sharedInstance;

    Rigidbody2D m_rigidbody;
    Animator m_Animator;
    public GameObject m_StartPoint;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask GroundLayer;
    public bool isGrounded;

    public GameObject projectile;
    public Transform shotPoint;

    [SerializeField] private float jumpForce = 400f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    private Vector3 velocity = Vector3.zero;
    public float speed;

    private float playerHealth, playerMana, playerScore, playerMaxScore, melePlayerDamage, rangePlayerDamage;

    //Atributo booleaneo del metodo Flip()
    bool M_FacingRight = true;

    private float recoveringManaTimer;

    //Constanes
    public const float INITIAL_HEALTH = 100, INITIAL_MANA = 100, MAX_HEALTH = 100, MAX_MANA = 100, MIN_HEALTH = 0, MIN_MANA = 0, CAST_COST = 15;

    private void Awake() {

        sharedInstance = this;

        m_rigidbody = this.GetComponent<Rigidbody2D>();
        m_Animator = this.GetComponent<Animator>();

        this.transform.position = m_StartPoint.transform.position;

        //Declaramos los valores iniciales del personaje
        this.playerHealth = INITIAL_HEALTH;
        this.playerMana = INITIAL_MANA;
        recoveringManaTimer = 1f;

        rangePlayerDamage = 5f;
    }


    // Start is called before the first frame update
    public void StartGame(){

        this.transform.position = m_StartPoint.transform.position;
        this.playerHealth = INITIAL_HEALTH;
        this.playerMana = INITIAL_MANA;
        recoveringManaTimer = 1f;

        playerScore = 0;
        playerMaxScore = PlayerPrefs.GetFloat("maxscore");
        GameManager.sharedInstance.SetEnemyDeathCount(0);
    }

    // Update is called once per frame
    void Update(){

        if (GameManager.sharedInstance.currentGameState == GameState.inGame) {

            CasualInputs();

            //En el futuro multiplicar por el scoreEnemyValue segun el tipo de enemigo que muere
            playerScore = GameManager.sharedInstance.GetEnemyDeathCount() * 10;

            if (!IsAlive()) {

                GameManager.sharedInstance.GameOver();

                m_Animator.SetBool("isDying", true);
                if(playerScore > playerMaxScore) {

                    PlayerPrefs.SetFloat("maxscore", playerScore);
                }
            }
        }

    }

    private void FixedUpdate() {

        if (GameManager.sharedInstance.currentGameState == GameState.inGame) {

            recoveringMana();

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, GroundLayer);
        }

    }

    private void CasualInputs() {

        if (Input.GetButtonDown("Fire1") && playerMana > CAST_COST) {

            Attack();
        }
    }

    public void Move(float move) {

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, m_rigidbody.velocity.y);
        // And then smoothing it out and applying it to the character
        m_rigidbody.velocity = Vector3.SmoothDamp(m_rigidbody.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (move > 0 && !M_FacingRight) {

            Flip();
        } else if (move < 0 && M_FacingRight) {

            Flip();
        }
    }

    public void Jump() {

        m_rigidbody.AddForce(new Vector2(0f, jumpForce));      
    }

    private void Attack() {

        m_Animator.SetTrigger("Attack");
    }

    private void Shot() {

        Instantiate(projectile, shotPoint.position, transform.rotation);
        playerMana -= CAST_COST;
    }

    private void recoveringMana() {

        recoveringManaTimer -= Time.fixedDeltaTime;

        if (recoveringManaTimer <= 0 && playerMana < MAX_MANA) {

            playerMana += 5;
            recoveringManaTimer = 1f;
        }
    }

    //Metodo para voltear personaje y su variable
    private void Flip() {
        M_FacingRight = !M_FacingRight;

        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;

        this.transform.localScale = theScale;
    }

    private bool IsAlive() {

        if (playerHealth > MIN_HEALTH) {

            return true;
        } else {

            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if(collision.collider.gameObject.tag == "werewolfclaw1") {

            this.playerHealth -= 10f;
        }
        if (collision.collider.gameObject.tag == "werewolfclaw2") {

            this.playerHealth -= 10f;
        }
    }

    public float GetHealth() {

        return this.playerHealth;
    }

    public float GetMana() {

        return this.playerMana;
    }

    public float GetMelePlayerDamage() {

        return this.melePlayerDamage;
    }

    public float GetRangePlayerDamage() {

        return this.rangePlayerDamage;
    }

    public float GetPlayerScore() {

        return this.playerScore;
    }
}
