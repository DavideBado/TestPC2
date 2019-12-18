using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class AddTypeButton : Button
{
    TextMeshProUGUI m_txt;
    int m_cellTypIndex = 0;
    Image m_image;
    TypeSelectorCell2D m_selectedCell;
    TypeSelector typeSelector;
    List<CellTypeBase> selectedCellTypes = new List<CellTypeBase>();
    protected override void Awake()
    {
        m_cellTypIndex = transform.GetSiblingIndex();
        typeSelector = GetComponentInParent<TypeSelector>();
        m_image = GetComponent<Image>();
        m_txt = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected override void OnEnable()
    {
        m_selectedCell = typeSelector.SelectedCell2D;
        selectedCellTypes = m_selectedCell.thisCell.data.graphics3D.GetComponent<Cell3D>().CellPsTypes;
        m_txt.text = selectedCellTypes[m_cellTypIndex].GetType().Name;

        if (selectedCellTypes[m_cellTypIndex].enabled) m_image.color = Color.blue;
        else m_image.color = Color.white;

    }

    protected override void OnDisable()
    {
        if (m_image) m_image.color = Color.white;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (selectedCellTypes[m_cellTypIndex].enabled) m_image.color = Color.blue + Color.red;
        else m_image.color = Color.red;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (selectedCellTypes[m_cellTypIndex].enabled) m_image.color = Color.blue;
        else m_image.color = Color.white;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        selectedCellTypes[m_cellTypIndex].enabled = !selectedCellTypes[m_cellTypIndex].enabled;
        if (selectedCellTypes[m_cellTypIndex].enabled)
        {
            m_selectedCell.thisCell.data.TypeID[m_cellTypIndex] = 1;
            m_image.color = Color.blue + Color.red;
        }
        else
        {
            m_image.color = Color.red;
            m_selectedCell.thisCell.data.TypeID[m_cellTypIndex] = 0;
        }
    }
}