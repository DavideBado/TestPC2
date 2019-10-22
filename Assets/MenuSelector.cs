using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelector : MonoBehaviour
{
    public GameObject Mouse, Pad;
    public KeyCode OpenMouse;
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
        }
        else if(Input.GetAxis(OpenPad) != 0)
        {
            Pad.SetActive(!Pad.activeSelf);
        }
    }
}
