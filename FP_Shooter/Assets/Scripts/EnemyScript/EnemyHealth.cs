using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public int current_Health;
    [SerializeField]
    private int max_Health;

    private EnemyHandler zombie_Anim;
    private EnemyController controller_Script;

    void Awake() {
        zombie_Anim = GetComponent<EnemyHandler>();
        controller_Script = GetComponent<EnemyController>();
    }

    void Start() {
        current_Health = max_Health;
    }

    public void ApplyDamage(int damage) {
        current_Health -= damage;

        // if current health is less or equal to 0
        if (current_Health <= 0) {
            // then wait before deactivating the gameobject
            StartCoroutine(WaitBeforeDeactivateEnemy());
            
            // play dead animation
            zombie_Anim.Play_Zombie_DeadAnimation(true);

            // makes the gameobject stop moving
            controller_Script.navAgent.isStopped = true;
            controller_Script.navAgent.velocity = Vector3.zero;
            controller_Script.enabled = false;
        } 

    }

    public IEnumerator WaitBeforeDeactivateEnemy() {
        yield return new WaitForSeconds((float)1.5);

        Destroy(controller_Script.gameObject);
    }


}
