using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<EnemyAI> EnemiesAI = new List<EnemyAI>();
    public GameObject Level;

    private void Start()
    {
        EnemiesAI = FindObjectsOfType<EnemyAI>().ToList();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SetUpLevel;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SetUpLevel;        
    }

    void SetUpLevel(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 1 && !GameManager.instance.OnPlanPhase && !GameManager.instance.OnExePhase && !GameManager.instance.FlowFSM.GetCurrentAnimatorStateInfo(0).IsName("PlanningPhase"))
        {
            GameManager.instance.ChangePhase();
        }
    }
    public void ReloadLevel(int thisSceneIndex)
    {
        SceneManager.LoadScene(thisSceneIndex);
        GameManager.instance.FlowFSM.ResetTrigger(GameManager.instance.GameOverTrigger);
        GameManager.instance.FlowFSM.SetTrigger(GameManager.instance.ChangePhaseTrigger);
    }

    public void GoToMainMenu(int mainMenuSceneIndex)
    {
        GameManager.instance.FlowFSM.ResetTrigger(GameManager.instance.GameOverTrigger);
        GameManager.instance.FlowFSM.SetTrigger(GameManager.instance.MainMenuTrigger);
        SceneManager.LoadScene(mainMenuSceneIndex);
    }
}
