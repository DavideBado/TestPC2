using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellData : ScriptableObject
{
    public Vector3 Position = new Vector3();
    public Vector3 AnchoredPosition = new Vector3();
    public int X, Y;
    public GameObject graphics3D;
}
