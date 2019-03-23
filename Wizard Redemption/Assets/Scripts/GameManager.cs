using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enumerado para los distintos estados
public enum GameState {

    menu,
    inGame,
    gameOver
}


public class GameManager : MonoBehaviour {

    //Variable que referencia al propio Game Manager
    public static GameManager sharedInstance;

    //En que estado del juego nos encontramos
    public GameState currentGameState = GameState.menu;

    private void Awake() {

        sharedInstance = this;
    }

    public void Start() {

        BackToMenu();
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.S)) {

            StartGame();
        }
    }


    //Inicia el juego
    public void StartGame() {
        SetGameState(GameState.inGame);

    }

    //Sera llamado cuando el jugador muera
    public void GameOver() {
        SetGameState(GameState.gameOver);

    }

    //Para volver al menu principal
    public void BackToMenu() {
        SetGameState(GameState.menu);

    }

    //Para cambiar el estado del juego
    void SetGameState(GameState newGameState) {

        if(newGameState == GameState.menu) {

            //Hay que prepara la escena que muestra el menu
        }else if(newGameState == GameState.inGame) {

            //Hay que prepara la escena para jugar
        } else if(newGameState == GameState.gameOver) {

            //Hay que prepara la escena para el gameover
        }

        //Asignamos el estado del juego por parametro
        this.currentGameState = newGameState;
    }
}
