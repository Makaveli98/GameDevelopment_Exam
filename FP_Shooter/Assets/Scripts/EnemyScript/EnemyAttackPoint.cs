using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackPoint : MonoBehaviour {
    private int range = 1;
    [SerializeField]
    private LayerMask player_LayerMask;
    private Collider[] p_Hit;
    public int damage;

    void Update() {
        Z_AttackPoint();
    }

    void Z_AttackPoint() {
        p_Hit = (Physics.OverlapSphere(transform.position, range,  player_LayerMask));
        if (p_Hit.Length > 0) {
            p_Hit[0].gameObject.GetComponent<PlayerHP>().Z_ApplyDamage(damage);
        }
    }
}
