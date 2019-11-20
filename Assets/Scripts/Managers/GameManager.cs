using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public UIManager UI_Manager;
    public LevelManager Level_Manager;
    public Animator FlowFSM;
    public string ChangePhaseTrigger, GameOverTrigger;

    public Action PlayerCaught;

    private void OnEnable()
    {
        PlayerCaught += GameOver;   
    }

    private void OnDisable()
    {
        PlayerCaught -= GameOver;
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
}

