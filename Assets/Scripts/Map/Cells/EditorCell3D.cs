using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EditorCell3D : MonoBehaviour
{
    public int X, Y;
    public Cell3D thisCell;

    void Start()
    {
        thisCell = gameObject.GetComponent<Cell3D>();
        thisCell.data = new CellData
        {
            Position = gameObject.GetComponent<Transform>().position,
            X = X,
            Y = Y
        };
        thisCell.enabled = false;
    }
}
