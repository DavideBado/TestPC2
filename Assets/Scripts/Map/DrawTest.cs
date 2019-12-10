using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawTest : MonoBehaviour
{
    public Camera MapCamera;
    public GameObject LinerendPrefab;
    LineRenderer lineRenderer;
    Vector3 prevMousePos = new Vector3();

    public GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    public EventSystem m_EventSystem;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject newLinerendererObj = GameObject.Instantiate(LinerendPrefab, Vector3.zero, Quaternion.identity);
            newLinerendererObj.transform.parent = transform;
            lineRenderer = newLinerendererObj.GetComponent<LineRenderer>();
            lineRenderer.positionCount = 0;
        }
        if (Input.GetKey(KeyCode.Mouse0) && Input.mousePosition != prevMousePos && lineRenderer)
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);

            m_Raycaster.Raycast(m_PointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.tag == "Map")
                {
                    lineRenderer.positionCount++;
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = 10;
                    Vector3 point = MapCamera.ScreenToWorldPoint(mousePos);
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(point.x, point.y));
                    prevMousePos = Input.mousePosition;
                } 
            }
        }
    }
}
