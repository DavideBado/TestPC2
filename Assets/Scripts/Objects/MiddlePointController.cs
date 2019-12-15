using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddlePointController : MonoBehaviour
{
          public Transform PointA, PointB, PointC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PointB.transform.position = new Vector3((PointA.transform.position.x + PointC.transform.position.x) / 2, PointB.transform.position.y, (PointA.transform.position.z + PointC.transform.position.z) / 2);
}
}
