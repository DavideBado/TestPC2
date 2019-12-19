using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridData;

public class GridController : MonoBehaviour
{
    public GameObject Cell2DPrefab;
    public GameObject CellSelector2DPrefab;
    public RectTransform MapSpace;
    public KeyCode UpdateGridData, Reset, Load;
    public LinkedList<Cell2D>[] SelectedCells;
    float XMod = 0;
    float YMod = 0;
    // Start is called before the first frame update
    void Start()
    {
        XMod = MapSpace.sizeDelta.x / GridController3D.gridController3D.EditorGridData.HorizontalDim;
        YMod = MapSpace.sizeDelta.y / GridController3D.gridController3D.EditorGridData.VerticalDim;
        GridController3D.gridController3D.gridController2D = this;
    }

    private void Update()
    {        
        if (Input.GetKeyDown(Reset)) CreateNewGrid();
        if (Input.GetKeyDown(UpdateGridData) && !GridController3D.gridController3D.JustLoaded) SetupGameplayGrid();
        else if (Input.GetKeyDown(UpdateGridData) && GridController3D.gridController3D.JustLoaded) GridController3D.gridController3D.SetupGameplayGridTypeSelected();
    }

    private void CreateNewGrid()
    {
        SetupArrayCells();


        for (int i = 0; i < GridController3D.gridController3D.EditorGridData.HorizontalDim; i++)
        {
            for (int j = 0; j < GridController3D.gridController3D.EditorGridData.VerticalDim; j++)
            {
                GameObject tempCell = Instantiate(Cell2DPrefab, MapSpace);
                RectTransform tempCellTransform = tempCell.GetComponent<RectTransform>();
                tempCell.SetActive(true);
                tempCellTransform.sizeDelta = new Vector2(XMod, YMod);
                tempCellTransform.anchoredPosition = new Vector2(XMod * i, YMod * j);
                EditorCell tempEditorCell = tempCell.AddComponent<EditorCell>();
                tempEditorCell.X = i;
                tempEditorCell.Y = j;
                tempEditorCell.gridController = this;
            }
        }
    }

    void SetupArrayCells()
    {
        if (SelectedCells != null)
        {
            int index = 0;
            for (int i = 0; i < SelectedCells.Length; i++)
            {
                if (SelectedCells[i].Count != 0)
                {
                    LinkedListNode<Cell2D> linkedListNode = SelectedCells[i].First;
                    for (int j = 0; j < SelectedCells[i].Count; j++)
                    {
                        Destroy(linkedListNode.Value.gameObject);
                        linkedListNode = linkedListNode.Next;
                    }
                    index++;
                }
            }
        }

        SelectedCells = new LinkedList<Cell2D>[GridController3D.gridController3D.EditorGridData.HorizontalDim];

        for (int i = 0; i < SelectedCells.Length; i++)
        {
            SelectedCells[i] = new LinkedList<Cell2D>();
        }
    }

    public void SetupGameplayGrid()
    {
        SetupXLengthGamePlayGridCells();
        SetupYLengthGamePlayGridCells();
        FillGameplayGrid();
        DebugGameplayGrid();
        GridController3D.gridController3D.SetupGameplayGrid();
    }
    void SetupXLengthGamePlayGridCells()
    {
        int X_Length = GridController3D.gridController3D.EditorGridData.HorizontalDim;

        for (int i = 0; i < SelectedCells.Length; i++)
        {
            if (SelectedCells[i].Count == 0)
            {
                X_Length--;
            }
        }
        GridController3D.gridController3D.GameplayGridData.Cells = new CellData[X_Length][];
    }

    void SetupYLengthGamePlayGridCells()
    {
        int index = 0;
        for (int i = 0; i < SelectedCells.Length; i++)
        {
            if (SelectedCells[i].Count != 0)
            {
                GridController3D.gridController3D.GameplayGridData.Cells[index] = new CellData[SelectedCells[i].Count];
                index++;
            }
        }
    }

    void FillGameplayGrid()
    {
        int index = 0;
        for (int i = 0; i < SelectedCells.Length; i++)
        {
            if (SelectedCells[i].Count != 0)
            {
                LinkedListNode<Cell2D> linkedListNode = SelectedCells[i].First;
                for (int j = 0; j < SelectedCells[i].Count; j++)
                {
                    GridController3D.gridController3D.GameplayGridData.Cells[index][j] = linkedListNode.Value.data;
                    linkedListNode = linkedListNode.Next;
                }
                Array.Sort(GridController3D.gridController3D.GameplayGridData.Cells[index], (x, y) => x.Y.CompareTo(y.Y));
                index++;
            }
        }
    }

    void DebugGameplayGrid()
    {
        GridController3D.gridController3D.GameplayGridData.X = GridController3D.gridController3D.GameplayGridData.Cells.Length;
        Debug.LogFormat("La matrice e' [{0}]", GridController3D.gridController3D.GameplayGridData.Cells.Length);

        for (int i = 0; i < GridController3D.gridController3D.GameplayGridData.Cells.Length; i++)
        {
            for (int j = 0; j < GridController3D.gridController3D.GameplayGridData.Cells[i].Length; j++)
            {
                Debug.LogFormat("La matrice a [{0}][{1}] e': Cella {2},{3}", i, j, GridController3D.gridController3D.GameplayGridData.Cells[i][j].X, GridController3D.gridController3D.GameplayGridData.Cells[i][j].Y);
            }
        }
    }

    public void LoadGrid()
    {
        if (GridController3D.gridController3D.GameplayGridData.Cells != null)
            for (int i = 0; i < GridController3D.gridController3D.GameplayGridData.Cells.Length; i++)
            {
                for (int j = 0; j < GridController3D.gridController3D.GameplayGridData.Cells[i].Length; j++)
                {
                    GameObject tempCell = Instantiate(Cell2DPrefab, MapSpace);
                    RectTransform tempCellTransform = tempCell.GetComponent<RectTransform>();
                    tempCell.SetActive(true);
                    tempCellTransform.sizeDelta = new Vector2(XMod, YMod);
                    tempCellTransform.anchoredPosition = GridController3D.gridController3D.GameplayGridData.Cells[i][j].AnchoredPosition;
                    Cell2D cellvalue = tempCell.AddComponent<Cell2D>();
                    cellvalue.data = GridController3D.gridController3D.GameplayGridData.Cells[i][j];
                    TypeSelectorCell2D typeSelector = tempCell.AddComponent<TypeSelectorCell2D>();
                    typeSelector.gridController = this;
                    GridController3D.gridController3D.LoadGrid(GridController3D.gridController3D.GameplayGridData.Cells[i][j], tempCellTransform.anchoredPosition.x / XMod, tempCellTransform.anchoredPosition.y / YMod);
                }
            }
        GridController3D.gridController3D.JustLoaded = true;
    }
}