using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditorCell : Button
{
    public int X, Y;
    Cell thisCell;
    public GridController gridController;
    Image m_image;

    protected override void Start()
    {
        thisCell = gameObject.AddComponent<Cell>();
        thisCell.AnchoredPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
        thisCell.X = X;
        thisCell.Y = Y;
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
        if(_alreadySelected)
        {
            gridController.SelectedCells[X].Remove(thisCell);
            thisCell.enabled = false;
            m_image.color = Color.white;
        }
        else
        {
            thisCell.enabled = true;
            gridController.SelectedCells[X].AddLast(thisCell);
            m_image.color = Color.blue;
        }
    }
}
