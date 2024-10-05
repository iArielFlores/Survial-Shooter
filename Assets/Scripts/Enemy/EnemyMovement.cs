using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private PlayerHealth playerHealth;
    private Transform playerTransform;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        enemyHealth = GetComponent<EnemyHealth>();
    }


    void Update ()
    {
        

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            navMeshAgent.SetDestination(playerTransform.position);
        }
        else
        {
            navMeshAgent.enabled = false;
        }
    }
}
