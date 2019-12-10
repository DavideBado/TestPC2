using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string PursueTrgger, AlertTrigger, PatrolTrigger, ResearchTrigger, LookAroundTrigger, CatchHiddenPlayerTrigger;
    public string PauseTrigger;
    string BackTrigger = "";
    [HideInInspector]
    public string CurrentTrigger = "";
    public Animator AI_FSM;

    public EnemyNavController EnemyController;

    #region Actions
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
    #endregion

    #region DelegatesDef
    public delegate void PauseDelegateDef(bool _inPause);
    #endregion

    #region Delegates
    public PauseDelegateDef PauseDelegate;
    #endregion

    private void OnEnable()
    {
        PauseDelegate += SetPauseTrigger;
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
        EmenyHeardRun += SetResearchCounterMinValue;
        EmenyAloneHeardObj += SetResearchTrigger;        
        EmenySeePlayerInHidingSpot += SetCatchHiddenPlayerTrigger;
    }

    private void OnDisable()
    {
        PauseDelegate -= SetPauseTrigger;
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
        EmenyHeardRun -= SetResearchCounterMinValue;
        EmenyAloneHeardObj -= SetResearchTrigger;
        EmenySeePlayerInHidingSpot -= SetCatchHiddenPlayerTrigger;
    }

    private void SetPatrolTrigger()
    {
        BackTrigger = CurrentTrigger;
        CurrentTrigger = PatrolTrigger;

        AI_FSM.SetTrigger(PatrolTrigger);
    }
    private void SetAlertTrigger()
    {
        BackTrigger = CurrentTrigger;
        CurrentTrigger = AlertTrigger;

        AI_FSM.SetTrigger(AlertTrigger);
    }
    private void SetPursueTrigger()
    {
        BackTrigger = CurrentTrigger;
        CurrentTrigger = PursueTrgger;

        AI_FSM.SetTrigger(PursueTrgger);
    }
    private void SetResearchTrigger()
    {
        BackTrigger = CurrentTrigger;
        CurrentTrigger = ResearchTrigger;

        AI_FSM.SetTrigger(ResearchTrigger);
    }
    private void SetLookAroundTrigger()
    {
        BackTrigger = CurrentTrigger;
        CurrentTrigger = LookAroundTrigger;

        AI_FSM.SetTrigger(LookAroundTrigger);
    }
    private void SetCatchHiddenPlayerTrigger()
    {
        BackTrigger = CurrentTrigger;
        CurrentTrigger = CatchHiddenPlayerTrigger;

        AI_FSM.SetTrigger(CatchHiddenPlayerTrigger);
    }

    private void SetPauseTrigger(bool _inPause)
    {
        if (_inPause)
        {
            BackTrigger = CurrentTrigger;
            CurrentTrigger = PauseTrigger;
        }
        else
        {
            CurrentTrigger = BackTrigger;
            BackTrigger = PauseTrigger;
        }

        AI_FSM.SetTrigger(CurrentTrigger);
    }

    private void SetResearchCounterMinValue()
    {
        if (EnemyController.Counter < EnemyController.Counter_Alert_MaxValue)
        {
            EnemyController.Counter = EnemyController.Counter_Alert_MaxValue; 
        }
    }
}
