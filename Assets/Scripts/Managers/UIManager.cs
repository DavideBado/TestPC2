using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public Canvas MaiCanvas;
    public TMP_Text PhaseTxt;
    public GameObject GameOverPanel;
    public GameObject Win;
    
    public GameObject ReloadButtonGameOver;
    public GameObject MainMenuButtonGameOver;
    public TMP_Text GameOverTxt;

    public GameObject ReloadButtonWin;
    public GameObject MainMenuButtonWin;
    public TMP_Text WinTxt;

    public float TxtFadeSpeed;

    bool InGameOver = false;
    bool InWin = false;

    public Action StartGameOverFade;
    public Action StartWinFade;

    private void OnEnable()
    {
        StartGameOverFade += GameOverInUpdate;
        StartWinFade += WinInUpdate;
        SceneManager.sceneLoaded += ResetUI;
    }

    private void OnDisable()
    {
        StartGameOverFade -= GameOverInUpdate;
        SceneManager.sceneLoaded -= ResetUI;
    }
    private void GameOverInUpdate()
    {
        InGameOver = true;
    }

    private void WinInUpdate()
    {
        InWin = true;
    }

    private void GameOverFade()
    {
        if (GameOverTxt.color.a > 0)
        {
            GameOverTxt.color = new Color(GameOverTxt.color.r, GameOverTxt.color.b, GameOverTxt.color.g, GameOverTxt.color.a - (TxtFadeSpeed * Time.deltaTime));
        }
        else
        {
            ReloadButtonGameOver.SetActive(true);
            MainMenuButtonGameOver.SetActive(true);
            InGameOver = false;
        }
    }

    private void WinFade()
    {
        if (WinTxt.color.a > 0)
        {
            WinTxt.color = new Color(WinTxt.color.r, WinTxt.color.b, WinTxt.color.g, WinTxt.color.a - (TxtFadeSpeed * Time.deltaTime));
        }
        else
        {
            ReloadButtonWin.SetActive(true);
            MainMenuButtonWin.SetActive(true);
            InWin = false;
        }
    }

    private void Update()
    {
        if (InGameOver)
        {
            GameOverFade();
        }
        else if (InWin)
        {
            WinFade();
        }
    }


    void ResetUI(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1)
        {
            GameOverTxt.color = new Color(GameOverTxt.color.r, GameOverTxt.color.b, GameOverTxt.color.g, 1f);
            ReloadButtonGameOver.SetActive(false);
            MainMenuButtonGameOver.SetActive(false);
            WinTxt.color = new Color(WinTxt.color.r, WinTxt.color.b, WinTxt.color.g, 1f);
            ReloadButtonWin.SetActive(false);
            MainMenuButtonWin.SetActive(false);
        }
    }
}
