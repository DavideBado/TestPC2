using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        for (int i = 0; i < fow.viewAngle.Count; i++)
        {
            AnglePreview(fow, fow.viewAngle[i]);
        }

        Handles.color = Color.red;

        if (fow.visibleTarget != null)
        {
            Handles.DrawLine(fow.transform.position, fow.visibleTarget.position); 
        }
    }

    private static void AnglePreview(FieldOfView _fow, float _viewAngle)
    {
        Vector3 viewAngleA = _fow.DirFromAngle(-_viewAngle / 2, false);
        Vector3 viewAngleB = _fow.DirFromAngle(_viewAngle / 2, false);

        Handles.DrawLine(_fow.transform.position, _fow.transform.position + viewAngleA * _fow.viewRadius);
        Handles.DrawLine(_fow.transform.position, _fow.transform.position + viewAngleB * _fow.viewRadius);
    }
}
