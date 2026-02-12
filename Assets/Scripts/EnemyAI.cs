using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public float timeBetweenAttacks = 1f;
    bool alreadyAttacked;
    AttackControl attackScript;
    CharacterHealth healthScript;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    GameObject closestPlayer;
    Rigidbody rb;

    [SerializeField]
    private float deathDelay = 5f;
    private bool isDead = false;

    [SerializeField]
    private GameObject head;
    [SerializeField]
    private GameObject body;
    [SerializeField]
    private GameObject hand1;
    [SerializeField]
    private GameObject hand2;
    [SerializeField]
    private GameObject leg1;
    [SerializeField]
    private GameObject leg2;
    [SerializeField]
    private GameObject shoulder1;
    [SerializeField]
    private GameObject shoulder2;
    [SerializeField]
    private GameObject weapon;
    private List<GameObject> bodyParts = new List<GameObject>();

    private void Awake()
    {
        UpdatePlayerList();
        agent = GetComponent<NavMeshAgent>();
        attackScript = GetComponent<AttackControl>();
        healthScript = GetComponent<CharacterHealth>();
        rb = GetComponent<Rigidbody>();

        // Add all existing body parts to a list
        if (head != null)       bodyParts.Add(head);
        if (body != null)       bodyParts.Add(body);
        if (hand1 != null)      bodyParts.Add(hand1);
        if (hand2 != null)      bodyParts.Add(hand2);
        if (leg1 != null)       bodyParts.Add(leg1);
        if (leg2 != null)       bodyParts.Add(leg2);
        if (shoulder1 != null)  bodyParts.Add(shoulder1);
        if (shoulder2 != null)  bodyParts.Add(shoulder2);

        // Could we just have a public List<GameObject> bodyParts? hmm
    }

    private void Update()
    {
        if (isDead) return;

        if (healthScript.currentHealth <= 0)
        {
            isDead = true;
            Die();
        }

        WalkPointUpdateY();
        if (players.Length > 0) {
            closestPlayer = GetNearestPlayer();

            //just checks if a player is in range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInAttackRange && !playerInSightRange) Patrol();
            if (!playerInAttackRange && playerInSightRange) ChasePlayer(closestPlayer.transform);
            if (playerInAttackRange && playerInSightRange) AttackPlayer(closestPlayer.transform);
        }
        else Patrol();

        // Only let the enemy spin around the vertical axis
        transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
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
        //Debug.Log(closest);
        return closest;
    }

    private void Patrol()
    {
       // Debug.Log("patrolling");
        if (!walkPointSet) SearchWalkPoint();
        else agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 0.25f) walkPointSet = false;

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
    private void WalkPointUpdateY()
    {
        walkPoint.y = transform.position.y;
    }
    private void ChasePlayer(Transform player)
    {
       // Debug.Log("Chasing Player");
        agent.SetDestination(player.position);
    }
    private void AttackPlayer(Transform player)
    {
        //Debug.Log("Attacking Player");
        //keeps the enemy in place while attacking
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (alreadyAttacked == false)
        {
            attackScript.Attack();
            alreadyAttacked = true;
            StartCoroutine(AttackDelay());
        }
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        alreadyAttacked = false;
    }

    private void tumbleBodyPart(GameObject part)
    {
        part.transform.SetParent(null, true);
        Rigidbody partRb = part.GetComponent<Rigidbody>();
        partRb.isKinematic = false;
        partRb.useGravity = true;
        part.GetComponent<Collider>().enabled = true;
        Debug.Log(part.name);
    }

    private void Die()
    {
        //isDead = true;

        //this is disgusting, i'm so sorry

        rb.constraints = RigidbodyConstraints.None;
        rb.isKinematic = false;

        agent.enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        for (int i = 0; i < bodyParts.Count; i++) tumbleBodyPart(bodyParts[i]);

        gameObject.GetComponent<Collider>().enabled = false;

        rb.AddForce(Vector3.back * 1.5f);
        StartCoroutine(DeathDelay());
    }

    private IEnumerator DeathDelay()
    {
        Debug.Log("waiting to destroy objects");
        yield return new WaitForSeconds(deathDelay);
        Destroy(gameObject);
        for (int i = 0; i < bodyParts.Count; i++) Destroy(bodyParts[i]);
    }

    public void UpdatePlayerList()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
}
