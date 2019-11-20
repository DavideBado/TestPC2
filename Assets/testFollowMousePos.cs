using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class testFollowMousePos : MonoBehaviour
{
    public Camera MapCamera;
    public Transform MapCanvas;
    public bool pezza = false;
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
        List<Transform> Images = GetComponentsInChildren<Transform>().ToList();
       
            if (Input.GetMouseButton(0) && pezza)
            {
                pezza = false;
            }
       
        if (Input.GetMouseButtonUp(0) && !pezza)
        {
            if (Images.Count > 0)
                foreach (Transform _image in Images)
            {
                _image.parent = MapCanvas;
            }
            pezza = true;
        }
    }
}
