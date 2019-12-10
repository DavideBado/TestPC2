using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : StateMachineBehaviour
{
    EnemyNavController m_enemyNavController;
    EnemyAI enemyAI;
    NavMeshAgent agent;
    GameObject TriggerTrap;
    FieldOfView _FieldOfView;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_enemyNavController = animator.GetComponent<EnemyNavController>();
        enemyAI = animator.GetComponent<EnemyAI>();
        agent = animator.GetComponent<NavMeshAgent>();

        agent.speed = m_enemyNavController.WalkSpeed;
        agent.isStopped = true;
        m_enemyNavController.transform.position = m_enemyNavController.IdlePosition;

        if(!_FieldOfView)_FieldOfView = m_enemyNavController.GetComponent<FieldOfView>();
        SetupMeshFilters(_FieldOfView, false);
        _FieldOfView.enabled = false;

        if (!TriggerTrap) SetupTriggerTrap();
        else TriggerTrap.SetActive(true);
    }

    private static void SetupMeshFilters(FieldOfView _FieldOfView, bool _On)
    {
        foreach (MeshFilter _MeshFilter in _FieldOfView.viewMeshFilters)
        {
            _MeshFilter.gameObject.SetActive(_On);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TriggerTrap.SetActive(false);
        _FieldOfView.enabled = true;
        SetupMeshFilters(_FieldOfView, true);
        agent.isStopped = false;
    }

    void SetupTriggerTrap()
    {
        TriggerTrap = new GameObject();

        TriggerTrap.transform.position = m_enemyNavController.IdleTriggerTrapPosition;
        BoxCollider _TriggerTrapCollider = TriggerTrap.AddComponent<BoxCollider>() as BoxCollider;
        _TriggerTrapCollider.isTrigger = true;
        _TriggerTrapCollider.size = m_enemyNavController.IdleTriggerTrapDim;
        TriggerTrap _TriggerTrapScript = TriggerTrap.AddComponent<TriggerTrap>() as TriggerTrap;
        _TriggerTrapScript.enabled = true;
        _TriggerTrapScript.m_EnemyAI = enemyAI;
    }
}
