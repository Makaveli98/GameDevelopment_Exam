using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] zombies;
    private GameObject player;
    
    [SerializeField]
    private Transform[] spawnPos;

    [SerializeField]
    private TextMeshProUGUI wave_Counter_Text, z_Counter_Text;
    private int z_Amount_ToSpawn = 10, wave_Counter, z_Counter, max_Wave = 2;

    void Awake() {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);
    }

    void Update() {
        StartCoroutine("WaitBeforeSpawnEnemies");
        GameOver();
        // connect the int var to the text UI 
        wave_Counter_Text.text = "Wave  " + wave_Counter;
        z_Counter_Text.text = "Zombies  " + z_Counter;
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
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<PlayerAttack>().enabled = false;
            // player.GetComponent<PlayerSprintAndCrouch>().enabled = false;
            player.GetComponentInChildren<PlayerFootsteps>().enabled = false;
            player.GetComponentInChildren<PlayerMouseLook>().enabled = false;
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
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY);
            for (int i = 0; i < enemies.Length; i++) {
                enemies[i].GetComponent<EnemyController>().enabled = false;
                enemies[i].GetComponent<EnemyController>().navAgent.isStopped = true;
                enemies[i].GetComponent<EnemyController>().navAgent.velocity = Vector3.zero;
                StopCoroutine("WaitBeforeSpawnEnemies");
            }
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
            player.GetComponent<PlayerAttack>().enabled = false;
            player.GetComponentInChildren<PlayerFootsteps>().enabled = false;
        }
    }
}
