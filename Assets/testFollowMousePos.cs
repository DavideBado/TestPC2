using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class testFollowMousePos : MonoBehaviour
{
    public Camera MapCamera;
    public Transform MapCanvas;
    public bool pezza = false;

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
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 point = MapCamera.ScreenToWorldPoint(mousePos);
        transform.position = point;
        List<Image> Images = GetComponentsInChildren<Image>().ToList();
       
        if(Images.Count > 0)
        {
            foreach (Image _image in Images)
            {
                _image.transform.position = transform.position;
            }
        }
            if (Input.GetMouseButton(0) && pezza)
            {
                pezza = false;
            }
       
        if (Input.GetMouseButtonUp(0) && !pezza)
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
                    if (Images.Count > 0)
                        foreach (Image _image in Images)
                        {
                            _image.transform.parent = MapCanvas;
                        }
                    pezza = true;
                } 
            }
        }
    }
}
