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

    [SerializeField]
    private Animation animation;
    
    [SerializeField]
    private AnimationClip[] animation_Clips;

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


    void Awake() {
        anim = GetComponent<Animator>();
        shoot_Sound = GetComponent<AudioSource>();
    }

    // Animations 

    void Play_ShootAnimation() {
        anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }

    void Play_AttackAnimation() {
        anim.SetTrigger(AnimationTags.ATTACK);
    }

    void Play_RandomAttackAnimation() {
        animation.clip = animation_Clips[Random.Range(0, animation_Clips.Length)];
        anim.SetTrigger(AnimationTags.ATTACK);
    }

    void Play_ReloadAnimation(bool can_Reload) {
        anim.SetBool(AnimationTags.RELOAD_BOOL, can_Reload);
    }

    void Play_CrouchAnimation() {
        anim.SetInteger(AnimationTags.SPEED, crouch_Speed);
    }

    void Play_WalkAnimation() {
        anim.SetInteger(AnimationTags.SPEED, walk_Speed);
    }

    void Play_RunAnimation() {
        anim.SetInteger(AnimationTags.SPEED, run_Speed);
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