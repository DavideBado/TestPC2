using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCell : CellTypeBase, ISpotType
{
    public Cell3D m_Cell;
    public GameObject CameraSpotPref;
    [HideInInspector]
    public GameObject m_CameraSpot;
    public int m_SpotTransformDataIndex = 0;

    public int SpotTransformDataIndex { get { return m_SpotTransformDataIndex; } set { m_SpotTransformDataIndex = SpotTransformDataIndex; } }

    private void OnEnable()
    {
        if (m_CameraSpot == null)
        {
            if (transform.childCount > 0 && GetComponentInChildren<CamSpot>() != null)
            {
                m_CameraSpot = GetComponentInChildren<CamSpot>().gameObject;
            }
            else
            {
                m_CameraSpot = Instantiate(CameraSpotPref, transform);
                if (GameManager.instance != null)
                    if (GameManager.instance.Drone != null) GameManager.instance.Drone.camSpots.Add(m_CameraSpot.GetComponent<CamSpot>());
            }
        }
        SpotSetup(m_CameraSpot, CameraSpotPref);
    }
    private void Update()
    {
        UpdateSpotData(m_CameraSpot);
    }
    void UpdateSpotData(GameObject _spot)
    {
        m_Cell.data.SpotTransformsPosition[SpotTransformDataIndex] = _spot.transform.position;
        m_Cell.data.SpotTransformsRotation[SpotTransformDataIndex] = _spot.transform.rotation;
        m_Cell.data.SpotTransformsScale[SpotTransformDataIndex] = _spot.transform.localScale;
    }

    private void SpotSetup(GameObject _spot, GameObject _spotPref)
    {     
        if (m_Cell.data)
        {
            if (m_Cell.data.SpotTransformsPosition.Count > SpotTransformDataIndex && m_Cell.data.SpotTransformsPosition[SpotTransformDataIndex] != null)
            {
                _spot.transform.position = m_Cell.data.SpotTransformsPosition[SpotTransformDataIndex];
                _spot.transform.rotation = m_Cell.data.SpotTransformsRotation[SpotTransformDataIndex];
                _spot.transform.localScale = m_Cell.data.SpotTransformsScale[SpotTransformDataIndex];

            }
            else
            {
                SpotTransformDataIndex = m_Cell.data.SpotTransformsPosition.Count;
                m_Cell.data.SpotTransformsPosition.Add(_spot.transform.position);
                m_Cell.data.SpotTransformsRotation.Add(_spot.transform.rotation);
                m_Cell.data.SpotTransformsScale.Add(_spot.transform.localScale);
            }
        }
    }
}
