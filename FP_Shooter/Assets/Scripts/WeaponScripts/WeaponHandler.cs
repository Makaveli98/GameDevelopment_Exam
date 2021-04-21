using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim {
    AIM,
    NONE
}

public enum WeaponFireType {
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType {
    NONE,
    BULLET
}

public class WeaponHandler : MonoBehaviour {
    
    private Animator anim;

    private Animator assaultRifle_Cam;
    private Animator SMG_Cam;
    private Animator Crossbow_Cam;
    private Animator Handgun_Cam;
    private Animator Shotgun_Cam;
    private Animator snipertRifle_Cam;

    // private Animation animation;
    
    // [SerializeField]
    // private AnimationClip[] animation_Clips;

    [SerializeField]
    private AudioSource shoot_Sound, reload_Sound;

    [SerializeField]
    private GameObject muzzle_Flash, attack_Point;

    private PlayerMovement player_Move_Script;
    private PlayerSprintAndCrouch sprint_Crouch_Script;

    public WeaponAim weapon_Aim;
    public WeaponFireType weapon_Fire_Type;
    public WeaponBulletType weapon_Bullet_Type;

    private int walk_Speed = 10;
    private int run_Speed = 20;
    private int crouch_Speed = 6;
    private int zero = 0;
    private int walk_Aim = 5;
    private int crouch_Aim = 3;

    private float crouch_Aim_Speed = 3f;
    private float walk_Aim_Speed = 5f;


    void Awake() {
        anim = GetComponent<Animator>();
        shoot_Sound = GetComponent<AudioSource>();

        player_Move_Script = GetComponentInParent<PlayerMovement>();
        sprint_Crouch_Script = GetComponentInParent<PlayerSprintAndCrouch>();

        assaultRifle_Cam = GameObject.Find("Assault Rifle Camera").GetComponent<Animator>();
        SMG_Cam = GameObject.Find("SMG Camera").GetComponent<Animator>();
        Crossbow_Cam = GameObject.Find("Crossbow Camera").GetComponent<Animator>();
        Handgun_Cam = GameObject.Find("Handgun Camera").GetComponent<Animator>();
        Shotgun_Cam = GameObject.Find("Shotgun Camera").GetComponent<Animator>();
        snipertRifle_Cam = GameObject.Find("Sniper Rifle Camera").GetComponent<Animator>();

    }

    // ANIMATION 

    // play animation


    // shoot
    public void Play_ShootAnimation() {
        anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }

    // attack
    public void Play_AttackAnimation() {
        anim.SetTrigger(AnimationTags.ATTACK);
    }
    
    // zoom in
    public void PLay_Zoom_InAnimation() {
        assaultRifle_Cam.Play(AnimationTags.ZOOM_IN);
        SMG_Cam.Play(AnimationTags.ZOOM_IN);
        Crossbow_Cam.Play(AnimationTags.ZOOM_IN);
        Handgun_Cam.Play(AnimationTags.ZOOM_IN);
        Shotgun_Cam.Play(AnimationTags.ZOOM_IN);
        snipertRifle_Cam.Play(AnimationTags.ZOOM_IN);
    }

    // zoom out
    public void PLay_Zoom_OutAnimation() {
        assaultRifle_Cam.Play(AnimationTags.ZOOM_OUT);
        SMG_Cam.Play(AnimationTags.ZOOM_OUT);
        Crossbow_Cam.Play(AnimationTags.ZOOM_OUT);
        Handgun_Cam.Play(AnimationTags.ZOOM_OUT);
        Shotgun_Cam.Play(AnimationTags.ZOOM_OUT);
        snipertRifle_Cam.Play(AnimationTags.ZOOM_OUT);
    }

    // public void Play_RandomAttackAnimation() {
    //     GetComponent<Animation>().clip = animation_Clips[Random.Range(0, animation_Clips.Length)];
    //     anim.SetTrigger(AnimationTags.ATTACK);
    // }

    // reload
    public void Play_ReloadAnimation(bool can_Reload) {
        anim.SetBool(AnimationTags.RELOAD_BOOL, can_Reload);
    }
    
    // idle
    public void Play_IdleAnimation(bool can_Aim) {
        anim.SetBool(AnimationTags.IDLE, can_Aim);

    }
    

    // crouch 
    public void Play_CrouchAnimation() {
        anim.SetInteger(AnimationTags.SPEED, crouch_Speed);
    }
    public void Play_AimCrouch_Animation() {
        anim.SetInteger(AnimationTags.SPEED, crouch_Aim);
        player_Move_Script.speed = crouch_Aim_Speed;
    }


    // walk 

    public void Play_WalkAnimation() {
        anim.SetInteger(AnimationTags.SPEED, walk_Speed);
    }

    public void Play_AimWalk_Animation( ) {
        anim.SetInteger(AnimationTags.SPEED, walk_Aim);
        player_Move_Script.speed = walk_Aim_Speed;
    }

    // run 

    public void Play_RunAnimation() {
        anim.SetInteger(AnimationTags.SPEED, run_Speed);
    }


    // stop animation

    // crouch
    public void Stop_CrouchAnimation() {
        anim.SetInteger(AnimationTags.SPEED, zero);
    }

    public void Stop_AimCrouch_Animation() {
        anim.SetInteger(AnimationTags.SPEED, zero);
        player_Move_Script.speed = sprint_Crouch_Script.crouch_Speed;
    }


    // walk
    public void Stop_WalkAnimation() {
        anim.SetInteger(AnimationTags.SPEED, zero);
    }

    public void Stop_AimWalk_Animation() {
        anim.SetInteger(AnimationTags.SPEED, zero);
        player_Move_Script.speed = sprint_Crouch_Script.move_Speed;
    }
    

    // run
    public void Stop_RunAnimation() {
        anim.SetInteger(AnimationTags.SPEED, zero);
    }

    // Animations - end



    // Sound 

    void Play_ShootSound() {
        shoot_Sound.Play();
    }
    
    void Play_ReloadSound() {
        reload_Sound.Play();
    }

    // sound - end



    // gameobjects
    
    void Turn_MuzzleFlash_On() {
        muzzle_Flash.SetActive(true);
    }

    void Turn_MuzzleFlash_Off() {
        muzzle_Flash.SetActive(false);
    }
    
    void TurnOn_AttackPoint() {
        attack_Point.SetActive(true);
    }
    
    void TurnOff_AttackPoint() {
        attack_Point.SetActive(false);
    }

    // gameobjects - end

} // class