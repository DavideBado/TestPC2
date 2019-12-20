using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridData;
using System.IO;

public class LevelDataLoader : MonoBehaviour
{
    public EditorGrid_ConfigData EditorGridData;
    public GameplayGrid_ConfigData GameplayGridData;
    public GameObject Cell3DPrefab;
    public Transform Z_MinBorder, Z_MaxBorder, X_MinBorder, X_MaxBorder;
    Vector3 gridOrigin, gridSize;
    float XMod3D = 0;
    float ZMod3D = 0;
    public GameObject Cell2DPrefab;
    public RectTransform MapSpace;
    float XMod2D = 0;
    float YMod2D = 0;

    public string LevelID;

    void Start()
    {
        gridOrigin = new Vector3(X_MinBorder.position.x, 0, Z_MinBorder.position.z);
        gridSize = new Vector3(Mathf.Abs(X_MaxBorder.position.x - X_MinBorder.position.x), 0, Mathf.Abs(Z_MaxBorder.position.z - Z_MinBorder.position.z));
        XMod3D = gridSize.x / EditorGridData.HorizontalDim;
        ZMod3D = gridSize.z / EditorGridData.VerticalDim;
        XMod2D = MapSpace.sizeDelta.x / EditorGridData.HorizontalDim;
        YMod2D = MapSpace.sizeDelta.y / EditorGridData.VerticalDim;

        LoadLevelGrid();
    }

    public void LoadLevelGrid()
    {
        LoadLevelData(LevelID);
        Load2DGrid();
    }

    public void LoadLevelData(string _levelID)
    {
        string path = null;
#if UNITY_EDITOR
        path = "Assets/Resources/GameJSONData/" + _levelID + ".json";
#endif
        //#if UNITY_STANDALONE
        //        // You cannot add a subfolder, at least it does not work for me
        //        path = "MyGame_Data/Resources/" + _levelID + ".json";
        //#endif
        
        string jsonLevelStringData = File.ReadAllText(path);

        LevelData level = JsonUtility.FromJson<LevelData>(jsonLevelStringData);

        level.ItemsToStringArray = JsonHelper.FromJson<string>(level.ArrayItemsToString);

        level.Items = new CellForSaveData[level.ItemsToStringArray.GetLength(0)][];
        for (int i = 0; i < level.ItemsToStringArray.GetLength(0); i++)
        {
            level.Items[i] = JsonHelper.FromJson<CellForSaveData>(level.ItemsToStringArray[i]);
        }


      GameplayGridData.Cells = new CellData[level.Items.GetLength(0)][];

        for (int i = 0; i < GameplayGridData.Cells.GetLength(0); i++)
        {
           GameplayGridData.Cells[i] = new CellData[level.Items[i].GetLength(0)];
        }

        for (int i = 0; i < GameplayGridData.Cells.GetLength(0); i++)
        {
            for (int j = 0; j < GameplayGridData.Cells[i].GetLength(0); j++)
            {
                GameplayGridData.Cells[i][j] = new CellData
                {
                    AnchoredPosition = level.Items[i][j].AnchoredPosition,
                    Position = level.Items[i][j].Position,
                    X = i,
                    Y = j,
                    TypeID = level.Items[i][j].TypeID,
                    SpotTransformsPosition = level.Items[i][j].SpotTransformsPosition,
                    SpotTransformsRotation = level.Items[i][j].SpotTransformsRotation,
                    SpotTransformsScale = level.Items[i][j].SpotTransformsScale,
                    CellSpotTypes = level.Items[i][j].CellSpotTypes
                };
            }
        }
    }

    public void Load3DGrid(CellData _cell, float XposMod, float ZPosMod)
    {
        GameObject tempCell = Instantiate(Cell3DPrefab, transform);
        Transform tempCellTransform = tempCell.GetComponent<Transform>();
        tempCell.SetActive(true);
        tempCellTransform.localScale = new Vector3((tempCellTransform.lossyScale.x / tempCellTransform.localScale.x) * XMod3D, 0.1f, (tempCellTransform.lossyScale.z / tempCellTransform.localScale.z) * ZMod3D);
        tempCellTransform.position = new Vector3((gridOrigin.x + XMod3D / 2) + XMod3D * XposMod, 0, (gridOrigin.z + ZMod3D / 2) + +ZMod3D * ZPosMod);
        Cell3D cellvalue = tempCell.GetComponent<Cell3D>();
        cellvalue.data = _cell;
        cellvalue.data.graphics3D = tempCell;
        cellvalue.GetComponent<MeshRenderer>().enabled = false;
        cellvalue.NotInEditor = true;
        cellvalue.UpdateTypes();
    }

    public void Load2DGrid()
    {
        if (GameplayGridData.Cells != null)
            for (int i = 0; i < GameplayGridData.Cells.Length; i++)
            {
                for (int j = 0; j < GameplayGridData.Cells[i].Length; j++)
                {
                    GameObject tempCell = Instantiate(Cell2DPrefab, MapSpace);
                    RectTransform tempCellTransform = tempCell.GetComponent<RectTransform>();
                    tempCell.SetActive(true);
                    tempCellTransform.sizeDelta = new Vector2(XMod2D, YMod2D);
                    tempCellTransform.anchoredPosition = GameplayGridData.Cells[i][j].AnchoredPosition;
                    Cell2D cellvalue = tempCell.AddComponent<Cell2D>();
                    cellvalue.data = GameplayGridData.Cells[i][j];
                    Load3DGrid(GameplayGridData.Cells[i][j], tempCellTransform.anchoredPosition.x / XMod2D, tempCellTransform.anchoredPosition.y / YMod2D);
                }
            }
    }
}
