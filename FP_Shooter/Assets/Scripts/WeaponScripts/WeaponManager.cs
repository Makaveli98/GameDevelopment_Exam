﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    [SerializeField]
    private WeaponHandler[] weapons;

    private int current_Weapon_Index;

    void Start() {
        current_Weapon_Index = 0;

        weapons[current_Weapon_Index].gameObject.SetActive(true);
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            TurnOn_SelectedWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            TurnOn_SelectedWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            TurnOn_SelectedWeapon(2);
            GetCurrentSelectedWeapon().Play_IdleAnimation(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            TurnOn_SelectedWeapon(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            TurnOn_SelectedWeapon(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            TurnOn_SelectedWeapon(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            TurnOn_SelectedWeapon(6);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            TurnOn_SelectedWeapon(7);
        }

    }

    void TurnOn_SelectedWeapon(int weaponIndex) {
        if (current_Weapon_Index == weaponIndex) {
            return;
        }

        // turn off the current weapon
        weapons[current_Weapon_Index].gameObject.SetActive(false);

        // turn on the selected weapon
        weapons[weaponIndex].gameObject.SetActive(true);

        // store the current selected index
        current_Weapon_Index = weaponIndex;

    } 


    // returns info about the weapon handler
    public WeaponHandler GetCurrentSelectedWeapon() {
        return weapons[current_Weapon_Index];
    }

} // class
