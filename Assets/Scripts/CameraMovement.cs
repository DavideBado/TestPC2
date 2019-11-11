using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float turnSpeed;
    public Transform player;

    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(player.position.x, player.position.y + 3f, player.position.z );
    }

    void LateUpdate()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
