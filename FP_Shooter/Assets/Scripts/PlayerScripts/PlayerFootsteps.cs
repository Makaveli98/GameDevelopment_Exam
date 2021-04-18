using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour {

    private AudioSource footstep_Sound;

    [SerializeField]
    private AudioClip[] footstep_Clips;

    [SerializeField]
    private AudioClip fall_Clip;

    private float fall_Volume = 0.5f; 

    private CharacterController character_Controller;
    
    [HideInInspector]
    public float min_Volume, max_Volume;

    private float accumulated_Distance;

    [HideInInspector]
    public float step_Distance;

    void Awake() {
        footstep_Sound = GetComponent<AudioSource>();
        character_Controller = GetComponentInParent<CharacterController>();
    }

    void Update() {
        Player_Footstep_Sound();
    }

    void Player_Footstep_Sound() {
        if (!character_Controller.isGrounded) {
            return;
        }

        if (character_Controller.velocity.sqrMagnitude > 0) {
            accumulated_Distance += Time.deltaTime;

            if (accumulated_Distance > step_Distance) {
                footstep_Sound.volume = Random.Range(min_Volume, max_Volume);
                footstep_Sound.clip = footstep_Clips[Random.Range(0, footstep_Clips.Length)];
                footstep_Sound.Play();

                accumulated_Distance = 0f;
            }
        } else {
            accumulated_Distance = 0f;
        }
    }

    public void Player_Fall_Sound() {
        footstep_Sound.volume = fall_Volume;
        footstep_Sound.clip = fall_Clip;
        footstep_Sound.Play();
    }
}