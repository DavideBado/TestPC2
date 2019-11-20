using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMapElements : MonoBehaviour
{
    public GameObject imageToDrag;
    public Canvas canvas;

    public void NewIconInstance()
    {
        GameObject newImageToDrag = GameObject.Instantiate(imageToDrag, Vector3.zero, Quaternion.identity);
        newImageToDrag.transform.parent = canvas.transform;
        newImageToDrag.SetActive(true);
    }
}
