using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    [SerializeField]
    private Image hp_Bar;

    void Awake() {
        hp_Bar = GameObject.Find("HP Bar").GetComponent<Image>();
    }

    public void Display_HP(int hp_Value) {
        hp_Bar.fillAmount = hp_Value;
    }

}
