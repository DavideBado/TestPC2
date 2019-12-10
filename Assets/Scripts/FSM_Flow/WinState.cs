using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.instance.OnExePhase = false;
        GameManager.instance.OnPlanPhase = false;
        GameManager.instance.UI_Manager.Win.SetActive(true);
        GameManager.instance.UI_Manager.StartWinFade?.Invoke();
        //GameManager.instance.Level_Manager.Level.SetActive(false);
        foreach (EnemyAI _enemy in GameManager.instance.Level_Manager.EnemiesAI)
        {
            _enemy.PauseDelegate(_enemy.GetComponent<EnemyNavController>().graphicsController.gameObject.activeSelf);
        }
        GameManager.instance.Player.TurnOnOffThePlayer(!GameManager.instance.Player.Graphics.activeSelf);
        GameManager.instance.Level_Manager.Level.SetActive(!GameManager.instance.Level_Manager.Level.activeSelf);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.instance.OnExePhase = false;
        //GameManager.instance.Level_Manager.Level.SetActive(true);
        foreach (EnemyAI _enemy in GameManager.instance.Level_Manager.EnemiesAI)
        {
            _enemy.PauseDelegate(_enemy.GetComponent<EnemyNavController>().graphicsController.gameObject.activeSelf);
        }
        GameManager.instance.Player.TurnOnOffThePlayer(!GameManager.instance.Player.Graphics.activeSelf);
        GameManager.instance.Level_Manager.Level.SetActive(!GameManager.instance.Level_Manager.Level.activeSelf);
        GameManager.instance.UI_Manager.Win.SetActive(false);
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
