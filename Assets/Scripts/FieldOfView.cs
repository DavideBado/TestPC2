using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public List<float> viewAngle = new List<float>();

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public Transform visibleTarget;

    public float meshResolution;
    public int edgeResolveIteration;
    public float edgeDstThreshold;

    public List<MeshFilter> viewMeshFilters = new List<MeshFilter>();
    List<Mesh> viewMeshes = new List<Mesh>();

    public EnemyNavController navController;

    private void Start()
    {
        for (int i = 0; i < viewMeshFilters.Count; i++)
        { 
            Mesh mesh = new Mesh
            {
                name = "View Mesh_" + i
            };
            viewMeshes.Add(mesh);
            viewMeshFilters[i].mesh = viewMeshes[i];
        }

        StartCoroutine("FindTargetsWithDElay", .2f);
    }

    IEnumerator FindTargetsWithDElay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            for (int i = 0; i < viewAngle.Count; i++)
            {
                FindVisibleTargets(i);
            }            
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < viewAngle.Count; i++)
        {
            DrawFieldOfView(i, i);
        }
    }
    void FindVisibleTargets(int _viewAngleIndex)
    {
        visibleTarget = null;
        int targetArea = -1;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle[_viewAngleIndex] / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTarget = target;                    
                }
            }
        }
        navController.VisibleTarget = visibleTarget;
        if (visibleTarget)
        {
            float angle = Vector3.Angle(transform.forward, (visibleTarget.position - transform.position).normalized);
            for (int i = 0; i < viewAngle.Count; i++)
            {
                if(angle <= viewAngle[i] / 2)
                {
                    targetArea = i;
                    break;
                }
            }
        }
        navController.visibleTargetArea = targetArea;
    }

    void DrawFieldOfView(int _viewAngleIdex, int _meshIndex)
    {
        int stepCount = Mathf.RoundToInt(viewAngle[_viewAngleIdex] * meshResolution);
        float stepAngleSize = viewAngle[_viewAngleIdex] / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i < stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle[_viewAngleIdex] / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if(i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                if(oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2; 
            }
        }

        viewMeshes[_meshIndex].Clear();
        viewMeshes[_meshIndex].vertices = vertices;
        viewMeshes[_meshIndex].triangles = triangles;
        viewMeshes[_meshIndex].RecalculateNormals();
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = minViewCast.point;
        Vector3 maxPoint = maxViewCast.point;

        for (int i = 0; i < edgeResolveIteration; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if(Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}

