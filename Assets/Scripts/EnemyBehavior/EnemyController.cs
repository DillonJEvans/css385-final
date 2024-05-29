using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public string playerTag = "Player";


    private NavMeshAgent navMeshAgent = null;
    private GameObject player = null;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(playerTag);
        // Stop the agent from rotating to face the player,
        // which would cause them to not be visible to the camera.
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        if (player)
        {
            navMeshAgent.destination = player.transform.position;
        }
    }
}
