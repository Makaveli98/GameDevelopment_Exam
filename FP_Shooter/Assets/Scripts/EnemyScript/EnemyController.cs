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
    [HideInInspector]
    public NavMeshAgent navAgent;
    private EnemyState enemy_State;
    private EnemyAnimator z_Animator;
    private Transform target;

    private float patrol_Speed = 0.5f, run_Speed = 4f;

    // private float chase_Distance = 500f, current_Chase_Distance;
    
    private float attack_Timer, wait_Before_Attack = 0.7f;

    private float attack_Distance = 1.8f, AfterAttack_Distance = 2.2f;
    
    // private float min_Patrol_Radius = 20f, max_Patrol_Radius = 60f;

    // private float patrol_Timer, patrol_For_This_Time = 15f;

    void Awake() {
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER).transform;
        z_Animator = GetComponent<EnemyAnimator>();
    }
    

    void Start() {
        // puts the enemy in patrol mode
        // enemy_State = EnemyState.PATROL;

        enemy_State = EnemyState.CHASE;

        // current_Chase_Distance = chase_Distance;

        // patrol_Timer = patrol_For_This_Time;

        attack_Timer = wait_Before_Attack;

    }

    void Update() {
        // if (enemy_State == EnemyState.PATROL) {
        //     Patrol();
        // }

        if (enemy_State == EnemyState.CHASE) {
            Chase();
        }

        if (enemy_State == EnemyState.ATTACK) {
            Attack();
        }

    }

    // void Patrol() {
    //     // tells the agent is able to move
    //     navAgent.isStopped = false;
    //     navAgent.speed = patrol_Speed;

    //     patrol_Timer += Time.deltaTime;
        
    //     // only if the patrol timer is greater than patrol for this time
    //     // execute the code
    //     if (patrol_Timer > patrol_For_This_Time) {
    //         SetNewRandomDestination();
            
    //         patrol_Timer = 0f;
    //     }

    //     // play walk animation
    //     if (navAgent.velocity.sqrMagnitude > 0) {
    //         z_Animator.Play_Zombie_WalkAnimation();   
    //     } else {
    //         z_Animator.Stop_Zombie_WalkAnimation();
    //     }
        
    //     // checks the distance between the enemy and player AND if its equel or lesser than the chase distance
    //     if (Vector3.Distance(transform.position, target.position) <= chase_Distance) {
    //         // stop walk animation

    //         // puts the enemy on chase mode
    //         enemy_State = EnemyState.CHASE;

    //         // play spotted audio sound
    //     }

    // } // Patrol

    void Chase() {
        // tells the agent is able to move
        navAgent.isStopped = false;
        navAgent.speed = run_Speed;

        // sets the destination to where the agent is suppose to go
        navAgent.SetDestination(target.position);

        // play run animation
        if (navAgent.velocity.sqrMagnitude > 0) {
            z_Animator.Play_Zombie_RunAnimation();   
        } else {
            z_Animator.Stop_Zombie_RunAnimation();
        }

        // checks if the distance between the enemy and the target is equel or less than attack_Distance
        // IF less than Attack Distance then enemy go in attack state
        if (Vector3.Distance(transform.position, target.position) <= attack_Distance) {
            // stop walk and run animation
            z_Animator.Stop_Zombie_RunAnimation();
            z_Animator.Stop_Zombie_WalkAnimation();

            // puts the enemy in attack state
            enemy_State = EnemyState.ATTACK;
            
            // resets the chase distance
            // if (chase_Distance != current_Chase_Distance) {
            //     chase_Distance = current_Chase_Distance;
            // }

          // ELSE IF greater than Chase Distance
          // THEN put the enemy back to patrol mode otherwise the enemy can chase you forever
        } 
        // else if (Vector3.Distance(transform.position, target.position) > chase_Distance) {
        //     // stop run animation
        //     z_Animator.Stop_Zombie_RunAnimation();

        //     // THEN reset to patrol mode
        //     enemy_State = EnemyState.PATROL;

        //     // reset the patrol timer to patrol for this time again
        //     // so the enemy will got straight to patrolling 
        //     patrol_Timer = patrol_For_This_Time;

        //     // resets the chase distance because enemy is now patrolling
        //     if (chase_Distance != current_Chase_Distance) {
        //         chase_Distance = current_Chase_Distance;
        //     } 

        // } // checks if greater than chase distance

    } // Chase

    void Attack() {
        // tells the agent is not able to move
        navAgent.isStopped = true;
        // puts the speed on 0
        navAgent.velocity = Vector3.zero;

        attack_Timer += Time.deltaTime;

        // only if attack timer is greater than wait before attack
        // execute the code
        if (attack_Timer > wait_Before_Attack) {
            // play attack animation
            z_Animator.Play_Zombie_AttackAnimation();

            // resets the attack_Timer otherwise the attack animation will loop
            attack_Timer = 0f;

            //play attack audio

            // if the distane between the enemy and player is greater than the attack distance plus the chase distance after attacking
            // THEN put the enemy back to chase distance otherwise the enemy will only attack in 1 place 
            // NOW you actually tell the enemy to move each time after attack if the player is to far away for the attack distance
            if (Vector3.Distance(transform.position, target.position) > attack_Distance + AfterAttack_Distance) {
                enemy_State = EnemyState.CHASE;
            }
        }
    } // Attack

    // void SetNewRandomDestination() {
    //     // generate a random direction
    //     float random_Radius = Random.Range(min_Patrol_Radius, max_Patrol_Radius);

    //     // insideUnitSphere = returns a random point inside a sphere with radius 1
    //     Vector3 random_Dir = Random.insideUnitSphere * random_Radius;
        
    //     // add the random direction with the position
    //     random_Dir += transform.position;

    //     NavMeshHit navHit;

    //     // SamplePosition = gets the random_Dir and compare with the nav area. 
    //     // If the next random_Dir is outside the nav area 
    //     // THEN it will calculate a new position within the nav area 
    //     // AND it will store the data in navHit.
    //     // the value after random_Radius is the area mask. The SamplePosition will check the position on the giving layer (area mask)
    //     NavMesh.SamplePosition(random_Dir, out navHit, random_Radius, -1);

    //     navAgent.SetDestination(navHit.position);


    // } // Random Destination

} // class
