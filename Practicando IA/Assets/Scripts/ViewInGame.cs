using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{
    public Text scoreLabel;

    public Text maxScoreLabel;

    // Update is called once per frame
    void Update() {

        if (GameManager.sharedInstance.currentGameState == GameState.inGame) {

            this.scoreLabel.text = "Score\n" + PlayerController.sharedInstance.GetPlayerScore();

            float maxscore = PlayerPrefs.GetFloat("maxscore");
            float maxscore1 = maxscore;
            //this.maxScoreLabel.text = "MaxScore\n" + maxscore1.ToString("f0");
        }

    }
}
