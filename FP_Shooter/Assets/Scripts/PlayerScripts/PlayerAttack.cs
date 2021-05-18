using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

    private WeaponManager weapon_Manager;
    private CharacterController c_Controller;
    private PlayerSprintAndCrouch sprint_Crouch_Script;
    private PlayerMovement player_Movement_Script;
    private GameObject crosshair;

    [SerializeField]
    private LayerMask layer;
    private RaycastHit rayHit;

    public bool is_Aiming;
    // [HideInInspector]
    public bool is_Reloading = false;
    private float fireRate = 5f, nextTimeToFire;
    
    void Awake() {
        weapon_Manager = GetComponent<WeaponManager>();
        c_Controller = GetComponent<CharacterController>();
        sprint_Crouch_Script = GetComponent<PlayerSprintAndCrouch>();
        player_Movement_Script = GetComponent<PlayerMovement>();
        crosshair = GameObject.Find("Crosshair");
    }

    void Update() {
        Zoom_In_And_Out();
        Shoot();
        StartCoroutine(Reload());   

    }

    void Zoom_In_And_Out () {
        if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM) {
            if (Input.GetMouseButtonDown(1)) {
                if (!is_Aiming) {
                    weapon_Manager.GetCurrentSelectedWeapon().PLay_Zoom_InAnimation();
                    weapon_Manager.GetCurrentSelectedWeapon().Play_IdleAnimation(false);

                    crosshair.SetActive(false);
                    is_Aiming = true;
                }

            } // if mouse button being pressed
            
            if (Input.GetMouseButtonUp(1)) {
                if (is_Aiming) {
                    weapon_Manager.GetCurrentSelectedWeapon().PLay_Zoom_OutAnimation();
                    weapon_Manager.GetCurrentSelectedWeapon().Play_IdleAnimation(true);

                    crosshair.SetActive(true);
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
                if (Input.GetMouseButton(0) && Time.time > nextTimeToFire && !sprint_Crouch_Script.is_Sprinting 
                    && weapon_Manager.GetCurrentSelectedWeapon().current_Ammo > 0) {

                    nextTimeToFire = Time.time + 1f / fireRate;
                    weapon_Manager.GetCurrentSelectedWeapon().Play_ShootAnimation();
                    BulletFired();
                } 

            } // shooting with multiple

            else if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Fire_Type == WeaponFireType.SINGLE) {
                if (Input.GetMouseButtonDown(0) && !sprint_Crouch_Script.is_Sprinting && weapon_Manager.GetCurrentSelectedWeapon().current_Ammo > 0) {
                        weapon_Manager.GetCurrentSelectedWeapon().Play_ShootAnimation();
                        BulletFired();
                    }

                } // shooting with single


            } // shooting with a gun

            else if (weapon_Manager.GetCurrentSelectedWeapon().weapon_Bullet_Type == WeaponBulletType.NONE) {
                if (Input.GetMouseButtonDown(0)) {
                    weapon_Manager.GetCurrentSelectedWeapon().Play_AttackAnimation();
                    // Debug.Log("we can attack with the Axe or Knife");
                } 

            } // attacking with a knife
    }

    void BulletFired() {
        // raycasts a invisible beam from the given position and gets the info of the gameobject which the raycast collides with
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayHit, weapon_Manager.GetCurrentSelectedWeapon().range, layer)) {
            // Debug.Log("We hitt the" + rayHit.collider.name);
            rayHit.transform.GetComponent<EnemyHealth>().ApplyDamage(weapon_Manager.GetCurrentSelectedWeapon().damage);
        }

        // decrease ammo
        weapon_Manager.GetCurrentSelectedWeapon().current_Ammo --;
        // turn on muzzle flash and play shoot sound
        weapon_Manager.GetCurrentSelectedWeapon().Turn_MuzzleFlash_On();
        weapon_Manager.GetCurrentSelectedWeapon().Play_ShootSound();
    }

    IEnumerator Reload() {
        // if the current weapon's ammo is equal or less than 0
        if (weapon_Manager.GetCurrentSelectedWeapon().current_Ammo <= 0) {
            // THEN play the reload animation
            is_Reloading = true;
            weapon_Manager.GetCurrentSelectedWeapon().Play_ReloadAnimation(true);
            // wait for 2 seconds then give the current ammo the value of the max ammo again
            yield return new WaitForSeconds(2);
            weapon_Manager.GetCurrentSelectedWeapon().current_Ammo = weapon_Manager.GetCurrentSelectedWeapon().max_Ammo;
            
            Debug.Log("We are reloading");
            // THEN stop player reload animation
            is_Reloading = false;
            weapon_Manager.GetCurrentSelectedWeapon().Play_ReloadAnimation(false);

        } 
        // else if the current ammo is less than the max ammo and R is being pressed then do the same as above
        else if (weapon_Manager.GetCurrentSelectedWeapon().current_Ammo < weapon_Manager.GetCurrentSelectedWeapon().max_Ammo && Input.GetKey(KeyCode.R)) {
            weapon_Manager.GetCurrentSelectedWeapon().Play_ReloadAnimation(true);

            yield return new WaitForSeconds(2);
            weapon_Manager.GetCurrentSelectedWeapon().current_Ammo = weapon_Manager.GetCurrentSelectedWeapon().max_Ammo;
            
            Debug.Log("We are reloading while pressing R");
            weapon_Manager.GetCurrentSelectedWeapon().Play_ReloadAnimation(false);


        }
        
    }
   
} // class