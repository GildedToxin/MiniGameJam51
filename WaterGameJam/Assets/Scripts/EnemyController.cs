using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [Header("References")]
    public Transform player;             
    private NavMeshAgent agent;

    [Header("Settings")]
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

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance)
        {
            AttackPlayer();
            agent.isStopped = true;
        }

        else if (distanceToPlayer <= chaseDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            if(targetLocation.HasValue)
            {
                targetLocation = null;
            }
        }

        else if (targetLocation.HasValue)
        {
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
            agent.isStopped = true;
        }
    }


    public void GoToLocation(Vector3 location)
    {
        targetLocation = location;
    }
    [ContextMenu("Go To Location 2")]
    public void GoToLocation2()
    {
        targetLocation = new Vector3(0,0,0);
    }

    private void AttackPlayer()
    {
        Debug.Log("Attacking player!");
    }
}