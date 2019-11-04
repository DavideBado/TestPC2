﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlertState : StateMachineBehaviour
{
    EnemyNavController enemyNavController;
    float counter_Alert = 0;
    EnemyAI enemyAI;
    NavMeshAgent agent;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyNavController = animator.GetComponent<EnemyNavController>();
        enemyAI = animator.GetComponent<EnemyAI>();
        agent = animator.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyNavController.visibleTargetArea > 0)
        {
            counter_Alert += enemyNavController.ModCounters[enemyNavController.visibleTargetArea] * Time.deltaTime;
        }
        else
        {
            counter_Alert = 0;
        }

        if (counter_Alert >= enemyNavController.Counter_Alert_MaxValue)
        {
            enemyAI.AlertStateMaxCounter?.Invoke();
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.isStopped = false;
    }
}
