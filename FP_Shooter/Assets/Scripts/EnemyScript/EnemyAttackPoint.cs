using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackPoint : MonoBehaviour {
    [SerializeField]
    private LayerMask player_LayerMask;
    private Collider[] p_Hit;
    public int damage;
    private int range = 1;

    void Update() {
        Z_AttackPoint();
    }

    void Z_AttackPoint() {
        // checks if the sphere is overlapping within the given values
        p_Hit = (Physics.OverlapSphere(transform.position, range,  player_LayerMask));
        // if the overlapping gameobjects are greater than 0
        if (p_Hit.Length > 0) {
            // Then apply damage to the gameobject at the given index in the array
            p_Hit[0].gameObject.GetComponent<PlayerHP>().Z_ApplyDamage(damage);
        }
    }
}
