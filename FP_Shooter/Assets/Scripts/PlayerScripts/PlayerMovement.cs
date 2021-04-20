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

    public float jump_Force = 10f;
    [SerializeField]
    private float vertical_Velocity;
    public bool is_Walking;

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
    }

    void MoveThePlayer() {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL)); // this will move the gameobject (player) down the x and z axis

        move_Direction = transform.TransformDirection(move_Direction); // this will tell the gameobject to move from local space to world space
        move_Direction *= speed * Time.deltaTime; // tells the gameobject at what speed it will move

        ApllyGravity();  // applies gravity
        
        character_Controller.Move(move_Direction); // and this will make the gameobject actually move

    
        // if move then play WALK anim
        if (is_Walking) {
            weapon_Manager.GetCurrentSelectedWeapon().Play_WalkAnimation();
        // if not move then play IDLE anim
        } else if (!is_Walking) {
            weapon_Manager.GetCurrentSelectedWeapon().Stop_WalkAnimation();    

        } // walk anim

        // if move and crouching then play CROUCH anim
        if (is_Walking && sprint_Crouch.is_Crouching) {
            weapon_Manager.GetCurrentSelectedWeapon().Play_CrouchAnimation();
        } // crouch anim

        // if move and sprinting then play RUN anim
        if (is_Walking && sprint_Crouch.is_Sprinting) {
            weapon_Manager.GetCurrentSelectedWeapon().Play_RunAnimation();

        } // run anim

        if (is_Walking && player_Attack.is_Aiming && !sprint_Crouch.is_Crouching) {
            weapon_Manager.GetCurrentSelectedWeapon().Play_AimWalk_Animation();
        }

        if (is_Walking && player_Attack.is_Aiming && sprint_Crouch.is_Crouching) {
            weapon_Manager.GetCurrentSelectedWeapon().Play_AimCrouch_Animation();
            
        }


    } // move player

    void ApllyGravity() {
        
        vertical_Velocity -= gravity * Time.deltaTime; // whatever the number is (now none) minus gravity BUT
        
        //jump
        PlayerJump(); // if you jump THEN

        move_Direction.y = vertical_Velocity * Time.deltaTime; // the vector 3 Y axis will go down from the given number at gravity

    }  // apply gravity

    void PlayerJump() {
        
        if (character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            vertical_Velocity = jump_Force;

            if (character_Controller.velocity.sqrMagnitude <= 0) {
                weapon_Manager.GetCurrentSelectedWeapon().Stop_WalkAnimation();            
            }

            if (character_Controller.isGrounded) { 
                player_Footsteps.Player_Fall_Sound();
            }
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

