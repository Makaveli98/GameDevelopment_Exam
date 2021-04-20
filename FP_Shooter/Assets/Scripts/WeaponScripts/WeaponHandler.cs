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

    private Animator zoom_Camera;

    // private Animation animation;
    
    // [SerializeField]
    // private AnimationClip[] animation_Clips;

    [SerializeField]
    private AudioSource shoot_Sound, reload_Sound;

    [SerializeField]
    private GameObject muzzle_Flash, attack_Point;

    public WeaponAim weapon_Aim;
    public WeaponFireType weapon_Fire_Type;
    public WeaponBulletType weapon_Bullet_Type;

    private int walk_Speed = 10;
    private int run_Speed = 20;
    private int crouch_Speed = 6;
    private int no_Speed = 0;


    void Awake() {
        anim = GetComponent<Animator>();
        shoot_Sound = GetComponent<AudioSource>();
        zoom_Camera = GameObject.Find("Assault Rifle Camera").GetComponent<Animator>();
    }

    // Animations 

    public void Play_ShootAnimation() {
        anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }

    public void Play_AttackAnimation() {
        anim.SetTrigger(AnimationTags.ATTACK);
    }

    public void PLay_Zoom_InAnimation() {
        zoom_Camera.Play(AnimationTags.ZOOM_IN);
    }
    
    public void PLay_Zoom_OutAnimation() {
        zoom_Camera.Play(AnimationTags.ZOOM_OUT);
    }

    public void Play_RandomAttackAnimation() {
        animation.clip = animation_Clips[Random.Range(0, animation_Clips.Length)];
        anim.SetTrigger(AnimationTags.ATTACK);
    }

    public void Play_ReloadAnimation(bool can_Reload) {
        anim.SetBool(AnimationTags.RELOAD_BOOL, can_Reload);
    }

    public void Play_CrouchAnimation() {
        anim.SetInteger(AnimationTags.SPEED, crouch_Speed);
    }

    public void Stop_CrouchAnimation() {
        anim.SetInteger(AnimationTags.SPEED, no_Speed);
    }

    public void Play_WalkAnimation() {
        anim.SetInteger(AnimationTags.SPEED, walk_Speed);
    }

    public void Stop_WalkAnimation() {
        anim.SetInteger(AnimationTags.SPEED, no_Speed);
    }

    public void Play_RunAnimation() {
        anim.SetInteger(AnimationTags.SPEED, run_Speed);
    }

    public void Stop_RunAnimation() {
        anim.SetInteger(AnimationTags.SPEED, no_Speed);
    }

    public void Play_IdleAnimation(bool can_Aim) {
        anim.SetBool(AnimationTags.IDLE, can_Aim);

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