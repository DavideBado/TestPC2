using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string PursueTrgger, AlertTrigger, PatrolTrigger, ResearchTrigger, LookAroundTrigger;
    public Animator AI_FSM;

    public Action PatrolStateDetectAPlayer;
    public Action AlertStateMaxCounter;
    public Action AlertStateMissThePlayer;
    public Action ResearchStateMaxCounter;
    public Action ResearchStateMissPlayer;
    public Action LookAroundStateEndRot;
    public Action LookAroundDetectThePlayer;
    public Action PursueStateMissThePlayer;
    public Action EmenyHeardWalk;
    public Action EmenyHeardRun;
    public Action EmenyAloneHeardObj;

    [HideInInspector]
    public NoiseController.NoiseType currentNoiseType;
    [HideInInspector]
    public NoiseController.NoiseType prevNoiseType;

    private void OnEnable()
    {
        PatrolStateDetectAPlayer += SetAlertTrigger;
        AlertStateMaxCounter += SetResearchTrigger;
        AlertStateMissThePlayer += SetPatrolTrigger;
        ResearchStateMaxCounter += SetPursueTrigger;
        ResearchStateMissPlayer += SetLookAroundTrigger;
        LookAroundStateEndRot += SetPatrolTrigger;
        LookAroundDetectThePlayer += SetResearchTrigger;
        PursueStateMissThePlayer += SetResearchTrigger;
        EmenyHeardWalk += SetAlertTrigger;
        EmenyHeardRun += SetResearchTrigger;
        EmenyAloneHeardObj += SetResearchTrigger;
    }

    private void OnDisable()
    {
        PatrolStateDetectAPlayer -= SetAlertTrigger;
        AlertStateMaxCounter -= SetResearchTrigger;
        AlertStateMissThePlayer -= SetPatrolTrigger;
        ResearchStateMaxCounter += SetPursueTrigger;
        ResearchStateMissPlayer -= SetLookAroundTrigger;
        LookAroundStateEndRot -= SetPatrolTrigger;
        LookAroundDetectThePlayer -= SetResearchTrigger;
        PursueStateMissThePlayer -= SetResearchTrigger;
        EmenyHeardWalk -= SetAlertTrigger;
        EmenyHeardRun -= SetResearchTrigger;
        EmenyAloneHeardObj -= SetResearchTrigger;
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
    private void SetResearchTrigger()
    {
        AI_FSM.SetTrigger(ResearchTrigger);
    }
    private void SetLookAroundTrigger()
    {
        AI_FSM.SetTrigger(LookAroundTrigger);
    }
}
