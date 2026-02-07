using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [Header("References")]
    public Transform player;             
    private NavMeshAgent agent;

    [Header("Settings")]
    public float searchDistance = 50f;    
    public float chaseDistance = 10f;    
    public float attackDistance = 1f;    
    public float moveSpeed = 3.5f;


    private Vector3? targetLocation = null;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = moveSpeed;
        agent.isStopped = true; // Start stationary
    }
    private void Start()
    {
        GameManager.Instance.RegisterEnemy(gameObject);
    }
    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance)
        {
            print("attacking player!");
            AttackPlayer();
            agent.isStopped = true;
        }

        else if (distanceToPlayer <= chaseDistance)
        {
            print("chasing player!");
            agent.isStopped = false;
            agent.SetDestination(player.position);
            if(targetLocation.HasValue)
            {
                targetLocation = null;
            }
        }

        else if (targetLocation.HasValue)
        {
            print("searching ping!");
            agent.isStopped = false;
            agent.SetDestination(targetLocation.Value);

     
            if (Vector3.Distance(transform.position, targetLocation.Value) < 0.1f)
            {
                agent.isStopped = true;
                targetLocation = null; 
            }
        }

        else
        {
            print("nothing to do!");
            agent.isStopped = true;
        }
    }


    public void GoToLocation(Vector3 location)
    {
        targetLocation = location;
    }
    private void AttackPlayer()
    {
        Debug.Log("Attacking player!");
    }

    public void ReceiveSonarPing(Vector3 location)
    {
        print("received ping at " + location);
        float distanceToPing = Vector3.Distance(transform.position, location);

        if (distanceToPing <= searchDistance)
        {
            targetLocation = location;
        }
    }
}