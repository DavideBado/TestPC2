﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavController : MonoBehaviour
{
    public List<Transform> PathTargets = new List<Transform>();
    [HideInInspector]
    public Transform visibleTarget;
    //[HideInInspector]
    public Transform SoundTarget;
    [HideInInspector]
    public int visibleTargetArea;
    public NavMeshAgent agent;
    int destinationIndex = 0;
    public bool FermatiAGuardare;
    public FieldOfView fieldOfView;
    float fieldOfViewOriginalViewRadius;
    [Range(0, 1)]
    public float VelocitaIncrementoCono;
    public float LunghezzaMaxCono;
    Vector3 lastPlayerPos;
    Vector3 myLastPos;
    public float AttesaTraLeRotazioni;
    public float TimeForLookAround;
    int rotationStatesIndex = 0;
    float rotTimer;
    public float GameOverDist;

    [Range(0, 5)]
    public List<float> ModCounters = new List<float>();

    public float Counter_Patrol_MaxValue;

    public float Counter_Alert_MaxValue;

    public float Counter_Pursue_MaxValue;

    public Material PatrolMat;
    public Material AlertMat;
    public Material LookAroundMat;
    public Material PursueMat;
}