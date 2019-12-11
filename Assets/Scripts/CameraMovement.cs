using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera m_camera;
    public Transform player;
    public Vector3 point;

    private void Update()
    {
        float distance = Vector3.Distance(m_camera.transform.position, player.transform.position);
        point = m_camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10));
    }
}
