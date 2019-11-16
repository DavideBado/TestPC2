using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float turnSpeed;
    public Transform player;

    public Vector3 originCameraPos;
    public Quaternion originCameraRot;
    public Vector3 currentCameraPos;
    public Quaternion currentCameraRot;

    void Start()
    {
        originCameraPos = this.transform.position;
        originCameraRot = this.transform.rotation;

        currentCameraPos = originCameraPos;
        currentCameraRot = originCameraRot;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            currentCameraPos = this.transform.position;
            currentCameraRot = this.transform.rotation;
            transform.RotateAround(player.transform.position, Vector3.up, Input.GetAxis("Mouse X") * turnSpeed); 
        }
    }

}
