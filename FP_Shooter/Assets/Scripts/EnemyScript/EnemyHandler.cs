using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour {

    private Animator z_Anim;
    
    [SerializeField]
    private GameObject z_Attack_Point_R, z_Attack_Point_L;

    private int z_Walk_Speed = 1;
    private int z_Run_Speed = 2;
    private int zero_Speed = 0;

    void Awake() {
        z_Anim = GetComponent<Animator>();
    }
    
    // play animation
    public void Play_Zombie_WalkAnimation() {
        z_Anim.SetInteger(AnimationTags.ZOMBIE_SPEED, z_Walk_Speed);
    }

    public void Play_Zombie_RunAnimation() {
        z_Anim.SetInteger(AnimationTags.ZOMBIE_SPEED, z_Run_Speed);
    }

    public void Play_Zombie_AttackAnimation() {
        z_Anim.SetTrigger(AnimationTags.ZOMBIE_ATTACK);
    }

    public void Play_Zombie_DeadAnimation(bool is_Dead) {
        z_Anim.SetBool(AnimationTags.ZOMBIE_DEAD, is_Dead);
    }
    
    // public void Play_Zombie_DeadAnimation2(bool is_Dead2) {
    //     z_Anim.SetBool(AnimationTags.ZOMBIE_DEAD_2, is_Dead2);
    // }
    
    
    // stop animation
    public void Stop_Zombie_WalkAnimation() {
        z_Anim.SetInteger(AnimationTags.ZOMBIE_SPEED, zero_Speed);
    }

    public void Stop_Zombie_RunAnimation() {
        z_Anim.SetInteger(AnimationTags.ZOMBIE_SPEED, zero_Speed);
    }

    // gameobject
    public void TurnOn_Z_AttackPoint_R() {
        z_Attack_Point_R.SetActive(true);
    }

    public void TurnOff_Z_AttackPoint_R() {
        z_Attack_Point_R.SetActive(false);
    }

    public void TurnOn_Z_AttackPoint_L() {
        z_Attack_Point_L.SetActive(true);
    }

    public void TurnOff_Z_AttackPoint_L() {
        z_Attack_Point_L.SetActive(false);
    }

}
