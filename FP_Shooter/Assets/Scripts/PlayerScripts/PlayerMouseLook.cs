using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseLook : MonoBehaviour {

    [SerializeField]
    private Transform playerRotation, lookRotation;

    
    [SerializeField]
    private bool invert;


    [SerializeField]
    private float sensivity = 1.5f;

    [SerializeField]
    private Vector2 default_Look_Limits = new Vector2(-70, 80f);

    private Vector2 look_Angles;

    private Vector2 current_Mouse_Look;

    void Start() {

        Cursor.lockState = CursorLockMode.Locked; // sets cursor on lock state
    }

    void Update() {

        Lock_And_Unlock_Cursor();
        // checks if cursor is locked only then will the method LookAround apply
        if (Cursor.lockState == CursorLockMode.Locked ) { 
            LookAround(); 
        }
    }

    void Lock_And_Unlock_Cursor() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Cursor.lockState == CursorLockMode.Locked) {
                Cursor.lockState = CursorLockMode.None;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    } // lock and unlock

    void LookAround() {

        current_Mouse_Look = new Vector2(Input.GetAxisRaw(MouseAxis.MOUSE_Y), Input.GetAxisRaw(MouseAxis.MOUSE_X)); // same as with input key but now with the mouse

        look_Angles.x += current_Mouse_Look.x * sensivity * (invert ? 1f : -1f); // this will move the gameobject down and up the x Axis (and if checked invert the look)
        look_Angles.y += current_Mouse_Look.y * sensivity; 

        look_Angles.x = Mathf.Clamp(look_Angles.x, default_Look_Limits.x, default_Look_Limits.y); // this wil give the Vector2, look_Angles.x, a limit to rotate at


        lookRotation.localRotation = Quaternion.Euler(look_Angles.x, 0f, 0f); // this will rotate what is put in the Transform field
        playerRotation.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f); // this will rotate what is put in the Transform field
        
    } // look around

} // class
