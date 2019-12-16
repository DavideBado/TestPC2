using System;
using System.Collections.Generic;
using UnityEngine;

public class TypeSelector : MonoBehaviour
{
    [HideInInspector]
    public TypeSelectorCell2D SelectedCell2D;

    public void CLose()
    {
        SelectedCell2D.m_image.color = Color.white;
        GridController3D.gridController3D.GameplayGridData.Cells[SelectedCell2D.thisCell.data.X][SelectedCell2D.thisCell.data.Y].graphics3D.GetComponent<MeshRenderer>().material.color = Color.white;
        gameObject.SetActive(false);
    }

    public void AddType(CellTypeBase _cellType)
    {
        SelectedCell2D.thisCell.data.graphics3D.AddComponent(_cellType.GetType());
    }
}
