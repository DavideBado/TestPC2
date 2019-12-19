using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DroneMoveController : MonoBehaviour
{
    public float speed;
    public LayerMask WallMask;
    public GameObject Pointer;
    public CinemachineFreeLook DroneCamera;

    private void Update()
    {
        CheckInput();
        CheckCells();
    }
    void CheckInput()
    {
        Vector3 VerticalTranslation = Input.GetAxis("Vertical") * speed * Time.deltaTime * transform.forward;
        Vector3 HorizontalTranslation = Input.GetAxis("Horizontal") * speed * Time.deltaTime * transform.right;

        transform.position += VerticalTranslation + HorizontalTranslation;
        //VerticalTranslation *= Time.deltaTime;
        //HorizontalTranslation *= Time.deltaTime;

        //if (VerticalTranslation > 0)
        //{
        //    if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), transform.forward, 0.5f, WallMask)) transform.Translate(0, 0, VerticalTranslation);
        //}
        //else if (VerticalTranslation < 0) if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), -transform.forward, 0.5f, WallMask))
        //    {
        //        transform.Translate(0, 0, VerticalTranslation);
        //        Debug.DrawLine(transform.position, -transform.forward, Color.red, 1);
        //    }

        //if (HorizontalTranslation > 0)
        //{
        //    if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), transform.right, 0.5f, WallMask)) transform.Translate(HorizontalTranslation, 0, 0);
        //}
        //else if (HorizontalTranslation < 0) if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), -transform.right, 0.5f, WallMask))
        //    {
        //        transform.Translate(HorizontalTranslation, 0, 0);
        //        Debug.DrawLine(transform.position, -transform.forward, Color.red, 1);
        //    }
    }

    void CheckCells()
    {
        RaycastHit hit;
        if (Physics.Raycast(DroneCamera.transform.position, Pointer.transform.position - DroneCamera.transform.position, out hit, 500000f))
        {
            Debug.DrawLine(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10)), hit.point, Color.red, 5f);
            if (hit.transform.GetComponent<Cell3D>() != null)
            {
                hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
}