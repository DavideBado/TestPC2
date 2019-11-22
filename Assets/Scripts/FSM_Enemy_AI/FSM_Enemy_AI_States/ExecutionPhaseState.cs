﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExecutionPhaseState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.instance.OnExePhase = true;
        GameManager.instance.UI_Manager.PhaseTxt.text = "ExecutionPhase";
        GameManager.instance.UI_Manager.PhaseTxt.gameObject.SetActive(true);
        GameManager.instance.Player.transform.position = GameManager.instance.Player.ResetPosition;
        GameManager.instance.Player.GetComponent<NavMeshObstacle>().enabled = true;
        foreach (EnemyAI _enemyAI in GameManager.instance.Level_Manager.EnemiesAI)
        {
            _enemyAI.AI_FSM.SetTrigger("ChangePhase");
            _enemyAI.AI_FSM.SetTrigger("ToExePhase");
            _enemyAI.GetComponent<CapsuleCollider>().enabled = true;
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
        GameManager.instance.UI_Manager.PhaseTxt.gameObject.SetActive(false);
    }

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
