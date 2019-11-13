using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ResearchState : StateMachineBehaviour
{
    EnemyNavController m_enemyNavController;
    EnemyAI enemyAI;
    NavMeshAgent agent;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_enemyNavController = animator.GetComponent<EnemyNavController>();
        enemyAI = animator.GetComponent<EnemyAI>();
        agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_enemyNavController.VisibleTarget)
        {
            m_enemyNavController.Counter += m_enemyNavController.ModCounters[m_enemyNavController.visibleTargetArea] * Time.deltaTime;
            agent.destination = m_enemyNavController.VisibleTarget.position;
            if (m_enemyNavController.Counter >= m_enemyNavController.Counter_Research_MaxValue) enemyAI.ResearchStateMaxCounter?.Invoke();
        }
        else
        {
            if (m_enemyNavController.NoiseTarget)
            {
                agent.destination = m_enemyNavController.NoiseTarget.position;                
            }
            else if (agent.pathStatus == NavMeshPathStatus.PathComplete) enemyAI.AlertStateMissThePlayer?.Invoke();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
