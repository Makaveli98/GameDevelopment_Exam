using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private WeaponManager weapon_Manager;
    private CharacterController c_Controller;
    private PlayerSprintAndCrouch sprint_Crouch_Script;
    private PlayerMovement player_Movement_Script;
    public bool is_Aiming;

    // private float aim_Crouch_Speed = 3f;
    // private float aim_Walk_Speed = 5f;


    void Awake() {
        weapon_Manager = GetComponent<WeaponManager>();
        c_Controller = GetComponent<CharacterController>();
        sprint_Crouch_Script = GetComponent<PlayerSprintAndCrouch>();
        player_Movement_Script = GetComponent<PlayerMovement>();
    }

    void Update() {
        Zoom_In_And_Out();

    }

    void Zoom_In_And_Out () {
        if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM) {
            if (Input.GetMouseButtonDown(1)) {
                if (!is_Aiming) {
                    weapon_Manager.GetCurrentSelectedWeapon().PLay_Zoom_InAnimation();
                    weapon_Manager.GetCurrentSelectedWeapon().Play_IdleAnimation(false);
                    is_Aiming = true;


                    // if mouse button is being pressed
                    // that means if camera is zoomed in
                    // then check if player is moving and crouching
                    // only then play the crouch_aim animation
                    if (sprint_Crouch_Script.is_Crouching) {
                        weapon_Manager.GetCurrentSelectedWeapon().Play_AimCrouch_Animation(); 

                    } // check for crouch


                
                    // the same with crouch but now with walk
                    if (!sprint_Crouch_Script.is_Crouching) {
                        weapon_Manager.GetCurrentSelectedWeapon().Play_AimWalk_Animation();

                    } // check for walk

                }

            } // if mouse button being pressed
            
            if (Input.GetMouseButtonUp(1)) {
                if (is_Aiming) {
                    weapon_Manager.GetCurrentSelectedWeapon().PLay_Zoom_OutAnimation();
                    weapon_Manager.GetCurrentSelectedWeapon().Play_IdleAnimation(true);
                    is_Aiming = false;

                    // if mouse button being released
                    // that means the camera is zoomed out
                    // then check if player is moving and crouching
                    // then stop the aim_crouch animation and play the normal crouch animation
                    if (sprint_Crouch_Script.is_Crouching && player_Movement_Script.is_Walking || !player_Movement_Script.is_Walking) {
                        weapon_Manager.GetCurrentSelectedWeapon().Stop_AimCrouch_Animation(); 

                    } // check for crouch

                    if (player_Movement_Script.is_Walking && !sprint_Crouch_Script.is_Crouching || 
                        !player_Movement_Script.is_Walking && !sprint_Crouch_Script.is_Crouching) {

                        weapon_Manager.GetCurrentSelectedWeapon().Stop_AimWalk_Animation();

                    } // check for walk

                }


            } // if mouse button being released
            
        }

    } // Zoom_In_And_Out
   
} // class
