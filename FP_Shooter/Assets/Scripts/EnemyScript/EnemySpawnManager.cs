using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] zombies;

    [SerializeField]
    private Transform[] spawnPos;

    int zombies_Amount = 20;

    void Start() {
        SpawnEnemy(zombies_Amount);

    }

    void SpawnEnemy(int zombiesToSpawn) {
        for (int i = 0; i < zombiesToSpawn; i++) {
            
            int zombieIndex = Random.Range(0, zombies.Length);
            int spawnIndex = Random.Range(0, spawnPos.Length);

            Instantiate(zombies[zombieIndex], spawnPos[spawnIndex].transform.position, spawnPos[spawnIndex].transform.rotation);
        }
    }

}
