using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour{

    public float speed = 0.0f;

    private Rigidbody2D rigidbody;

    private void Awake() {

        this.rigidbody = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void FixedUpdate(){

        this.rigidbody.velocity = new Vector2(speed, 0.0f);

        float parentPosition = this.transform.parent.position.x;

        if(this.transform.position.x >= 30f){

            this.transform.position = new Vector3(parentPosition - 30f, this.transform.position.y, this.transform.position.z);
        }
        
    }
}
