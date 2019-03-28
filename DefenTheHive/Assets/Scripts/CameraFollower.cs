using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour{

    public static CameraFollower sharedInstance;

    //A quien tiene que seguir la camara
    public GameObject followTarget;

    //Suavizar las rotaciones
    public float movementSmoothness = 1.0f;
    public float rotationSmoothness = 1.0f;

    //Cuando la camara puede seguir al personaje
    public bool canFollow = true;

    private void Awake() {

        sharedInstance = this;
    }

    //Lo ultimo que se ejecuta en cada frame
    private void LateUpdate() {

        if (followTarget == null || canFollow == false) {

            return;
        }

        //Funcion "Lerp", interpolacion lineal, ir de un sitio a otro de una forma suave
        transform.position = Vector3.Lerp(transform.position, 
                                          followTarget.transform.position, 
                                          Time.deltaTime * movementSmoothness
                                          );

        //Funcion "Slerp", es lo mismo que Lerp pero de forma esferica
        transform.rotation = Quaternion.Slerp(transform.rotation, 
                                              followTarget.transform.rotation, 
                                              Time.deltaTime * movementSmoothness
                                              );
    }

}
