using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour{

    public static EnemyRespawn sharedInstance;

    public GameObject enemyPrefab;
    GameObject enmeyInstance;
    Transform enemyRespawnPosition;

    private void Awake() {

        sharedInstance = this;
    }

    // Start is called before the first frame update
    public void StartRespawn(){

        enemyRespawnPosition = this.transform;
        StartCoroutine("Respawn");
    }

    

    IEnumerator Respawn() {
        //Si no hay enemigos
        while(GameObject.Find("enemyPrefab.name") == null) {

            if(GameManager.sharedInstance.currentGameState == GameState.inGame) {

                enmeyInstance = Instantiate(enemyPrefab, enemyRespawnPosition.position, enemyRespawnPosition.rotation);
            }

            yield return new WaitForSecondsRealtime(5f);
        }
    }
}
