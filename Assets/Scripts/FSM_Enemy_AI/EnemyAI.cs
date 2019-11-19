using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string PursueTrgger, AlertTrigger, PatrolTrigger, ResearchTrigger, LookAroundTrigger, CatchHiddenPlayer;
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
    public Action EmenySeePlayerInHidingSpot;

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
        EmenySeePlayerInHidingSpot += SetCatchHiddenPlayerTrigger;
    }

    private void OnDisable()
    {
        PatrolStateDetectAPlayer -= SetAlertTrigger;
        AlertStateMaxCounter -= SetResearchTrigger;
        AlertStateMissThePlayer -= SetPatrolTrigger;
        ResearchStateMaxCounter -= SetPursueTrigger;
        ResearchStateMissPlayer -= SetLookAroundTrigger;
        LookAroundStateEndRot -= SetPatrolTrigger;
        LookAroundDetectThePlayer -= SetResearchTrigger;
        PursueStateMissThePlayer -= SetResearchTrigger;
        EmenyHeardWalk -= SetAlertTrigger;
        EmenyHeardRun -= SetResearchTrigger;
        EmenyAloneHeardObj -= SetResearchTrigger;
        EmenySeePlayerInHidingSpot -= SetCatchHiddenPlayerTrigger;
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
    private void SetCatchHiddenPlayerTrigger()
    {
        AI_FSM.SetTrigger(CatchHiddenPlayer);
    }
}
