using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditorCell : Button
{
    public int X, Y;
    Cell2D thisCell;
    public GridController gridController;
    Image m_image;

    protected override void Start()
    {
        thisCell = gameObject.AddComponent<Cell2D>();
        thisCell.data = new CellData
        {
            AnchoredPosition = gameObject.GetComponent<RectTransform>().anchoredPosition,
            X = X,
            Y = Y
        };
        thisCell.enabled = false;
        m_image = GetComponent<Image>();
    }
    // Start is called before the first frame update
    public override void OnPointerClick(PointerEventData eventData)
    {
        SelectTheCell(thisCell.enabled);
    }

    void SelectTheCell(bool _alreadySelected)
    {
        if (_alreadySelected)
        {
            gridController.SelectedCells[X].Remove(thisCell);
            thisCell.enabled = false;
            m_image.color = Color.white;
            GridController3D.gridController3D.ChangeCellColorDelegate(thisCell.data.X, thisCell.data.Y, Color.white);
            GridController3D.gridController3D.SelectCellDelegate(thisCell.data.X, thisCell.data.Y, false);
        }
        else
        {
            thisCell.enabled = true;
            gridController.SelectedCells[X].AddLast(thisCell);
            m_image.color = Color.blue;
            GridController3D.gridController3D.ChangeCellColorDelegate(thisCell.data.X, thisCell.data.Y, Color.blue);
            GridController3D.gridController3D.SelectCellDelegate(thisCell.data.X, thisCell.data.Y, true);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            SelectTheCell(thisCell.enabled);
        }
        else
        {
            m_image.color = Color.red;
            GridController3D.gridController3D.ChangeCellColorDelegate(thisCell.data.X, thisCell.data.Y, Color.red);
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!thisCell.enabled)
        {
            m_image.color = Color.white;
            GridController3D.gridController3D.ChangeCellColorDelegate(thisCell.data.X, thisCell.data.Y, Color.white);
        }
        else
        {
            m_image.color = Color.blue;
            GridController3D.gridController3D.ChangeCellColorDelegate(thisCell.data.X, thisCell.data.Y, Color.blue);
        }
    }
}
