using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridData;

public class GridController : MonoBehaviour
{
    public EditorGrid_ConfigData EditorGridData;
    public GameplayGrid_ConfigData GameplayGridData;
    public GameObject Cell2DPrefab;
    public RectTransform MapSpace;

    public LinkedList<Cell>[] SelectedCells;

    // Start is called before the first frame update
    void Start()
    {
        SetupArrayCells();

        float XMod = MapSpace.sizeDelta.x / EditorGridData.HorizontalDim;
        float YMod = MapSpace.sizeDelta.y / EditorGridData.VerticalDim;

        for (int i = 0; i < EditorGridData.HorizontalDim; i++)
        {
            for (int j = 0; j < EditorGridData.VerticalDim; j++)
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
        SelectedCells = new LinkedList<Cell>[EditorGridData.HorizontalDim];

        for (int i = 0; i < SelectedCells.Length; i++)
        {
            SelectedCells[i] = new LinkedList<Cell>();
        }
    }

    public void SetupGameplayGrid()
    {
        SetupXLengthGamePlayGridCells();
        SetupYLengthGamePlayGridCells();
        FillGameplayGrid();
        DebugGameplayGrid();
    }
    void SetupXLengthGamePlayGridCells()
    {
        int X_Length = EditorGridData.HorizontalDim;

        for (int i = 0; i < SelectedCells.Length; i++)
        {
            if (SelectedCells[i].Count == 0)
            {
                X_Length--;
            }
        }
        GameplayGridData.Cells = new Cell[X_Length][];
    }

    void SetupYLengthGamePlayGridCells()
    {
        int index = 0;
        for (int i = 0; i < SelectedCells.Length; i++)
        {           
            if (SelectedCells[i].Count != 0)
            {
                GameplayGridData.Cells[index] = new Cell[SelectedCells[i].Count];
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
                LinkedListNode<Cell> linkedListNode = SelectedCells[i].First;
                for (int j = 0; j < SelectedCells[i].Count; j++)
                {
                    GameplayGridData.Cells[index][j] = linkedListNode.Value;
                    linkedListNode = linkedListNode.Next;
                }
                Array.Sort(GameplayGridData.Cells[index], (x, y) => x.Y.CompareTo(y.Y));
                index++;
            }
        }
    }

    void DebugGameplayGrid()
    {
        Debug.LogFormat("La matrice e' [{0}]", GameplayGridData.Cells.Length);

        for (int i = 0; i < GameplayGridData.Cells.Length; i++)
        {
            for (int j = 0; j < GameplayGridData.Cells[i].Length; j++)
            {
                Debug.LogFormat("La matrice a [{0}][{1}] e': Cella {2},{3}", i,j, GameplayGridData.Cells[i][j].GetComponent<Cell>().X, GameplayGridData.Cells[i][j].GetComponent<Cell>().Y);
            }
        }
    }
}