using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public UIManager UI_Manager;
    public LevelManager Level_Manager;
    public Animator FlowFSM;
    public PlayerMovController Player;
    public DroneMoveController Drone;
    public string ChangePhaseTrigger, GameOverTrigger, WinTrigger, MainMenuTrigger;

    public KeyCode ChangePhaseKey;

    public Action PlayerCaught;
    public Action PlayerGoal;

    [HideInInspector]
    public bool OnExePhase = false;
    [HideInInspector]
    public bool OnPlanPhase = false;

    private void OnEnable()
    {
        PlayerCaught += GameOver;
        PlayerGoal += Win;   
    }

    private void OnDisable()
    {
        PlayerCaught -= GameOver;
        PlayerGoal -= Win;   
    }

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void ChangePhase()
    {
        FlowFSM.SetTrigger(ChangePhaseTrigger);
    }

    private void GameOver()
    {
        FlowFSM.SetTrigger(GameOverTrigger);
    }

    private void Win()
    {
        FlowFSM.SetTrigger(WinTrigger);
    }

    private void Update()
    {
        if (Input.GetKeyDown(ChangePhaseKey) && OnPlanPhase) ChangePhase();
    }

    public void Setup()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Level_Manager.EnemiesAI.Clear();
            if (!Player) Player = FindObjectOfType<PlayerMovController>();
            if (!Drone) Drone = FindObjectOfType<DroneMoveController>();
       /*     if (Level_Manager.EnemiesAI.Count == 0) */Level_Manager.EnemiesAI = FindObjectsOfType<EnemyAI>().ToList();
            if (!Level_Manager.Level) Level_Manager.Level = FindObjectOfType<PezzaMissingLevel>().gameObject;
        }
    }
}

