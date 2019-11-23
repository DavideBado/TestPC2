﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PlanningPhaseState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
/*        SceneManager.sceneLoaded += */StartLevel();
    }

    private static void StartLevel(/*Scene scene, LoadSceneMode loadSceneMode*/)
    {
        GameManager.instance.Setup();
        GameManager.instance.OnExePhase = false;
        GameManager.instance.OnPlanPhase = true;
        GameManager.instance.UI_Manager.PhaseTxt.gameObject.SetActive(true);
        GameManager.instance.UI_Manager.PhaseTxt.text = "PlanningPhase";
        GameManager.instance.Player.transform.position = GameManager.instance.Player.ResetPosition;
        GameManager.instance.Player.GetComponent<NavMeshObstacle>().enabled = false;
        GameManager.instance.Player.currentSpeed = GameManager.instance.Player.walkSpeed;
        GameManager.instance.Player.isCrouching = false;
        foreach (EnemyAI _enemyAI in GameManager.instance.Level_Manager.EnemiesAI)
        {
            //_enemyAI.AI_FSM.SetTrigger("ChangePhase");
            _enemyAI.AI_FSM.SetTrigger("ToPlanPhase");
            _enemyAI.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(GameManager.instance.Level_Manager.Level == null) StartLevel();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.instance.OnPlanPhase = false;
        //SceneManager.sceneLoaded -= StartLevel;
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
