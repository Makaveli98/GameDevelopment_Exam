using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField]
    private int current_Health;
    [SerializeField]
    private int max_Health;

    private EnemyAnimator zombie_Anim;
    private EnemyController controller_Script;
    void Awake() {
        zombie_Anim = GetComponent<EnemyAnimator>();
        controller_Script = GetComponent<EnemyController>();
    }

    void Start() {
        current_Health = max_Health;
    }


    public void ApplyDamage(int damage) {
        current_Health -= damage;

        if (current_Health <= 0) {
            controller_Script.navAgent.isStopped = true;
            controller_Script.navAgent.velocity = Vector3.zero;
            zombie_Anim.Play_Zombie_DeadAnimation(true);
        } 

    }


}
