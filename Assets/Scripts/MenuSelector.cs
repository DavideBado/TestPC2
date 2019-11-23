using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelector : MonoBehaviour
{
    public Camera MapCamera, MainCamera;
    public GameObject Mouse, Pad, Pin;
    public KeyCode OpenMouse, OpenPin;
    public string OpenPad;
    public bool InMapView = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(OpenMouse))
        {
            Mouse.SetActive(!Mouse.activeSelf);
            InMapView = Mouse.activeSelf;
            MainCamera.gameObject.SetActive(!MainCamera.gameObject.activeSelf);
            MapCamera.gameObject.SetActive(!MapCamera.gameObject.activeSelf);
            foreach (EnemyAI _enemy in GameManager.instance.Level_Manager.EnemiesAI)
            {
                _enemy.PauseDelegate(_enemy.GetComponent<EnemyNavController>().graphicsController.gameObject.activeSelf);
            }
            if (!GameManager.instance.Player.isHiding)
            {
                GameManager.instance.Player.TurnOnOffThePlayer(!GameManager.instance.Player.Graphics.activeSelf);
            }
            GameManager.instance.Level_Manager.Level.SetActive(!GameManager.instance.Level_Manager.Level.activeSelf);
            GameManager.instance.UI_Manager.PhaseTxt.gameObject.SetActive(!GameManager.instance.UI_Manager.PhaseTxt.gameObject.activeSelf);
        }
        if (Input.GetKeyDown(OpenPin))
        {
            Pin.SetActive(!Pin.activeSelf);
            MainCamera.gameObject.SetActive(!MainCamera.gameObject.activeSelf);
            MapCamera.gameObject.SetActive(!MapCamera.gameObject.activeSelf);
            GameManager.instance.Level_Manager.Level.SetActive(!GameManager.instance.Level_Manager.Level.activeSelf);
        }
        else if (Input.GetAxis(OpenPad) != 0)
        {
            Pad.SetActive(!Pad.activeSelf);
        }
    }
}
