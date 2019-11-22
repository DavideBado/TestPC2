using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public UIManager UI_Manager;
    public LevelManager Level_Manager;
    public Animator FlowFSM;
    public PlayerMovController Player;
    public string ChangePhaseTrigger, GameOverTrigger, WinTrigger;

    public Action PlayerCaught;
    public Action PlayerGoal;

    [HideInInspector]
    public bool OnExePhase = false;

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
}

