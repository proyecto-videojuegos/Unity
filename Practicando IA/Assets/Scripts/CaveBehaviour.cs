using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveBehaviour : MonoBehaviour{

    private void OnTriggerEnter2D(Collider2D otherCollider) {

        if (otherCollider.CompareTag("Player")){

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
