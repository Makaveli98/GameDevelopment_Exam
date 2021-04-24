using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private WeaponManager weapon_Manager;
    private CharacterController c_Controller;
    private PlayerSprintAndCrouch sprint_Crouch_Script;
    private PlayerMovement player_Movement_Script;

    [SerializeField]
    private LayerMask layer;
    private RaycastHit rayHit;

    public bool is_Aiming;
    [HideInInspector]
    public bool is_Shooting, is_Reloading;
    private float fireRate = 15f, nextTimeToFire;
    
    void Awake() {
        weapon_Manager = GetComponent<WeaponManager>();
        c_Controller = GetComponent<CharacterController>();
        sprint_Crouch_Script = GetComponent<PlayerSprintAndCrouch>();
        player_Movement_Script = GetComponent<PlayerMovement>();
    }

    void Update() {
        Zoom_In_And_Out();
        Shoot();
        weapon_Manager.GetCurrentSelectedWeapon().Reload();
    }

    void Zoom_In_And_Out () {
        if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM) {
            if (Input.GetMouseButtonDown(1)) {
                if (!is_Aiming) {
                    weapon_Manager.GetCurrentSelectedWeapon().PLay_Zoom_InAnimation();
                    weapon_Manager.GetCurrentSelectedWeapon().Play_IdleAnimation(false);
                    is_Aiming = true;
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
                    // then disable the aim_crouch_speed
                    if (sprint_Crouch_Script.is_Crouching && player_Movement_Script.is_Walking || 
                        !player_Movement_Script.is_Walking && sprint_Crouch_Script.is_Crouching) {
                            
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

    void Shoot() {
        if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Bullet_Type == WeaponBulletType.BULLET) {
            if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Fire_Type == WeaponFireType.MULTIPLE) {
                if (Input.GetMouseButton(0) && Time.time > nextTimeToFire) {
                    if (!is_Shooting) {
                        // Debug.Log("we can fire with the weapon fire type: Multiple");
                        nextTimeToFire = Time.time + 1f / fireRate;
                        weapon_Manager.GetCurrentSelectedWeapon().Play_ShootAnimation();
                        BulletFired();
                        is_Shooting = true;
                    }
                } 
                if (Input.GetMouseButtonUp(0)) {
                        is_Shooting = false;
                    } 
                
            } 
            else if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Fire_Type == WeaponFireType.SINGLE) {
                if (Input.GetMouseButtonDown(0)) {
                    if (!is_Shooting) {
                        // Debug.Log("we can fire with the weapon fire type: Single");
                        weapon_Manager.GetCurrentSelectedWeapon().Play_ShootAnimation();
                        BulletFired();  
                        is_Shooting = true;

                    }   
                }
                if (Input.GetMouseButtonUp(0)) {
                    is_Shooting = false;
                }
            }
            
        } else if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Bullet_Type == WeaponBulletType.NONE) {
            if (Input.GetMouseButtonDown(0)) {
                weapon_Manager.GetCurrentSelectedWeapon().Play_AttackAnimation();
                // Debug.Log("we can attack with the Axe or Knife");
                
            } 
        }
    }

    void BulletFired() {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayHit, weapon_Manager.GetCurrentSelectedWeapon().range, layer)) {
            Debug.Log("We hitt the" + rayHit.collider.name);
            rayHit.transform.GetComponent<Health>().ApplyDamage(weapon_Manager.GetCurrentSelectedWeapon().damage);
        }
    }
   
} // class