using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private CharacterController character_Controller;

    private PlayerFootsteps player_Footsteps;

    [HideInInspector]
    public Vector3 move_Direction;

    public float speed = 5f;
    private float gravity = 20f;

    public float jump_Force = 10f;
    [SerializeField]
    private float vertical_Velocity;

    void Awake() {

        character_Controller = GetComponent<CharacterController>();
        player_Footsteps = GetComponentInChildren<PlayerFootsteps>();
    }

    void Update() {

        MoveThePlayer();
    }

    void MoveThePlayer() {

        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL)); // this will move the gameobject (player) down the x and z axis

        move_Direction = transform.TransformDirection(move_Direction); // this will tell the gameobject to move to a certain position, in this case at any direction
        move_Direction *= speed * Time.deltaTime; // tells the gameobject at what speed it will move

        ApllyGravity();  // applies gravity
        
        character_Controller.Move(move_Direction); // and this will make the gameobject actually move

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

            if (character_Controller.isGrounded) { 
                player_Footsteps.Player_Fall_Sound();
            }
        }
    }

} // class

