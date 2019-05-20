using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {

    inGame,
    pause,
    gameOver
}

public class GameManager : MonoBehaviour{

    public static GameManager sharedInstance;

    //En que estado del juego nos encontramos
    public GameState currentGameState;

    public Canvas pause;

    private float enemyDeathCount = 0;


    private void Awake() {

        sharedInstance = this;
    }

    // Update is called once per frame
    void Update(){

        if (Input.GetKeyDown(KeyCode.Escape)) {

            if(currentGameState == GameState.inGame) {

                PauseGame();
            }else if(currentGameState == GameState.pause) {

                ResumeGame();
            }
        }

        if(currentGameState == GameState.inGame) {

            if (pause != null) {
                pause.enabled = false;
            }
        }
    }

    //Inicia el juego
    public void StartGame() {

        currentGameState = GameState.inGame;
    }

    //Sera llamado cuando el jugador muera
    public void GameOver() {

        SetGameState(GameState.gameOver);
    }

    //Para volver al menu principal
    public void BackToMenu() {

        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void ResumeGame() {

        SetGameState(GameState.inGame);
        if (pause != null) {
            pause.enabled = false;
        }
        Time.timeScale = 1f;
    }
    
    public void PauseGame() {

        SetGameState(GameState.pause);
        if (pause != null) {
            pause.enabled = true;
        }
        Time.timeScale = 0f;
    }

    public void EnemyDie() {

        enemyDeathCount++;
    }

    //Para cambiar el estado del juego
    private void SetGameState(GameState newGameState) {

        this.currentGameState = newGameState;
    }

    public float GetEnemyDeathCount() {

        return this.enemyDeathCount;
    }

    public void SetEnemyDeathCount(float number) {

        this.enemyDeathCount = number;
    }
}
