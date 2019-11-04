using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavController : MonoBehaviour
{
    public List<Transform> Targets = new List<Transform>();
    [HideInInspector]
    public Transform visibleTarget;
    //[HideInInspector]
    public int visibleTargetArea;
    public NavMeshAgent agent;
    int destinationIndex = 0;
    public bool FermatiAGuardare;
    public FieldOfView fieldOfView;
    float fieldOfViewOriginalViewRadius;
    [Range(0, 1)]
    public float VelocitaIncrementoCono;
    public float LunghezzaMaxCono;
    float deltaLunghezzaCono;
    bool needCheckLastPlayerPos = false;
    Vector3 lastPlayerPos;
    Vector3 myLastPos;
    bool onChange = true;
    public float AttesaTraLeRotazioni;
    public float TimeForLookAround;
    //List<RotationStates> rotationStates = new List<RotationStates>();
    int rotationStatesIndex = 0;
    float rotTimer;
    public float GameOverDist;

    [Range(0, 5)]
    public List<float> ModCounters = new List<float>();

    public float Counter_Patrol_MaxValue;

    public float Counter_Alert_MaxValue;

    float counter_Pursue;
    public float Counter_Pursue_MaxValue;
    private void Start()
    {
        //SetRotationStatesList();
        rotTimer = AttesaTraLeRotazioni;

        fieldOfViewOriginalViewRadius = fieldOfView.viewRadius;
        deltaLunghezzaCono = LunghezzaMaxCono - fieldOfViewOriginalViewRadius;

        agent.destination = Targets[destinationIndex].position;
    }

    private void Update()
    {
        float dist = agent.remainingDistance;
        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            float dst = Vector3.Distance(agent.destination, Targets[destinationIndex].position);
            if (dst < 1.5f)
            {
                destinationIndex++;
                if (destinationIndex >= Targets.Count)
                {
                    destinationIndex = 0;
                }
                agent.destination = Targets[destinationIndex].position;
            }

        }

        //LookTheNearestTarget();
    }

    //void LookTheNearestTarget()
    //{
    //    if (visibleTarget != null)
    //    {
    //        transform.LookAt(visibleTarget);
    //        lastPlayerPos = visibleTarget.position;
    //        myLastPos = transform.position;

    //        needCheckLastPlayerPos = true;

    //        if (FermatiAGuardare)
    //        {
    //            WalkPause(true);
    //        }

    //        ChangeFieldOfViewRadius(true);
    //    }
    //    else
    //    {
    //        WalkPause(false);
    //        if (needCheckLastPlayerPos)
    //        {
    //            GoToLastPlayerPos(lastPlayerPos);
    //        }
    //    }
    //}

    //void ChangeFieldOfViewRadius(bool _detect)
    //{
    //    if (_detect)
    //    {
    //        if (fieldOfView.viewRadius < LunghezzaMaxCono)
    //        {
    //            fieldOfView.viewRadius += deltaLunghezzaCono * VelocitaIncrementoCono * Time.deltaTime;
    //        }
    //    }
    //    else
    //    {
    //        if (fieldOfView.viewRadius != fieldOfViewOriginalViewRadius)
    //        {
    //            onChange = true;
    //            if (fieldOfView.viewRadius > fieldOfViewOriginalViewRadius)
    //            {
    //                fieldOfView.viewRadius -= deltaLunghezzaCono * VelocitaIncrementoCono * Time.deltaTime;
    //            }
    //            if (fieldOfView.viewRadius <= fieldOfViewOriginalViewRadius)
    //            {
    //                fieldOfView.viewRadius = fieldOfViewOriginalViewRadius;
    //                onChange = false;
    //            }
    //        }
    //        else
    //        {
    //            if (!onChange)
    //            {
    //                ReturnToLastPathPosition();
    //            }
    //        }
    //    }
    //}

    //void WalkPause(bool _pause)
    //{
    //    agent.isStopped = _pause;
    //}

    void GoToLastPlayerPos(Vector3 _lastPlayerPos)
    {
        agent.destination = _lastPlayerPos;
        float dist = agent.remainingDistance;
        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
           // LookAround();
        }
    }

    //void LookAround()
    //{
    //    switch (rotationStates[rotationStatesIndex])
    //    {
    //        case RotationStates.Left:
    //            RotateTowards(Vector3.left);
    //            break;
    //        case RotationStates.Right:
    //            RotateTowards(Vector3.right);
    //            break;
    //        case RotationStates.Forward:
    //            RotateTowards(Vector3.forward);
    //            break;
    //        case RotationStates.Back:
    //            RotateTowards(Vector3.back);
    //            break;
    //        case RotationStates.Wait:
    //            rotTimer -= Time.deltaTime;
    //            if (rotTimer <= 0)
    //            {
    //                rotTimer = AttesaTraLeRotazioni;
    //                rotationStatesIndex++;
    //            }
    //            break;
    //        case RotationStates.End:
    //            //ChangeFieldOfViewRadius(false);
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //private void RotateTowards(Vector3 target)
    //{
    //    //Vector3 direction = (target - transform.position).normalized;
    //    Quaternion lookRotation = Quaternion.LookRotation(target);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * TimeForLookAround);
    //    if (Quaternion.Angle(transform.rotation, lookRotation) < 1)
    //    {
    //        rotationStatesIndex++;
    //    }
    //}

    void ReturnToLastPathPosition()
    {
        agent.destination = myLastPos;
        float dist = agent.remainingDistance;
        if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            agent.destination = Targets[destinationIndex].position;
            needCheckLastPlayerPos = false;
            rotationStatesIndex = 0;
            onChange = true;
        }
    }

    //void SetRotationStatesList()
    //{
    //    rotationStates.Add(RotationStates.Left);
    //    rotationStates.Add(RotationStates.Wait);
    //    rotationStates.Add(RotationStates.Forward);
    //    rotationStates.Add(RotationStates.Wait);
    //    rotationStates.Add(RotationStates.Right);
    //    rotationStates.Add(RotationStates.Wait);
    //    rotationStates.Add(RotationStates.Back);
    //    rotationStates.Add(RotationStates.Wait);
    //    rotationStates.Add(RotationStates.End);
    //}

    //enum RotationStates
    //{
    //    Left,
    //    Right,
    //    Forward,
    //    Back,
    //    Wait,
    //    End
    //}
}
