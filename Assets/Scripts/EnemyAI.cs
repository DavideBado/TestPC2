using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string PursueTrgger, AlertTrigger, PatrolTrigger, LookAroundTrigger;
    public Animator AIState;

    public Action PatrolStateDetectAPlayer;
    public Action AlertStateMaxCounter;
    public Action AlertStateMissThePlayer;
    public Action LookAroundStateEndRot;
    public Action LookAroundDetectThePlayer;
    public Action PursueStateMissThePlayer;

    private void OnEnable()
    {
        PatrolStateDetectAPlayer += SetAlertTrigger;
        AlertStateMaxCounter += SetPursueTrigger;
        AlertStateMissThePlayer += SetLookAroundTrigger;
        LookAroundStateEndRot += SetPatrolTrigger;
        LookAroundDetectThePlayer += SetAlertTrigger;
        PursueStateMissThePlayer += SetLookAroundTrigger;
    }

    private void OnDisable()
    {
        PatrolStateDetectAPlayer -= SetAlertTrigger;
        AlertStateMaxCounter -= SetPursueTrigger;
        AlertStateMissThePlayer -= SetLookAroundTrigger;
        LookAroundStateEndRot -= SetPatrolTrigger;
        LookAroundDetectThePlayer -= SetAlertTrigger;
        PursueStateMissThePlayer -= SetLookAroundTrigger;
    }

    private void SetPatrolTrigger()
    {
        AIState.SetTrigger(PatrolTrigger);
    }
    private void SetAlertTrigger()
    {
        AIState.SetTrigger(AlertTrigger);
    }
    private void SetPursueTrigger()
    {
        AIState.SetTrigger(PursueTrgger);
    }
    private void SetLookAroundTrigger()
    {
        AIState.SetTrigger(LookAroundTrigger);
    }
}
