using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
    private Button start_Game;


    void Awake() {
        start_Game = GetComponent<Button>();
    }

    void Start() {
        start_Game.onClick.AddListener(GameStart);
    }

    void GameStart() {
        SceneManager.LoadScene("GameScene");
    }
    
}
