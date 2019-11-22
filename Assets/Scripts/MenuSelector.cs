using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelector : MonoBehaviour
{
    public GameObject Level;
    public Camera MapCamera, MainCamera;
    public GameObject Mouse, Pad, Pin;
    public KeyCode OpenMouse, OpenPin;
    public string OpenPad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(OpenMouse))
        {
            Mouse.SetActive(!Mouse.activeSelf);
            MainCamera.gameObject.SetActive(!MainCamera.gameObject.activeSelf);
            MapCamera.gameObject.SetActive(!MapCamera.gameObject.activeSelf);
            foreach (EnemyAI _enemy in GameManager.instance.Level_Manager.EnemiesAI)
            {
                _enemy.PauseDelegate(_enemy.GetComponent<EnemyNavController>().graphicsController.gameObject.activeSelf);
            }
            GameManager.instance.Player.TurnOnOffThePlayer(!GameManager.instance.Player.Graphics.activeSelf);
            Level.SetActive(!Level.activeSelf);
        }
        if (Input.GetKeyDown(OpenPin))
        {
            Pin.SetActive(!Pin.activeSelf);
            MainCamera.gameObject.SetActive(!MainCamera.gameObject.activeSelf);
            MapCamera.gameObject.SetActive(!MapCamera.gameObject.activeSelf);
            Level.SetActive(!Level.activeSelf);
        }
        else if(Input.GetAxis(OpenPad) != 0)
        {
            Pad.SetActive(!Pad.activeSelf);
        }
    }
}
