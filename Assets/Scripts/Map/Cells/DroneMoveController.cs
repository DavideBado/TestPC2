﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class DroneMoveController : MonoBehaviour
{
    public KeyCode FirstAllSpotPosTypes, SecondAllSpotPosTypes;
    public float speed;
    public LayerMask WallMask;
    public GameObject Pointer;
    public CinemachineFreeLook DroneCamera;
    public List<CellTypeBase> AllSpotPosTypes = new List<CellTypeBase>();
    [HideInInspector]
    public CellTypeBase CurrentIspotType;

    private void Update()
    {
        CheckInput();
        CheckCells();
        SelectCurrentSpotType();
        ActiveSpot();
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
    RaycastHit Currenthit;
        RaycastHit Oldhit;
    void CheckCells()
    {

        LayerMask layerMask = ~WallMask;
        if (Physics.Raycast(DroneCamera.transform.position, Pointer.transform.position - DroneCamera.transform.position, out Currenthit, 500000f, layerMask))
        {
            Debug.DrawLine(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10)), Currenthit.point, Color.red, 5f);
            if (Currenthit.transform.GetComponent<Cell3D>() != null)
            {
                Currenthit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                Oldhit = Currenthit;
            }
            else Oldhit.transform.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        else if(Oldhit.transform) Oldhit.transform.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    void ActiveSpot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            List<CellTypeBase> cellTypeBases = Currenthit.transform.GetComponents<CellTypeBase>().ToList();

            for (int i = 0; i < cellTypeBases.Count; i++)
            {
                cellTypeBases[i].enabled = false;
            }

            for (int i = 0; i < cellTypeBases.Count; i++)
            {
                if (cellTypeBases[i].GetType() == CurrentIspotType.GetType())
                {
                    cellTypeBases[i].enabled = true;
                    break;
                }
            }
        }
    }

    void SelectCurrentSpotType()
    {
        if (Input.GetKeyDown(FirstAllSpotPosTypes)) CurrentIspotType = AllSpotPosTypes[0];
        if (Input.GetKeyDown(SecondAllSpotPosTypes)) CurrentIspotType = AllSpotPosTypes[1];
    }
}