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
    private Animator assaultRifle_Cam, SMG_Cam, Crossbow_Cam, Handgun_Cam, Shotgun_Cam, snipertRifle_Cam;

    // private Animation animation;
    
    // [SerializeField]
    // private AnimationClip[] animation_Clips;

    [SerializeField]
    private AudioSource shoot_Sound, reload_Sound;

    [SerializeField]
    private GameObject attack_Point;
    public ParticleSystem muzzleflash;

    
    private PlayerMovement player_Move_Script;
    private PlayerSprintAndCrouch sprint_Crouch_Script;
    private PlayerAttack p_Attack_Script;


    public WeaponAim weapon_Aim;
    public WeaponFireType weapon_Fire_Type;
    public WeaponBulletType weapon_Bullet_Type;


    private int walk_Condition = 10, run_Condition = 20, crouch_Condition = 6, zero = 0;
    private int crouch_Aim_Condition = 3, walk_Aim_Condition = 5;
    private float crouch_Aim_Speed = 1.5f, walk_Aim_Speed = 3f;
    public int range, damage;
    public bool is_Reloading;
    
    // [SerializeField]
    public int current_Ammo, max_Ammo;
    public float reloadTime;

    void Awake() {
        anim = GetComponent<Animator>();
        shoot_Sound = GetComponent<AudioSource>();
        // muzzleflash = GetComponent<ParticleSystem>();

        player_Move_Script = GetComponentInParent<PlayerMovement>();
        sprint_Crouch_Script = GetComponentInParent<PlayerSprintAndCrouch>();
        p_Attack_Script = GetComponentInParent<PlayerAttack>();

        assaultRifle_Cam = GameObject.Find("Assault Rifle Camera").GetComponent<Animator>();
        SMG_Cam = GameObject.Find("SMG Camera").GetComponent<Animator>();
        Crossbow_Cam = GameObject.Find("Crossbow Camera").GetComponent<Animator>();
        Handgun_Cam = GameObject.Find("Handgun Camera").GetComponent<Animator>();
        Shotgun_Cam = GameObject.Find("Shotgun Camera").GetComponent<Animator>();
        snipertRifle_Cam = GameObject.Find("Sniper Rifle Camera").GetComponent<Animator>();
    }

    void Start() {
        current_Ammo = max_Ammo;

    }



    // ANIMATION 

    // play animation

    // shoot
    public void Play_ShootAnimation() {
        anim.SetTrigger(AnimationTags.SHOOT);
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
        anim.SetBool(AnimationTags.RELOAD, can_Reload);
    }
    
    // idle
    public void Play_IdleAnimation(bool can_Aim) {
        anim.SetBool(AnimationTags.IDLE, can_Aim);

    }
    

    // crouch 
    public void Play_CrouchAnimation() {
        anim.SetInteger(AnimationTags.SPEED, crouch_Condition);
    }
    public void Play_AimCrouch_Animation() {
        anim.SetInteger(AnimationTags.SPEED, crouch_Aim_Condition);
        player_Move_Script.speed = crouch_Aim_Speed;
    }


    // walk 

    public void Play_WalkAnimation() {
        anim.SetInteger(AnimationTags.SPEED, walk_Condition);
    }

    public void Play_AimWalk_Animation( ) {
        anim.SetInteger(AnimationTags.SPEED, walk_Aim_Condition);
        player_Move_Script.speed = walk_Aim_Speed;
    }

    // run 

    public void Play_RunAnimation() {
        anim.SetInteger(AnimationTags.SPEED, run_Condition);
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

    public void Play_ShootSound() {
        shoot_Sound.Play();
    }
    
    void Play_ReloadSound() {
        reload_Sound.Play();
    }

    // sound - end



    // gameobjects
    
    public void Turn_MuzzleFlash_On() {
        muzzleflash.Play();
    }
    
    void TurnOn_AttackPoint() {
        attack_Point.SetActive(true);
    }
    
    void TurnOff_AttackPoint() {
        attack_Point.SetActive(false);
    }

    // gameobjects - end

} // class