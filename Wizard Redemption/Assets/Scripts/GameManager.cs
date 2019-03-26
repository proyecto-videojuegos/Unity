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

    public Canvas menuCanvas, gameCanvas, gameOverCanvas;

    private void Awake() {

        sharedInstance = this;
    }

    public void Start() {

        BackToMenu();
    }

    private void Update() {

        if (Input.GetButtonDown("Start") 
            && 
            this.currentGameState != GameState.inGame) {

            StartGame();
        }

        if (Input.GetButtonDown("Pause")) {

            BackToMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {

            ExitGame();
        }
    }


    //Inicia el juego
    public void StartGame() {
        SetGameState(GameState.inGame);
        
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

        CameraFollow cameraFollow = camera.GetComponent<CameraFollow>();

        cameraFollow.ResetCameraPosition();

        if(PlayerController.sharedInstance.transform.position.x > 10) {

            LevelGenerator.sharedInstance.RemoveAllTheBlocks();
            LevelGenerator.sharedInstance.GenerateInitialBlocks();
            
        }

        PlayerController.sharedInstance.StartGame();

    }

    //Sera llamado cuando el jugador muera
    public void GameOver() {
        SetGameState(GameState.gameOver);

    }

    //Para volver al menu principal
    public void BackToMenu() {
        SetGameState(GameState.menu);

    }

    //Para finalizar la ejecucion del videojuego
    public void ExitGame() {

        Application.Quit();
    }

    //Para cambiar el estado del juego
    void SetGameState(GameState newGameState) {

        //Hay que prepara la escena que muestra el menu
        if (newGameState == GameState.menu) {

            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;

        //Hay que prepara la escena para jugar
        } else if(newGameState == GameState.inGame) {

            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;

            //Hay que prepara la escena para el gameover    
        } else if(newGameState == GameState.gameOver) {

            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;
        }

        //Asignamos el estado del juego por parametro
        this.currentGameState = newGameState;
    }
}
