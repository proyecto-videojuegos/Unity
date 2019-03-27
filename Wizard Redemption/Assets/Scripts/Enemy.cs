using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float runningSpeed = 1.5f;

    private Rigidbody2D rigidbody;

    public static bool turnAround;

    public Vector3 startPosition;

    private void Awake() {

        rigidbody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    private void Start() {

        this.transform.position = startPosition;
    }

    private void FixedUpdate() {

        float currentRunningSpeed = runningSpeed;
        
            if(turnAround == true) {

                //La velocidad es positiva
                currentRunningSpeed = runningSpeed;
                //Gira al enemigo en 180 grados en horizontal. 
                this.transform.eulerAngles = new Vector3(0, 180.0f, 0);
            } else {

                //La velocidad es negativa
                currentRunningSpeed = -runningSpeed;
                this.transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if (GameManager.sharedInstance.currentGameState == GameState.inGame) {

                rigidbody.velocity = new Vector2(currentRunningSpeed,//Velocidad en el eje de las X
                                                 rigidbody.velocity.y//Velocidad en el eje de las y
                                                 );

            }

        
    }
}
