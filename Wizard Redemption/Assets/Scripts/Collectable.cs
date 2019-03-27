using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CollectableType {

    healthPotion,
    manaPotion,
    money
}

public class Collectable : MonoBehaviour{

    public CollectableType type = CollectableType.money;

    //Para saber si la moneda ha sido recogida o no
    bool isCollected = false;

    public int value = 0;
    
    //Metodo para activar la moneda y su collider
    void Show() {

        //Activamos la imagen de la moneda
        this.GetComponent<SpriteRenderer>().enabled = true;
        //Activamos el collider
        this.GetComponent<CircleCollider2D>().enabled = true;
        //No hemos recogido la moneda
        isCollected = false;
    }

    //Metodo para desactivar la moneda y su collider
    void Hide() {

        //Desactivamos la imagen de la moneda
        this.GetComponent<SpriteRenderer>().enabled = false;
        //Desactivamos el collider
        this.GetComponent<CircleCollider2D>().enabled = false;
        
    }

    //Metodo para recolectar la moneda
    void Collect() {

        //Hemos recogido la moneda
        isCollected = true;
        //Ocultamos la moneda
        Hide();

        switch (this.type) {

            case CollectableType.money:
                GameManager.sharedInstance.CollectedObject(value);
                break;
            case CollectableType.healthPotion:
                PlayerController.sharedInstance.CollectHealth(value);
                break;
            case CollectableType.manaPotion:
                PlayerController.sharedInstance.CollectMana(value);
                break;
        }
        

    }

    private void OnTriggerEnter2D(Collider2D otherCollider) {
        
        if(otherCollider.tag == "Player") {

            Collect();
        }

    }

}
