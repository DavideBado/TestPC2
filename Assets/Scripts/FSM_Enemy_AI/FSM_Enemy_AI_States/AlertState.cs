using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlertState : StateMachineBehaviour
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

        //m_enemyNavController.GetComponent<MeshRenderer>().material = m_enemyNavController.graphicsController.AlertMat;
        m_enemyNavController.graphicsController.AlertAnimGObj.SetActive(true);

        agent.isStopped = true;       
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_enemyNavController.VisibleTarget)
        {           
            m_enemyNavController.Counter += m_enemyNavController.ModCounters[m_enemyNavController.visibleTargetArea] * Time.deltaTime;
            m_enemyNavController.transform.LookAt(m_enemyNavController.VisibleTarget.transform.position);
            if (m_enemyNavController.Counter >= m_enemyNavController.Counter_Alert_MaxValue) enemyAI.AlertStateMaxCounter?.Invoke();
        }
        //else
        //{
        //    if(m_enemyNavController.NoiseTarget)
        //    {
        //        agent.destination = m_enemyNavController.NoiseTarget.position;
        //        if (m_enemyNavController.Counter >= m_enemyNavController.Counter_Alert_MaxValue) enemyAI.AlertStateMaxCounter?.Invoke();
        //    }
        //    else if (agent.pathStatus == NavMeshPathStatus.PathComplete) enemyAI.AlertStateMissThePlayer?.Invoke();
        //}
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.isStopped = false;
        m_enemyNavController.graphicsController.AlertAnimGObj.SetActive(false);
        //m_enemyNavController.GetComponent<MeshRenderer>().material = m_enemyNavController.graphicsController.PatrolMat;
    }
}
