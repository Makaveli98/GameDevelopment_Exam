using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {
    // [SerializeField]
    public int current_HP;
    [SerializeField]
    private int max_HP;

    void Start() {
        current_HP = max_HP;
    }

    public void Z_ApplyDamage(int z_damage) {
        current_HP -= z_damage;
    }

}
