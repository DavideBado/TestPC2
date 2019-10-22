using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavController : MonoBehaviour
{
    public List<Transform> Targets = new List<Transform>();
    public NavMeshAgent agent;
    int destinationIndex = 0;
    private void Start()
    {
        agent.destination = Targets[destinationIndex].position;
    }

    private void Update()
    {
        float dist = agent.remainingDistance;
        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            destinationIndex++;
            if (destinationIndex >= Targets.Count)
            {
                destinationIndex = 0;
            }
            agent.destination = Targets[destinationIndex].position;
        }
    }
}
