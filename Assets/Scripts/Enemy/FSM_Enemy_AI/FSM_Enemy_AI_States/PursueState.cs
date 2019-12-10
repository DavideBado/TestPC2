using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursueState : StateMachineBehaviour
{
    EnemyNavController m_enemyNavController;
    EnemyAI enemyAI;
    NavMeshAgent agent;
    Transform savedTarget;
    float timer;
   
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_enemyNavController = animator.GetComponent<EnemyNavController>();
        enemyAI = animator.GetComponent<EnemyAI>();
        agent = animator.GetComponent<NavMeshAgent>();
        timer = m_enemyNavController.Counter_Pursue_MaxValue;
        //m_enemyNavController.GetComponent<MeshRenderer>().material = m_enemyNavController.graphicsController.PursueMat;
        m_enemyNavController.graphicsController.PursueAnimGObj.SetActive(true);
        agent.speed = m_enemyNavController.RunSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_enemyNavController.VisibleTarget)
        {
            m_enemyNavController.OldVisibleTarget = m_enemyNavController.VisibleTarget;

            savedTarget = m_enemyNavController.VisibleTarget;
            agent.destination = m_enemyNavController.VisibleTarget.position;
           
            if (Vector3.Distance(animator.transform.position, m_enemyNavController.VisibleTarget.position) < m_enemyNavController.GameOverDist)
            {
                m_enemyNavController.graphicsController.StartAttackAnimation?.Invoke();
            }
        }
        else
        {
            CheckHiddenPlayer();
            agent.destination = savedTarget.position;
            timer -= Time.deltaTime;
            if(timer <= 0) enemyAI.PursueStateMissThePlayer?.Invoke();
        }
    }

    private void CheckHiddenPlayer()
    {
        if (m_enemyNavController.OldVisibleTarget)
        {
            m_enemyNavController.TargetPrevHidingState = m_enemyNavController.TargetCurrentHidingState;
            m_enemyNavController.TargetCurrentHidingState = m_enemyNavController.OldVisibleTarget.GetComponent<PlayerMovController>().isHiding;
            if (!m_enemyNavController.TargetPrevHidingState && m_enemyNavController.TargetCurrentHidingState)
            {
                m_enemyNavController.HiddenTarget = m_enemyNavController.OldVisibleTarget;
                enemyAI.EmenySeePlayerInHidingSpot?.Invoke();
            }
            m_enemyNavController.OldVisibleTarget = m_enemyNavController.VisibleTarget;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_enemyNavController.graphicsController.PursueAnimGObj.SetActive(false);
        //m_enemyNavController.GetComponent<MeshRenderer>().material = m_enemyNavController.graphicsController.PatrolMat;
    }
}