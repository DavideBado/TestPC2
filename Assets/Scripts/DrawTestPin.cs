using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawTestPin : MonoBehaviour
{
    public Canvas canvas;
    public Camera MapCamera;
    public GameObject LinerendPrefab;
    public GameObject Pin;
    LineRenderer lineRenderer;
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
            newLinerendererObj.transform.parent = transform;
            lineRenderer = newLinerendererObj.GetComponent<LineRenderer>();
            lineRenderer.positionCount = 0;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && lineRenderer)
        {
            lineRenderer.positionCount++;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 point = MapCamera.ScreenToWorldPoint(mousePos);
            GameObject newPin = Instantiate(Pin, canvas.transform);
            newPin.GetComponent<Image>().rectTransform.position = point;
            //newPin.transform.position = new Vector3(newPin.transform.position.x, newPin.transform.position.y, 0);
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(newPin.transform.position.x, newPin.transform.position.y));
        }
    }
}
