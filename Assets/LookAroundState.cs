﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LookAroundState : StateMachineBehaviour
{
    EnemyNavController enemyNavController;
    EnemyAI enemyAI;
    int rotationStatesIndex = 0;
    List<Vector3> rotDirections = new List<Vector3>();

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyNavController = animator.GetComponent<EnemyNavController>();
        enemyAI = animator.GetComponent<EnemyAI>();
        SetupRotDirections();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RotateTowards(rotDirections[rotationStatesIndex], enemyNavController);
        CheckThePlayer();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    private void SetupRotDirections()
    {
        rotDirections.Add(Vector3.forward);
        rotDirections.Add(Vector3.right);
        rotDirections.Add(Vector3.back);
        rotDirections.Add(Vector3.left);
    }
    private void RotateTowards(Vector3 target, EnemyNavController _this)
    {
        Quaternion lookRotation = Quaternion.LookRotation(target);
        _this.transform.rotation = Quaternion.Slerp(_this.transform.rotation, lookRotation, Time.deltaTime * _this.TimeForLookAround / rotDirections.Count);
        if (Quaternion.Angle(_this.transform.rotation, lookRotation) < 1)
        {
            rotationStatesIndex++;
            if (rotationStatesIndex >= rotDirections.Count) enemyAI.LookAroundStateEndRot?.Invoke();
        }
    }

    private void CheckThePlayer()
    {
        if(enemyNavController.visibleTarget) enemyAI.LookAroundDetectThePlayer?.Invoke();
    }
}
