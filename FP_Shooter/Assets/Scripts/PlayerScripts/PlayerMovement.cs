using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private CharacterController character_Controller;
    private PlayerFootsteps player_Footsteps;
    private WeaponManager weapon_Manager;
    private PlayerSprintAndCrouch sprint_Crouch;
    private PlayerAttack player_Attack;


    [HideInInspector]
    public Vector3 move_Direction;

    private Vector3 test_Direction;

    public float speed = 10f;
    private float gravity = 20f;
    private float jump_Force = 10f;
    // private float crouch_jump_Force = 7.5f;
    [SerializeField]
    private float vertical_Velocity;
    [SerializeField]
    public bool is_Walking;
    [SerializeField]
    private bool is_Jumping;
    


    void Awake() {

        character_Controller = GetComponent<CharacterController>();
        player_Footsteps = GetComponentInChildren<PlayerFootsteps>();
        weapon_Manager = GetComponent<WeaponManager>();
        sprint_Crouch = GetComponent<PlayerSprintAndCrouch>();
        player_Attack = GetComponent<PlayerAttack>();
        
    }

    void Update() {
        MoveThePlayer();
        CheckIfWalking();
        CheckIfJumping();
    }

    void MoveThePlayer() {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL)); // this will move the gameobject (player) down the x and z axis

        move_Direction = transform.TransformDirection(move_Direction); // this will tell the gameobject to move from local space to world space
        move_Direction *= speed * Time.deltaTime; // tells the gameobject at what speed it will move

        ApllyGravity();  // applies gravity
        
        character_Controller.Move(move_Direction); // and this will make the gameobject actually move


        // if not jumping
        if (!is_Jumping) {
            // then check if is walking
            if (is_Walking) {
                // play walk anim
                weapon_Manager.GetCurrentSelectedWeapon().Play_WalkAnimation();
            } else {
                // stop walk anim
                weapon_Manager.GetCurrentSelectedWeapon().Stop_WalkAnimation();
            }
        }

        // if not jumping
        if (!is_Jumping) {
            // then check if is walking
            if (is_Walking && sprint_Crouch.is_Crouching) {
                // play crouch anim
                weapon_Manager.GetCurrentSelectedWeapon().Play_CrouchAnimation();
            } 
        }

        // if not jumping
        if (!is_Jumping) {
            // then check if is walking
            if (is_Walking && sprint_Crouch.is_Sprinting) {
                // play run anim
                weapon_Manager.GetCurrentSelectedWeapon().Play_RunAnimation();
            }
        }

        // if not jumping
        if (!is_Jumping) {
            // then check if is walking
            if (is_Walking && player_Attack.is_Aiming && !sprint_Crouch.is_Crouching) {
                // play aim_walk anim
                weapon_Manager.GetCurrentSelectedWeapon().Play_AimWalk_Animation();
            }
        }

        // if not jumping
        if (!is_Jumping) {
            // then check if is walking
            if (is_Walking && player_Attack.is_Aiming && sprint_Crouch.is_Crouching) {
                // play aim_crouch anim
                weapon_Manager.GetCurrentSelectedWeapon().Play_AimCrouch_Animation();
            }
        }

    } // move player

    void ApllyGravity() {
        
        vertical_Velocity -= gravity * Time.deltaTime; // whatever the number is (now none) minus gravity BUT
        
        //jump
        PlayerJump(); // if you jump THEN

        move_Direction.y = vertical_Velocity * Time.deltaTime; // the vector 3 Y axis will go down from the given number at gravity

    }  // apply gravity

    void PlayerJump() {
        if (!is_Jumping) {
            if (character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
                vertical_Velocity = jump_Force;

                // weapon_Manager.GetCurrentSelectedWeapon().Stop_WalkAnimation();
                // weapon_Manager.GetCurrentSelectedWeapon().Stop_AimWalk_Animation();

                // weapon_Manager.GetCurrentSelectedWeapon().Stop_CrouchAnimation();
                // weapon_Manager.GetCurrentSelectedWeapon().Stop_AimCrouch_Animation();

                // weapon_Manager.GetCurrentSelectedWeapon().Stop_RunAnimation();
                // weapon_Manager.GetCurrentSelectedWeapon().Play_IdleAnimation(false);

                is_Jumping = false;
            } 

            if (is_Jumping || character_Controller.isGrounded) {
                if (Input.GetKeyUp(KeyCode.Space)) {
                    weapon_Manager.GetCurrentSelectedWeapon().Play_IdleAnimation(true);
                }
                
            }
            // else if (character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space) && sprint_Crouch.is_Crouching) {

            //     jump_Force = crouch_jump_Force;
            //     vertical_Velocity = jump_Force;

            //     is_Jumping = false;
            // }

            // if (character_Controller.isGrounded) { 
            //     player_Footsteps.Player_Fall_Sound();
            // }
        }
    }

    void CheckIfJumping() {
        if (vertical_Velocity > 0) {
            is_Jumping = true;
            is_Walking = false;
        } else if (vertical_Velocity <= 0) {
            is_Jumping = false;
        }
    }

    void CheckIfWalking() {
        if (character_Controller.velocity.sqrMagnitude >= 3) {
            is_Walking = true;
        } else if (character_Controller.velocity.sqrMagnitude < 3) {
            is_Walking = false;
        }

    }

} // class

