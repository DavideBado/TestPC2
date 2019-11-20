using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMapElements : MonoBehaviour
{
    public GameObject imageToDrag;
    public Canvas canvas;
    public Transform Tomas;

    public void NewIconInstance()
    {
        GameObject newImageToDrag = GameObject.Instantiate(imageToDrag, Vector3.zero, Quaternion.identity);
        newImageToDrag.transform.parent = Tomas;
        Tomas.GetComponent<testFollowMousePos>().pezza = true;
        newImageToDrag.SetActive(true);
    }
}
