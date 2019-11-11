using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : StateMachineBehaviour
{
    EnemyNavController enemyNavController;
    EnemyAI enemyAI;
    NavMeshAgent agent;
    int destinationIndex;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyNavController = animator.GetComponent<EnemyNavController>();
        enemyAI = animator.GetComponent<EnemyAI>();
        agent = animator.GetComponent<NavMeshAgent>();
        enemyNavController.Counter = 0;
        //agent.speed = enemyNavController.WalkSpeed;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CheckThePlayer();
        Move();
    }


    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private void Move()
    {
        agent.destination = enemyNavController.PathTargets[destinationIndex].position;
        if (agent.remainingDistance != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) UpdateDestination();
    }

    private void UpdateDestination()
    {
        destinationIndex++;
        if (destinationIndex >= enemyNavController.PathTargets.Count)
        {
            destinationIndex = 0;
        }
        agent.SetDestination(agent.destination = enemyNavController.PathTargets[destinationIndex].position);
    }

    private void CheckThePlayer()
    {
        if (enemyNavController.VisibleTarget)
        {
            enemyAI.PatrolStateDetectAPlayer?.Invoke();
        }
    }
}
