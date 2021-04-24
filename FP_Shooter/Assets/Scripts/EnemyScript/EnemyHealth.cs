using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public int current_Health;
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
            StartCoroutine(WaitBeforeDeactivateEnemy());

            zombie_Anim.Play_Zombie_DeadAnimation(true);

            controller_Script.navAgent.isStopped = true;
            controller_Script.navAgent.velocity = Vector3.zero;
            controller_Script.enabled = false;
        } 

    }

    IEnumerator WaitBeforeDeactivateEnemy() {
        yield return new WaitForSeconds((float)1.5);

        Destroy(controller_Script.gameObject);
    }


}
