using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] zombies;

    [SerializeField]
    private Transform[] spawnPos;

    [SerializeField]
    private GameObject crosshair, waveCounter_UI, zombieCounter_UI, hp_Bar_BG, hp_Bar, hp_Sign, afterGame_WaveC, 
    game_Over, r_Button, r_Button_2, victory, text_Gameobj;
    private GameObject player;
    
    [SerializeField]
    private Button restart_Button, restart_Button_2;


    [SerializeField]
    private TextMeshProUGUI wave_Counter_Text, z_Counter_Text, after_Game_Wave_C, gameOver, victory_Text, text;
    private int z_Amount_ToSpawn = 10, wave_Counter, z_Counter, max_Wave = 10;

    void Start() {
        restart_Button.onClick.AddListener(RestartGame);
        restart_Button_2.onClick.AddListener(RestartGame);
        Invoke("Set_TextInActive", 5f);
        Invoke("Set_ButtonActive", 5f);
    }

    void Awake() {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
    }

    void Update() {
        StartCoroutine("WaitBeforeSpawnEnemies");
        GameOver();
        // connect the int var to the text UI 
        wave_Counter_Text.text = "Wave  " + wave_Counter;
        z_Counter_Text.text = "Zombies  " + z_Counter;

        after_Game_Wave_C.text = "Wave " + wave_Counter;
    }

    void SpawnEnemy(int zombiesToSpawn) {
        for (int i = 0; i < zombiesToSpawn; i++) {
            // puts the amount of the var zombies and spawnPos in a random range
            int zombieIndex = Random.Range(0, zombies.Length);
            int spawnIndex = Random.Range(0, spawnPos.Length);
            // copy the gameobject on the given location
            Instantiate(zombies[zombieIndex], spawnPos[spawnIndex].transform.position, spawnPos[spawnIndex].transform.rotation);

        }
    }

    IEnumerator WaitBeforeSpawnEnemies() {
        // wait 3 sec before executing the method WaveCounter
        yield return new WaitForSeconds(3);

        WaveCounter();
    }

    void WaveCounter() {
        // finds the amount of the type EnemyController
        z_Counter = FindObjectsOfType<EnemyController>().Length;
        // if we reach max wave and the zombies count is on 0
        /// THEN stop spawning
        if (wave_Counter == max_Wave && z_Counter <= 0) {
            StopCoroutine("WaitBeforeSpawnEnemies");
            player.GetComponent<PlayerAttack>().enabled = false;
            victory.SetActive(true);
            
            // turn off UI
            crosshair.SetActive(false);
            waveCounter_UI.SetActive(false);
            zombieCounter_UI.SetActive(false);

            hp_Bar_BG.SetActive(false);
            hp_Bar.SetActive(false);
            hp_Sign.SetActive(false); 

            r_Button.SetActive(true);
            r_Button_2.SetActive(false);     
        } 
        // else keep spawning
        else if (z_Counter <= 0) {
            z_Amount_ToSpawn += 10;            
            wave_Counter ++;
            SpawnEnemy(z_Amount_ToSpawn);
        }
    }

    void GameOver() {
        if (player.GetComponent<PlayerHP>().current_HP <= 0) {
            // search for an array of objects with the tag "Enemy" 
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY);
            // loop the array 
            for (int i = 0; i < enemies.Length; i++) {
                // turn off the functions of the gameobjects
                enemies[i].GetComponent<EnemyController>().enabled = false;
                enemies[i].GetComponent<EnemyController>().navAgent.isStopped = true;
                enemies[i].GetComponent<EnemyController>().navAgent.velocity = Vector3.zero;
                // stop spawning new enemies
                StopCoroutine("WaitBeforeSpawnEnemies");
            }
            // turn off  the functions of the player
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
            player.GetComponent<PlayerAttack>().enabled = false;
            // player.GetComponentInChildren<PlayerMouseLook>().enabled = false;
            player.GetComponentInChildren<PlayerFootsteps>().enabled = false;
            
            // turn off the UI
            crosshair.SetActive(false);

            waveCounter_UI.SetActive(false);
            zombieCounter_UI.SetActive(false);

            hp_Bar_BG.SetActive(false);
            hp_Bar.SetActive(false);
            hp_Sign.SetActive(false); 

            afterGame_WaveC.SetActive(true);
            game_Over.SetActive(true);
            r_Button.SetActive(true);
            r_Button_2.SetActive(false);
        }
    }

    void RestartGame() {
        SceneManager.LoadScene("GameScene");
    }

    void Set_TextInActive() { 
        text_Gameobj.SetActive(false);
    }

    void Set_ButtonActive() {
        r_Button_2.SetActive(true);
    }
}
