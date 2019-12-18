using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridData;

public class GridController3D : MonoBehaviour
{
    public static GridController3D gridController3D;
    public GridController gridController2D;

    public List<CellTypeBase> AllTypes = new List<CellTypeBase>();

    public EditorGrid_ConfigData EditorGridData;
    public GameplayGrid_ConfigData GameplayGridData;
    public GameObject Cell3DPrefab;
    public Transform Z_MinBorder, Z_MaxBorder, X_MinBorder, X_MaxBorder;
    Vector3 gridOrigin, gridSize;
    public Camera SetupCamera;
    public KeyCode Reset, Load;
    public bool JustLoaded = false;
    public EditorCell3D[,] Cells;
    public LinkedList<Cell3D>[] SelectedCells;
    public SaveMap saveMap;
    public string LevelID;
    float XMod = 0;
    float ZMod = 0;

    #region DelegateDef
    public delegate void CellColorDeleagete(int x, int y, Color color);
    public delegate void CellAddDeleagete(int x, int y, bool add);
    #endregion

    #region Delegates
    public CellColorDeleagete ChangeCellColorDelegate;
    public CellAddDeleagete SelectCellDelegate;
    #endregion

    private void OnEnable()
    {
        ChangeCellColorDelegate += Update3DCellColor;
        SelectCellDelegate += AddCell;
    }

    private void OnDisable()
    {
        ChangeCellColorDelegate -= Update3DCellColor;
        SelectCellDelegate -= AddCell;
    }

    private void Awake()
    {
        if (gridController3D != null) Destroy(gridController3D.gameObject);

        gridController3D = this;
        SetupCamera.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        gridOrigin = new Vector3(X_MinBorder.position.x, 0, Z_MinBorder.position.z);
        gridSize = new Vector3(Mathf.Abs(X_MaxBorder.position.x - X_MinBorder.position.x), 0, Mathf.Abs(Z_MaxBorder.position.z - Z_MinBorder.position.z));
        //transform.position = gridOrigin;
        //transform.localScale = gridSize;
        XMod = gridSize.x / EditorGridData.HorizontalDim;
        ZMod = gridSize.z / EditorGridData.VerticalDim;
    }

    private void Update()
    {
        if (Input.GetKeyDown(Reset)) CreateNewGrid(XMod, ZMod);
        if (Input.GetKeyDown(Load) && !JustLoaded)
        {
            saveMap.LoadLevel(LevelID);
            gridController2D.LoadGrid();
        }
    }

    private void CreateNewGrid(float XMod, float ZMod)
    {
        SetupArrayCells();
        SetupLinkArrayCells();
        for (int i = 0; i < EditorGridData.HorizontalDim; i++)
        {
            for (int j = 0; j < EditorGridData.VerticalDim; j++)
            {
                GameObject tempCell = Instantiate(Cell3DPrefab, transform);
                Transform tempCellTransform = tempCell.GetComponent<Transform>();
                tempCell.SetActive(true);
                tempCellTransform.localScale = new Vector3((tempCellTransform.lossyScale.x / tempCellTransform.localScale.x) * XMod, 0.1f, (tempCellTransform.lossyScale.z / tempCellTransform.localScale.z) * ZMod);
                tempCellTransform.position = new Vector3((gridOrigin.x + XMod / 2) + XMod * i, 0, (gridOrigin.z + ZMod / 2) + +ZMod * j);
                EditorCell3D tempEditorCell = tempCell.AddComponent<EditorCell3D>();
                tempEditorCell.X = i;
                tempEditorCell.Y = j;
                Cells[i, j] = tempEditorCell;
            }
        }
    }

    void SetupArrayCells()
    {
        if (Cells != null)
        {
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    Destroy(Cells[i, j].gameObject);
                }
            }
        }
        Cells = new EditorCell3D[EditorGridData.HorizontalDim, EditorGridData.VerticalDim];
    }

    void SetupLinkArrayCells()
    {
        if (SelectedCells != null)
        {
            int index = 0;
            for (int i = 0; i < SelectedCells.Length; i++)
            {
                if (SelectedCells[i].Count != 0)
                {
                    LinkedListNode<Cell3D> linkedListNode = SelectedCells[i].First;
                    for (int j = 0; j < SelectedCells[i].Count; j++)
                    {
                        Destroy(linkedListNode.Value.gameObject);
                        linkedListNode = linkedListNode.Next;
                    }
                    index++;
                }
            }
        }

        SelectedCells = new LinkedList<Cell3D>[EditorGridData.HorizontalDim];

        for (int i = 0; i < SelectedCells.Length; i++)
        {
            SelectedCells[i] = new LinkedList<Cell3D>();
        }
    }

    void Update3DCellColor(int x, int y, Color _color)
    {
        if (Cells[x, y])
        {

            Cells[x, y].gameObject.GetComponent<MeshRenderer>().material.color = _color;
        }
    }

    void AddCell(int x, int y, bool add)
    {
        if (Cells[x, y])
        {
            if (add) SelectedCells[x].AddLast(Cells[x, y].thisCell);
            else SelectedCells[x].Remove(Cells[x, y].thisCell);
        }
    }

    public void SetupGameplayGrid()
    {
        //SetupXLengthGamePlayGridCells();
        //SetupYLengthGamePlayGridCells();
        FillGameplayGrid();
        DebugGameplayGrid();
        saveMap.SaveLevel(LevelID);
    }
    public void SetupGameplayGridTypeSelected()
    {
        saveMap.SaveLevel(LevelID);
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
        GameplayGridData.Cells = new CellData[X_Length][];
    }

    void SetupYLengthGamePlayGridCells()
    {
        int index = 0;
        for (int i = 0; i < SelectedCells.Length; i++)
        {
            if (SelectedCells[i].Count != 0)
            {
                GameplayGridData.Cells[index] = new CellData[SelectedCells[i].Count];
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
                LinkedListNode<Cell3D> linkedListNode = SelectedCells[i].First;
                for (int j = 0; j < SelectedCells[i].Count; j++)
                {
                    GameplayGridData.Cells[index][j].Position = linkedListNode.Value.data.Position;
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
                Debug.LogFormat("La matrice a [{0}][{1}] e': Cella {2},{3}", i, j, GameplayGridData.Cells[i][j].X, GameplayGridData.Cells[i][j].Y);
            }
        }
    }
    
    public void LoadGrid(CellData _cell, float XposMod, float ZPosMod)
    {
        GameObject tempCell = Instantiate(Cell3DPrefab, transform);
        Transform tempCellTransform = tempCell.GetComponent<Transform>();
        tempCell.SetActive(true);
        tempCellTransform.localScale = new Vector3((tempCellTransform.lossyScale.x / tempCellTransform.localScale.x) * XMod, 0.1f, (tempCellTransform.lossyScale.z / tempCellTransform.localScale.z) * ZMod);
        //tempCellTransform.position = _cell.Position;
        tempCellTransform.position = new Vector3((gridOrigin.x + XMod / 2) + XMod * XposMod, 0, (gridOrigin.z + ZMod / 2) + +ZMod * ZPosMod);
        Cell3D cellvalue = tempCell.GetComponent<Cell3D>();
        cellvalue.data = _cell;
        cellvalue.data.graphics3D = tempCell;
        cellvalue.UpdateTypes();
    }
}