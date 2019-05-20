using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType {

    health,
    mana
}

public class PlayerBar : MonoBehaviour
{
    private Image slider;

    public BarType type;

    // Start is called before the first frame update
    void Start() {

        slider = GetComponent<Image>();

        switch (this.type) {
            case BarType.health:
                this.slider.fillAmount = PlayerController.MAX_HEALTH/100f;
                break;
            case BarType.mana:
                this.slider.fillAmount = PlayerController.MAX_MANA/100f;
                break;
        }

    }

    // Update is called once per frame
    void Update() {

        switch (this.type) {

            case BarType.health:

                float healthAmount = (PlayerController.sharedInstance.GetHealth() / 100f);
                this.slider.fillAmount = healthAmount;
                break;
            case BarType.mana:

                float manaAmount = (PlayerController.sharedInstance.GetMana() / 100f);
                this.slider.fillAmount = manaAmount;
                break;
        }

    }
}
