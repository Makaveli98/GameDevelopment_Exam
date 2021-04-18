using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour {

    private NavMeshAgent navMeshAgent;
    private EnemyState enemy_State;

    private Transform target;

    private float patrol_Speed = 0.5f;
    private float run_Speed = 4f;

    private float chase_Distance = 7f;
    private float current_Chase_Distance;

    private float attack_Timer;
    private float wait_Before_Attack = 1f;

    private float attack_Distance = 1.8f;
    private float chase_AfterAttack_Distance = 2f;
    
    private float min_Patrol_Radius = 20f, max_Patrol_Radius = 60f;

    private float patrol_Timer;
    private float patrol_For_This_Time = 15f;

    void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER).transform;
    }

    void Start() {
        enemy_State = EnemyState.PATROL;

        current_Chase_Distance = chase_Distance;

        patrol_Timer = patrol_For_This_Time;

        attack_Timer = wait_Before_Attack;

    }

    void Update() {

    }

    void Patrol() {
        
    }


    

}
