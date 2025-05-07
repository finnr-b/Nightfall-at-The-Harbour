using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    // The AI Patrols...

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // The AI Attacks!!!

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    private EnemyGunController gunController;

    // Swapping the States

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        gunController = GetComponentInChildren<EnemyGunController>();
    }

    // Update is called once per frame
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }


    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // The AI reached the Walkpoint YIPPIEEEEEEEEE!!!!
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // This part calculates a random point in the range.
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // This part makes sure the enemy does not wander out of bounds.

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // This part makes sure the enemy doesn't move for difficulty sake

        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            if (gunController != null)
            {
                gunController.Shoot();

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}