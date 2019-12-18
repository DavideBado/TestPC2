using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellData : ScriptableObject
{
    public Vector3 Position = new Vector3();
    public Vector3 AnchoredPosition = new Vector3();
    public int X, Y;
    public GameObject graphics3D;
    public int[] TypeID;
    
    public List<Vector3> SpotTransformsPosition = new List<Vector3>();
    public List<Quaternion> SpotTransformsRotation = new List<Quaternion>();
    public List<Vector3> SpotTransformsScale = new List<Vector3>();
    public List<ISpotType> CellSpotTypes = new List<ISpotType>();
}
