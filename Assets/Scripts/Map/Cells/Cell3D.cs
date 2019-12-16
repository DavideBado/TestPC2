using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell3D : MonoBehaviour
{
    public CellData data;
    // Start is called before the first frame update
    void Start()
    {
        if (data.TypeID != null) for (int i = 0; i < data.TypeID.Length; i++) if (data.TypeID[i] == 1) data.graphics3D.AddComponent(GridController3D.gridController3D.AllTypes[i].GetType());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
