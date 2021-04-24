using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] zombies;
    
    [SerializeField]
    private Transform[] spawnPos;
    private TextMeshProUGUI wave_Counter_Text, z_Counter_Text;
    private int z_Amount_ToSpawn = 10, wave_Counter, z_Counter;

    void Awake() {
        wave_Counter_Text = GetComponentInChildren<TextMeshProUGUI>();
        z_Counter_Text = GameObject.Find("Zombie Counter").GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        WaveCounter();
        wave_Counter_Text.text = "Wave  " + wave_Counter;
        z_Counter_Text.text = "Zombies  " + z_Counter;
    }

    void SpawnEnemy(int zombiesToSpawn) {
        for (int i = 0; i < zombiesToSpawn; i++) {
            
            int zombieIndex = Random.Range(0, zombies.Length);
            int spawnIndex = Random.Range(0, spawnPos.Length);

            Instantiate(zombies[zombieIndex], spawnPos[spawnIndex].transform.position, Quaternion.identity);

        }
    }

    IEnumerator WaitBeforeSpawnEnemies() {
        yield return new WaitForSeconds(3);



    }

    void WaveCounter() {
        z_Counter = FindObjectsOfType<EnemyController>().Length;
        if (z_Counter <= 0) {
            z_Amount_ToSpawn += 10;
            wave_Counter ++;
            SpawnEnemy(z_Amount_ToSpawn);
        }
    }
}
