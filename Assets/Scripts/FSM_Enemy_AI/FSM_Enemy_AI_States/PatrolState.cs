﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : StateMachineBehaviour
{
    EnemyNavController m_enemyNavController;
    EnemyAI enemyAI;
    NavMeshAgent agent;
    int destinationIndex;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_enemyNavController = animator.GetComponent<EnemyNavController>();
        enemyAI = animator.GetComponent<EnemyAI>();
        agent = animator.GetComponent<NavMeshAgent>();
        m_enemyNavController.Counter = 0;
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
        agent.destination = m_enemyNavController.PathTargets[destinationIndex].position;
        if (agent.remainingDistance != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) UpdateDestination();
    }

    private void UpdateDestination()
    {
        destinationIndex++;
        if (destinationIndex >= m_enemyNavController.PathTargets.Count)
        {
            destinationIndex = 0;
        }
        agent.SetDestination(agent.destination = m_enemyNavController.PathTargets[destinationIndex].position);
    }

    private void CheckThePlayer()
    {
        if (m_enemyNavController.VisibleTarget)
        {
            enemyAI.PatrolStateDetectAPlayer?.Invoke();
        }
        if (m_enemyNavController.NoiseTarget)
        {
            if (enemyAI.currentNoiseType == NoiseController.NoiseType.Walk)
            {
                enemyAI.EmenyHeardWalk?.Invoke();
            }
            else if (enemyAI.currentNoiseType == NoiseController.NoiseType.Run)
            {
                enemyAI.EmenyHeardRun?.Invoke();
            }
            else if (enemyAI.currentNoiseType == NoiseController.NoiseType.Object)
            {
                enemyAI.EmenyAloneHeardObj?.Invoke();
            }
        }
    }
}
