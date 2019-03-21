/*
 * NAMESPACE
 *
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    //PROPIEDADES
    //Variable global y estatica (una para todas las monedas)
    public static int coinsCount = 0;

    //MÉTODOS

    // Start is called before the first frame update
    void Start()
    {
        Coin.coinsCount++;
        Debug.Log("El juego ha comenzado, hay " + Coin.coinsCount + " monedas");
        
    }

    // Update is called once per frame
    void Update()
    {
   

    }

    //Se inicia cuando otro collider entra en contacto con el de este objeto
    private void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.CompareTag("Player") /* == true */)
        {
            
            Coin.coinsCount--;

            Debug.Log("Monedas restantes: " + Coin.coinsCount);

            if (Coin.coinsCount == 0)
            {

                Debug.Log("El juego ha terminado");

                GameObject gameManager = GameObject.Find("GameManager");

                Destroy(gameManager);

                GameObject[] fireworksSystem = GameObject.FindGameObjectsWithTag("Fireworks");

                foreach(GameObject fireworks in fireworksSystem){

                    fireworks.GetComponent<ParticleSystem>().Play();
                }
            }
            Destroy(gameObject);

        }

       
    }
}
