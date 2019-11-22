using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavController : MonoBehaviour
{
    public EnemyGraphicsController graphicsController;
    public List<Transform> PathTargets = new List<Transform>();
    [HideInInspector]
    public Transform VisibleTarget;
    [HideInInspector]
    public Transform OldVisibleTarget;

    [HideInInspector]
    public bool TargetPrevHidingState = false;
    [HideInInspector]
    public bool TargetCurrentHidingState = false;
    [HideInInspector]
    public Transform HiddenTarget;

    [HideInInspector]
    public Transform NoiseTarget;
    [HideInInspector]
    public NoiseController.NoiseType currentNoiseType;
    [HideInInspector]
    public NoiseController.NoiseType prevNoiseType;

    [HideInInspector]
    public int visibleTargetArea;
    public NavMeshAgent agent;
    public bool FermatiAGuardare;
    public FieldOfView fieldOfView;
    float fieldOfViewOriginalViewRadius;   
    Vector3 lastPlayerPos;
    Vector3 myLastPos;   
    public float TimeForLookAround;
    float rotTimer;
    public float GameOverDist;

    [Range(0, 5)]
    public List<float> ModCounters = new List<float>();

    public float Counter_Patrol_MaxValue;

    public float Counter_Alert_MaxValue;

    public float Counter_Research_MaxValue;

    public float Counter_Pursue_MaxValue;

    public float WalkSpeed;
    public float ResearchSpeed;
    public float RunSpeed;

    [HideInInspector]
    public float Counter;

    public Vector3 IdlePosition;
    public Vector3 IdleTriggerTrapPosition;
    public Vector3 IdleTriggerTrapDim;
}
