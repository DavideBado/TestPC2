using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMapElements : MonoBehaviour
{

    bool dragging;
    public GameObject imageToDrag;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && dragging == false)
        {
            GameObject newImageToDrag = GameObject.Instantiate(imageToDrag, Vector3.zero, Quaternion.identity);
            newImageToDrag.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            dragging = true;
        }
        else if (Input.GetMouseButtonUp(0) && dragging == true)
        {
            dragging = false;
        }
    }
}
