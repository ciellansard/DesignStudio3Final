using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    

    public NavMeshAgent agent;
    public GameObject[] players;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    AttackControl attackScript;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    GameObject closestPlayer;

    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        attackScript = GetComponent<AttackControl>();
    }

    private void Update()
    {
        closestPlayer = GetNearestPlayer();

        //just checks if a player is in range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInAttackRange && !playerInSightRange) Patrol();
        if (!playerInAttackRange && playerInSightRange) ChasePlayer(closestPlayer.transform);
        if (playerInAttackRange && playerInSightRange) AttackPlayer(closestPlayer.transform);
    }

    private GameObject GetNearestPlayer()
    {
        GameObject closest = null;
        float smallestDistance = 999999999;
        foreach (var player in players)
        {
            float distance = (transform.position - player.transform.position).magnitude;
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                closest = player;
            }
        }
        return closest;
    }

    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();
        else agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 0.1f) walkPointSet = false;

    }
    private void SearchWalkPoint()
    {
        //creates a random point within the walkPointRange
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //there may be a better way to do this than the way the tutorial showed me
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
        
    }
    private void ChasePlayer(Transform player)
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer(Transform player)
    {
        //keeps the enemy in place while attacking
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        //I have to add in a delay to the attacks
        attackScript.Attack();
    }
}
