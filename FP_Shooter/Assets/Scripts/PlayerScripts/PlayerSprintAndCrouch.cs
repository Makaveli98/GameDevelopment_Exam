using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour {

    private PlayerMovement player_Movement;
    private WeaponManager weapon_manager;
    private CharacterController char_Controller;
    private PlayerAttack player_Attack;
    private PlayerFootsteps footsteps_Sound;

    [HideInInspector]
    public float move_Speed = 5f, crouch_Speed = 2.5f, sprint_Speed = 10f;

    private Transform player_Height;
    private float crouch_Height = 1f, stand_Height = 1.6f;

    private float walk_Volume_Min = 0.2f, walk_Volume_Max =  0.6f;
    private float crouch_Volume = 0.1f, sprint_Volume = 1f;
   
    private float walk_Step_Distance = 0.4f, crouch_Step_Distance = 0.5f, sprint_Step_Distance = 0.25f;

    [HideInInspector]
    public bool is_Crouching, is_Sprinting;

    void Awake() {
        player_Movement = GetComponent<PlayerMovement>();
        footsteps_Sound = GetComponentInChildren<PlayerFootsteps>();
        player_Height = transform.GetChild(0);
        weapon_manager = GetComponent<WeaponManager>();
        player_Attack = GetComponent<PlayerAttack>();
    }

    void Start() {
        footsteps_Sound.step_Distance = walk_Step_Distance;
        footsteps_Sound.min_Volume = walk_Volume_Min;
        footsteps_Sound.max_Volume = walk_Volume_Max;
    }

    void Update() {
        Crouch();
        Sprint();
    }

    void Crouch() {
        if (Input.GetKeyDown(KeyCode.C) && !is_Sprinting) {
            // if we are not crouching - crouch
            if (!is_Crouching) {
                player_Height.localPosition = new Vector3(0f, crouch_Height, 0f);
                player_Movement.speed = crouch_Speed;

                footsteps_Sound.step_Distance = crouch_Step_Distance;
                footsteps_Sound.min_Volume = crouch_Volume;
                footsteps_Sound.max_Volume = crouch_Volume;

                is_Crouching = true;

            // if we are crouching - stand up
        } else {
                player_Height.localPosition = new Vector3(0f, stand_Height, 0f);
                player_Movement.speed = move_Speed;


                footsteps_Sound.step_Distance = walk_Step_Distance;
                footsteps_Sound.min_Volume = walk_Volume_Min;
                footsteps_Sound.max_Volume = walk_Volume_Max;

                is_Crouching = false;
               
            }
     
        }
    }

    void Sprint() {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching && !player_Attack.is_Aiming) {
            if (!is_Sprinting) {

                // play zoomout animation
                // weapon_manager.GetCurrentSelectedWeapon().PLay_Zoom_OutAnimation();
 
                player_Movement.speed = sprint_Speed;

                footsteps_Sound.step_Distance = sprint_Step_Distance;
                footsteps_Sound.min_Volume = sprint_Volume;
                footsteps_Sound.max_Volume = sprint_Volume;

                is_Sprinting = true;

            }

        } // if key down

        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching) {
            if (is_Sprinting) {
                player_Movement.speed = move_Speed;

                footsteps_Sound.step_Distance = walk_Step_Distance;
                footsteps_Sound.min_Volume = walk_Volume_Min;
                footsteps_Sound.max_Volume = walk_Volume_Max;

                is_Sprinting = false;

            }
            
        } // if key up

    } // sprint

} // class
