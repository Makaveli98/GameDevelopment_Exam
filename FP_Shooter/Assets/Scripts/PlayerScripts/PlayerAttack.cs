using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private WeaponManager weapon_Manager;

    void Awake() {
        weapon_Manager = GetComponent<WeaponManager>();
    }

    void Update() {
        if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM) {
            if (Input.GetMouseButtonDown(1)) {
                weapon_Manager.GetCurrentSelectedWeapon().PLay_Zoom_InAnimation();
                weapon_Manager.GetCurrentSelectedWeapon().Play_IdleAnimation(false);
                weapon_Manager.GetCurrentSelectedWeapon().Play_WalkAnimation();
                weapon_Manager.GetCurrentSelectedWeapon().Play_CrouchAnimation();

            
            }
            
            if (Input.GetMouseButtonUp(1)) {
                weapon_Manager.GetCurrentSelectedWeapon().PLay_Zoom_OutAnimation();
                weapon_Manager.GetCurrentSelectedWeapon().Play_IdleAnimation(true);
            }
        }
    }
   
}
