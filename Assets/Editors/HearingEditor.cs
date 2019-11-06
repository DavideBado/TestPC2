using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (HearingController))]
public class HearingEditor : Editor
{
    private void OnSceneGUI()
    {
        HearingController fow = (HearingController)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.HearingRadius);
    }
}
