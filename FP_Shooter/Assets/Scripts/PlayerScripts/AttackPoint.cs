using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour {

    [SerializeField]
    private LayerMask layer;
    private Collider[] hit;

    private WeaponManager weapon_Manager;

    void Awake() {
        weapon_Manager = GetComponentInParent<WeaponManager>();
    }

    void Update() {
        Attack();
    }

    void Attack() {
        hit = Physics.OverlapSphere(transform.position, weapon_Manager.GetCurrentSelectedWeapon().range, layer);
        if (hit.Length > 0) {
            Debug.Log("we hitt the" + hit[0].gameObject.name);
            hit[0].gameObject.GetComponent<EnemyHealth>().ApplyDamage(weapon_Manager.GetCurrentSelectedWeapon().damage);
        }
    }
}
