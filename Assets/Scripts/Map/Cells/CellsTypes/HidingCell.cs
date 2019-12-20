using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingCell : CellTypeBase, ISpotType
{
    public Cell3D m_Cell;
    public GameObject HidingSpotPref;
    [HideInInspector]
    public GameObject m_HidingSpot;
    public int m_SpotTransformDataIndex = 0;

    public int SpotTransformDataIndex { get { return m_SpotTransformDataIndex; } set { m_SpotTransformDataIndex = SpotTransformDataIndex; } }

    private void OnEnable()
    {
        if (m_HidingSpot == null)
        {
            if (transform.childCount > 0 && GetComponentInChildren<HidingSpot>() != null)
            {
                m_HidingSpot = GetComponentInChildren<HidingSpot>().gameObject;
            }
            else
            {
                m_HidingSpot = Instantiate(HidingSpotPref, transform);
            }
        }
        SpotSetup(m_HidingSpot, HidingSpotPref);
    }
    private void Update()
    {
        UpdateSpotData(m_HidingSpot);
    }
    void UpdateSpotData(GameObject _spot)
    {
        m_Cell.data.SpotTransformsPosition[SpotTransformDataIndex] = _spot.transform.position;
        m_Cell.data.SpotTransformsRotation[SpotTransformDataIndex] = _spot.transform.rotation;
        m_Cell.data.SpotTransformsScale[SpotTransformDataIndex] = _spot.transform.localScale;
    }

    private void SpotSetup(GameObject _spot, GameObject _spotPref)
    {
        if (_spot == null)
        {
            if (transform.childCount > 0 && GetComponentInChildren<KeySpot>() != null)
            {
                _spot = GetComponentInChildren<KeySpot>().gameObject;
            }
            else
            {
                _spot = Instantiate(_spotPref, transform);
            }
        }

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
