using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaGraphic : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public ParabolaController parabolaController;
    public GameObject ParabolaEndPoint;
    public LayerMask ObstacleLayer;
    Vector3 ParabolaDestDefaultPoint = new Vector3();
    bool x = false;

    private void Start()
    {
        ParabolaDestDefaultPoint = ParabolaEndPoint.transform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        DrawParabola(parabolaController.gizmo, parabolaController.ParabolaRoot);
        if(Input.GetMouseButtonUp(1)) lineRenderer.enabled = false;
    }

    void DrawParabola(ParabolaController.ParabolaFly gizmo, GameObject ParabolaRoot)
    {
        ParabolaEndPoint.transform.localPosition = ParabolaDestDefaultPoint;
        if (gizmo == null)
        {
            gizmo = new ParabolaController.ParabolaFly(ParabolaRoot.transform);
        }

        gizmo.RefreshTransforms(1f);
        if ((gizmo.Points.Length - 1) % 2 != 0)
            return;

        int accur = 50;
        Vector3 prevPos = gizmo.Points[0].position;
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 0;
        for (int c = 1; c <= accur; c++)
        {
            float currTime = c * gizmo.GetDuration() / accur;
            Vector3 currPos = gizmo.GetPositionAtTime(currTime);
            float mag = (currPos - prevPos).magnitude * 2;
            //Gizmos.color = new Color(mag, 0, 0, 1);
            //Gizmos.DrawLine(prevPos, currPos);
            //Gizmos.DrawSphere(currPos, 0.01f);
            lineRenderer.positionCount++;
            if(lineRenderer.positionCount != 0) lineRenderer.SetPosition(lineRenderer.positionCount -1, currPos);

            if (Physics.Raycast(prevPos, currPos, out RaycastHit _hit, Vector3.Distance(prevPos, currPos), ObstacleLayer)) ParabolaEndPoint.transform.position = _hit.point;
            else ParabolaEndPoint.transform.localPosition = ParabolaDestDefaultPoint;

            prevPos = currPos;
        }
        x = true;
    }
}
