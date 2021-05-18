using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {
    public int current_HP;
    [SerializeField]
    private int max_HP;
    private PlayerUI p_UI;

    void Awake() {
        p_UI = GameObject.Find("HP Bar").GetComponent<PlayerUI>();
    }

    void Start() {
        current_HP = max_HP;
    }

    public void Z_ApplyDamage(int z_damage) {
        current_HP -= z_damage;
        p_UI.Display_HP(current_HP);
    }

}
