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

    private NavMeshAgent navAgent;
    private EnemyState enemy_State;

    private Transform target;

    private float patrol_Speed = 0.5f;
    private float run_Speed = 4f;

    private float chase_Distance = 7f;
    private float current_Chase_Distance;

    private float attack_Timer;
    private float wait_Before_Attack = 1f;

    private float attack_Distance = 1.8f;
    private float chase_AfterAttack_Distance = 2.2f;
    
    private float min_Patrol_Radius = 20f, max_Patrol_Radius = 60f;

    private float patrol_Timer;
    private float patrol_For_This_Time = 15f;

    void Awake() {
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER).transform;
    }

    void Start() {
        // puts the enemy in patrol mode
        enemy_State = EnemyState.PATROL;

        current_Chase_Distance = chase_Distance;

        patrol_Timer = patrol_For_This_Time;

        attack_Timer = wait_Before_Attack;

    }

    void Update() {
        if (enemy_State == EnemyState.PATROL) {
            Patrol();
        }

        if (enemy_State == EnemyState.CHASE) {
            Chase();
        }

        if (enemy_State == EnemyState.ATTACK) {
            Attack();
        }

    }

    void Patrol() {
        // tells the agent is able to move
        navAgent.isStopped = false;
        navAgent.speed = patrol_Speed;

        patrol_Timer += Time.deltaTime;

        if (patrol_Timer > patrol_For_This_Time) {
            SetNewRandomDestination();
            
            patrol_Timer = 0f;
        }

        // play walk animation
        
        // checks the distance between the enemy and player AND if its equel or lesser than the chase distance
        if (Vector3.Distance(transform.position, target.position) <= chase_Distance) {
            // stop walk animation

            // puts the enemy on chase mode
            enemy_State = EnemyState.CHASE;

            // play spotted audio sound
        }

    } // Patrol

    void Chase() {
        // tells the agent is able to move
        navAgent.isStopped = false;
        navAgent.speed = run_Speed;

        // sets the destination to where the agent is suppose to go
        navAgent.SetDestination(target.position);

        // play run animation

        // checks if the distance between the enemy and the target is equel or lesser than attack_Distance
        // ONLY  then will the enemy attack
        if (Vector3.Distance(transform.position, target.position) <= attack_Distance) {
            // stop walk and run animation

            // puts the enemy in attack mode
            enemy_State = EnemyState.ATTACK;
            
            // resets the chase distance
            if (chase_Distance != current_Chase_Distance) {
                chase_Distance = current_Chase_Distance;
            }
          
          // else if the distance between the enemy and the player is greater than the chase distance
          // THEN put the enemy back to patrol mode otherwise the enemy can chase you forever
        } else if (Vector3.Distance(transform.position, target.position) > chase_Distance) {
            // stop run animation

            // reset to patrol mode
            enemy_State = EnemyState.PATROL;
            // reset the patrol timer too
            patrol_Timer = patrol_For_This_Time;

            // resets the chase distance because enemy is now patrolling
            if (chase_Distance != current_Chase_Distance) {
                chase_Distance = current_Chase_Distance;
            } 

        } // checks if greater than chase distance

    } // Chase

    void Attack() {
        // tells the agent is not able to move
        navAgent.isStopped = true;
        // puts the speed on 0
        navAgent.velocity = Vector3.zero;

        attack_Timer += Time.deltaTime;

        // if the attack timer is greater than wait before attack - attack
        if (attack_Timer > wait_Before_Attack) {
            // play attack animation

            // resets the attack_Timer otherwise the enemy will attack in for infinite
            attack_Timer = 0f;

            //play attack audio

            // if the distane between the enemy and player is greater than the attack distance plus the chase distance after attacking
            // THEN put the enemy back to chase distance otherwise the enemy will only attack in 1 place 
            // NOW you actually tell the enemy to move each time after attack if the player is to far away for the attack distance
            if (Vector3.Distance(transform.position, target.position) > attack_Distance + chase_AfterAttack_Distance) {
                enemy_State = EnemyState.CHASE;
            }
        }
    } // Attack

    void SetNewRandomDestination() {
        // generate a random direction
        float random_Radius = Random.Range(min_Patrol_Radius, max_Patrol_Radius);

        // insideUnitSphere = returns a random point inside a sphere with radius 1
        Vector3 random_Dir = Random.insideUnitSphere * random_Radius;
        
        // add the random direction with the position
        random_Dir += transform.position;

        NavMeshHit navHit;

        // SamplePosition = gets the random_Dir and compare with the nav area. 
        // If the next random_Dir is outside the nav area 
        // THEN it will calculate a new position within the nav area 
        // AND it will store the data in navHit.
        // the value after random_Radius is the area mask. The SamplePosition will check the position on the giving layer (area mask)
        NavMesh.SamplePosition(random_Dir, out navHit, random_Radius, -1);

        navAgent.SetDestination(navHit.position);


    } // Random Destination


    

}
