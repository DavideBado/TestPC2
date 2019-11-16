using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float turnSpeed;
    public Transform player;

    Vector3 originCameraPos;
    Vector3 currentCameraPos;

    void Start()
    {
        originCameraPos = transform.position;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            transform.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("Mouse X") * turnSpeed); 
        }
    }

}
