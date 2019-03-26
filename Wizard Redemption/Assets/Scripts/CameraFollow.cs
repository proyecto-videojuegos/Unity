using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour{


    public Transform target;

    //posicion de la camara en el eje x e inicio de la altura
    public Vector3 offset = new Vector3(0.2f, 0.0f, -10.0f);

    //Espacio de tiempo donde la camara esta quieta al inicio y luego empieza a seguir al jugador
    public float dampTime = 0.3f;

    public Vector3 velocity = Vector3.zero;

    private void Awake() {

        Application.targetFrameRate = 60;
    }

    public void ResetCameraPosition() {

        //Transforma las coordenadas del personaje en coordenadas para la camara
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);

        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));

        Vector3 destination = point + delta;

        destination = new Vector3(destination.x, offset.y, offset.z);

        this.transform.position = destination;
    }

    // Update is called once per frame
    void Update(){

        
        //Transforma las coordenadas del personaje en coordenadas para la camara
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);

        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));

        Vector3 destination = point + delta;

        destination = new Vector3(target.position.x, offset.y, offset.z);

        this.transform.position = Vector3.SmoothDamp(this.transform.position,
                                                     destination,
                                                     ref velocity,
                                                     dampTime);

    }
}
