using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {
    private Button start_Button;
    private GameObject player;
    private GameObject game_Manager;
    private GameObject start_Game;

    void Awake() {
        start_Button = GetComponentInChildren<Button>();
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
        game_Manager = GameObject.Find("Game Manager");
        start_Game = GameObject.Find("Start Game");
    }

    void Start() {
        start_Button.onClick.AddListener(GameStart);
    }
 
    void GameStart() {
        player.SetActive(true);
        start_Game.SetActive(false);
        game_Manager.SetActive(true);
    }
}
