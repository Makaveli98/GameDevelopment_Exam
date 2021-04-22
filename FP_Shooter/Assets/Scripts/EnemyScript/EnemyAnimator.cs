using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {

    private Animator z_Anim;

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
    
    
    // stop animation
    public void Stop_Zombie_WalkAnimation() {
        z_Anim.SetInteger(AnimationTags.ZOMBIE_SPEED, zero_Speed);
    }

    public void Stop_Zombie_RunAnimation() {
        z_Anim.SetInteger(AnimationTags.ZOMBIE_SPEED, zero_Speed);
    }

}
