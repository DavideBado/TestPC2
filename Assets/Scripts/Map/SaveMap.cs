using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SaveMap : MonoBehaviour
{
    public const string LEVELSAVES = "LevelSaves";

    public string LevelToLoad;
    
    public List<string> LevelsID = new List<string>();

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        string jsonStringLevelSaves = PlayerPrefs.GetString(LEVELSAVES);
        LevelSaves levelSaves = JsonUtility.FromJson<LevelSaves>(jsonStringLevelSaves);
        LevelsID = levelSaves.LevelsID;
    }

    void RemoveSavedLevel(string _levelName)
    {
        string jsonToLoad = PlayerPrefs.GetString("Data");
        //Load as Array
        LevelData[] _tempLoadListData = JsonHelper.FromJson<LevelData>(jsonToLoad);
        //Convert to List
        List<LevelData> loadListData = _tempLoadListData.OfType<LevelData>().ToList();

        loadListData.RemoveAll((x) => x.ID == _levelName);
        
        string jsonToSave = JsonHelper.ToJson(loadListData.ToArray());
        PlayerPrefs.SetString("Data", jsonToSave);
        PlayerPrefs.Save();
    }
    public void SaveLevel(string _levelID)
    {
        //RemoveSavedLevel(_levelID);
        LevelData saveClass = new LevelData();

        saveClass.ID = _levelID;
        LevelsID.Add(saveClass.ID);
        Debug.LogFormat("{0} saved!", saveClass.ID);
        saveClass.Items = new CellForSaveData[GridController3D.gridController3D.GameplayGridData.Cells.GetLength(0)][];

        for (int i = 0; i < GridController3D.gridController3D.GameplayGridData.Cells.GetLength(0); i++)
        {
            saveClass.Items[i] = new CellForSaveData[GridController3D.gridController3D.GameplayGridData.Cells[i].GetLength(0)];
        }

        for (int i = 0; i < saveClass.Items.GetLength(0); i++)
        {
            for (int j = 0; j < saveClass.Items[i].GetLength(0); j++)
            {
                saveClass.Items[i][j] = new CellForSaveData
                {
                    AnchoredPosition = GridController3D.gridController3D.GameplayGridData.Cells[i][j].AnchoredPosition,
                    Position = GridController3D.gridController3D.GameplayGridData.Cells[i][j].Position,
                    X = GridController3D.gridController3D.GameplayGridData.Cells[i][j].X,
                    Y = GridController3D.gridController3D.GameplayGridData.Cells[i][j].Y
                };
            }
        }
        saveClass.ItemsToStringArray = new string[saveClass.Items.GetLength(0)];
        for (int i = 0; i < saveClass.Items.GetLength(0); i++)
        {
            saveClass.ItemsToStringArray[i] = JsonHelper.ToJson<CellForSaveData>(saveClass.Items[i]); 
        }

        saveClass.ArrayItemsToString = JsonHelper.ToJson<string>(saveClass.ItemsToStringArray);

        // Salvo livello
        string jsonString = JsonUtility.ToJson(saveClass);
        PlayerPrefs.SetString(saveClass.ID, jsonString);
        // Salvo il nome del livello nella lista dei livelli
        LevelSaves levelSaves = new LevelSaves();
        levelSaves.LevelsID = LevelsID;
        string jsonStringForLevelID = JsonUtility.ToJson(levelSaves);
        PlayerPrefs.SetString(LEVELSAVES, jsonStringForLevelID);
        PlayerPrefs.Save();
    }

    public void LoadLevel(string _levelID)
    {
        string jsonLevelStringData = PlayerPrefs.GetString(_levelID);

        LevelData level = JsonUtility.FromJson<LevelData>(jsonLevelStringData);

        level.ItemsToStringArray = JsonHelper.FromJson<string>(level.ArrayItemsToString);

        level.Items = new CellForSaveData[level.ItemsToStringArray.GetLength(0)][];
        for (int i = 0; i < level.ItemsToStringArray.GetLength(0); i++)
        {
            level.Items[i] = JsonHelper.FromJson<CellForSaveData>(level.ItemsToStringArray[i]);
        }


        GridController3D.gridController3D.GameplayGridData.Cells = new CellData[level.Items.GetLength(0)][];

        for (int i = 0; i < GridController3D.gridController3D.GameplayGridData.Cells.GetLength(0); i++)
        {
            GridController3D.gridController3D.GameplayGridData.Cells[i] = new CellData[level.Items[i].GetLength(0)];
        }

        for (int i = 0; i < GridController3D.gridController3D.GameplayGridData.Cells.GetLength(0); i++)
        {
            for (int j = 0; j < GridController3D.gridController3D.GameplayGridData.Cells[i].GetLength(0); j++)
            {
                GridController3D.gridController3D.GameplayGridData.Cells[i][j] = new CellData
                {
                    AnchoredPosition = level.Items[i][j].AnchoredPosition,
                    Position = level.Items[i][j].Position,
                    X = i,
                    Y = j
                };
            }
        }
    }
}

[Serializable]
public class LevelData
{
    public string ID;
    public CellForSaveData[][] Items;
    public string[] ItemsToStringArray = new string[] {};
    public string ArrayItemsToString = "";
}

[Serializable]
public class CellForSaveData
{
    public Vector3 Position = new Vector3();
    public Vector3 AnchoredPosition = new Vector3();
    public int X, Y;
}

public class LevelSaves
{
    public List<string> LevelsID;
}