using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTest : MonoBehaviour
{
    public GameObject LinerendPrefab;
    LineRenderer lineRenderer;
    Vector3 prevMousePos = new Vector3();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject newLinerendererObj = GameObject.Instantiate(LinerendPrefab, Vector3.zero, Quaternion.identity);
            lineRenderer = newLinerendererObj.GetComponent<LineRenderer>();
            lineRenderer.positionCount = 0;
        }
        if (Input.GetKey(KeyCode.Mouse0) && Input.mousePosition != prevMousePos && lineRenderer)
        {
            lineRenderer.positionCount++;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 point = Camera.main.ScreenToWorldPoint(mousePos);
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(point.x, point.y));
            prevMousePos = Input.mousePosition;
        }
    }
}
