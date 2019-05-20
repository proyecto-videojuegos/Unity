using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour{

    private Rigidbody2D m_Fireball;


    public float fireballSpeed;
    public float fireballLife;
    public GameObject explosion;

    private void Awake() {

        m_Fireball = GetComponent<Rigidbody2D>();

    }
    // Start is called before the first frame update
    void Start(){

        if(PlayerController.sharedInstance.transform.localScale.x < 0) {

            m_Fireball.velocity = new Vector2(-fireballSpeed, m_Fireball.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else{

            m_Fireball.velocity = new Vector2(fireballSpeed, m_Fireball.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }

    }

    // Update is called once per frame
    void Update(){

        Destroy(gameObject, fireballLife);

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.enabled) {

            Instantiate(explosion, this.transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
