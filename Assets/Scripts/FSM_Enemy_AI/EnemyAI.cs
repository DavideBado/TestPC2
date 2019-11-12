using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string PursueTrgger, AlertTrigger, PatrolTrigger, LookAroundTrigger;
    public Animator AI_FSM;

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
        AI_FSM.SetTrigger(PatrolTrigger);
    }
    private void SetAlertTrigger()
    {
        AI_FSM.SetTrigger(AlertTrigger);
    }
    private void SetPursueTrigger()
    {
        AI_FSM.SetTrigger(PursueTrgger);
    }
    private void SetLookAroundTrigger()
    {
        AI_FSM.SetTrigger(LookAroundTrigger);
    }
}
