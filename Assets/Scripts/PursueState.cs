using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursueState : StateMachineBehaviour
{
    EnemyNavController enemyNavController;
    EnemyAI enemyAI;
    NavMeshAgent agent;
    Transform savedTarget;
    float timer;
   
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyNavController = animator.GetComponent<EnemyNavController>();
        enemyAI = animator.GetComponent<EnemyAI>();
        agent = animator.GetComponent<NavMeshAgent>();
        timer = enemyNavController.Counter_Pursue_MaxValue;
        enemyNavController.GetComponent<MeshRenderer>().material = enemyNavController.graphicsController.PursueMat;
        enemyNavController.graphicsController.PursueAnimGObj.SetActive(true);
        agent.speed = enemyNavController.RunSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemyNavController.visibleTarget)
        {
            savedTarget = enemyNavController.visibleTarget;
            agent.destination = enemyNavController.visibleTarget.position;
            if (Vector3.Distance(animator.transform.position, enemyNavController.visibleTarget.position) < enemyNavController.GameOverDist)
            {
                // GAMEOVER
                if (Camera.main)
                {
                    Camera.main.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            agent.destination = savedTarget.position;
            timer -= Time.deltaTime;
            if(timer <= 0) enemyAI.PursueStateMissThePlayer?.Invoke();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyNavController.graphicsController.PursueAnimGObj.SetActive(false);
        enemyNavController.GetComponent<MeshRenderer>().material = enemyNavController.graphicsController.PatrolMat;
    }
}