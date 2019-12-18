using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TypeSelectorCell2D : Button
{
    public GridController gridController;
    public Cell2D thisCell;
    public Image m_image;
    public bool selected = false;

    protected override void Start()
    {
        thisCell = gameObject.GetComponent<Cell2D>();
        thisCell.data.TypeID = new int[GridController3D.gridController3D.Cell3DPrefab.GetComponent<Cell3D>().CellPsTypes.Count];
        for (int i = 0; i < thisCell.data.TypeID.Length; i++)
        {
            thisCell.data.TypeID[i] = 0;
        }
        m_image = GetComponent<Image>();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        gridController.CellSelector2DPrefab.GetComponent<TypeSelector>().SelectedCell2D = this;
        gridController.CellSelector2DPrefab.SetActive(true);
        selected = true;
        m_image.color = Color.blue;
        GridController3D.gridController3D.GameplayGridData.Cells[thisCell.data.X][thisCell.data.Y].graphics3D.GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            gridController.CellSelector2DPrefab.GetComponent<TypeSelector>().SelectedCell2D = this;
            gridController.CellSelector2DPrefab.SetActive(true);
            selected = true;
            m_image.color = Color.blue;
            GridController3D.gridController3D.GameplayGridData.Cells[thisCell.data.X][thisCell.data.Y].graphics3D.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else
        {
            m_image.color = Color.red;
            GridController3D.gridController3D.GameplayGridData.Cells[thisCell.data.X][thisCell.data.Y].graphics3D.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!selected)
        {
            m_image.color = Color.white;
            GridController3D.gridController3D.GameplayGridData.Cells[thisCell.data.X][thisCell.data.Y].graphics3D.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        else
        {
            m_image.color = Color.blue;
            GridController3D.gridController3D.GameplayGridData.Cells[thisCell.data.X][thisCell.data.Y].graphics3D.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
}
