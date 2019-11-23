using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGraphicsController : MonoBehaviour
{
    public Material PatrolMat;
    public Material AlertMat;
    public Material LookAroundMat;
    public Material PursueMat;

    public GameObject AlertAnimGObj;
    public GameObject LookAroundAnimGObj;
    public GameObject PursueAnimGObj;

    public Animator animator;
    public EnemyNavController EnemyController;

    private Vector3 previousPosition;
    public float curSpeed;

    //private void Start()
    //{
    //    previousPosition = EnemyController.transform.position;
    //}

    void Update()
    {
        //Vector3 curMove = EnemyController.transform.position - previousPosition;
        //curSpeed = curMove.magnitude / Time.deltaTime;
        //previousPosition = EnemyController.transform.position;

        animator.SetFloat("Speed", EnemyController.agent.speed);
    }
}
