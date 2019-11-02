using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string PursueTrgger, AlertTrigger, PatrolTrigger;
    public Animator AIState;

    public Action PatrolStateDetectAPlayer;
    public Action AlertStateMaxCounter;

    private void OnEnable()
    {
        PatrolStateDetectAPlayer += SetAlertTrigger;
        AlertStateMaxCounter += SetPursueTrigger;
    }

    private void OnDisable()
    {
        PatrolStateDetectAPlayer -= SetAlertTrigger;
        AlertStateMaxCounter -= SetPursueTrigger;
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
}
