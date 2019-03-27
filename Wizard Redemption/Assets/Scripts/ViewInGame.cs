﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{

    public Text collectableLabel;

    public Text scoreLabel;

    public Text maxScoreLabel;

    // Update is called once per frame
    void Update()
    {

        if(GameManager.sharedInstance.currentGameState == GameState.inGame ||
           GameManager.sharedInstance.currentGameState == GameState.gameOver) {

            int currentObjects = GameManager.sharedInstance.collectedObjects;

            this.collectableLabel.text = currentObjects.ToString();
        }

        if(GameManager.sharedInstance.currentGameState == GameState.inGame) {

            float travelledDistance = PlayerController.sharedInstance.GetDistance();
            this.scoreLabel.text = "Score\n" + travelledDistance.ToString("f0");

            float maxscore = PlayerPrefs.GetFloat("maxscore", 0);
            this.maxScoreLabel.text = "MaxScore\n" + maxscore.ToString("f0");
        }
        
    }
}
