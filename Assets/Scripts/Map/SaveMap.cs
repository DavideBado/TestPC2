using System;
using System.Collections.Generic;
using System.IO;
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
                int[] tempTypeID = CellTypeIDCheck(GridController3D.gridController3D.GameplayGridData.Cells[i][j]);
                saveClass.Items[i][j] = new CellForSaveData
                {
                    AnchoredPosition = GridController3D.gridController3D.GameplayGridData.Cells[i][j].AnchoredPosition,
                    Position = GridController3D.gridController3D.GameplayGridData.Cells[i][j].Position,
                    X = GridController3D.gridController3D.GameplayGridData.Cells[i][j].X,
                    Y = GridController3D.gridController3D.GameplayGridData.Cells[i][j].Y,
                    TypeID = tempTypeID,
                    SpotTransformsPosition = GridController3D.gridController3D.GameplayGridData.Cells[i][j].SpotTransformsPosition,
                    SpotTransformsRotation = GridController3D.gridController3D.GameplayGridData.Cells[i][j].SpotTransformsRotation,
                    SpotTransformsScale = GridController3D.gridController3D.GameplayGridData.Cells[i][j].SpotTransformsScale,
                    CellSpotTypes = GridController3D.gridController3D.GameplayGridData.Cells[i][j].CellSpotTypes
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
        //PlayerPrefs.SetString(saveClass.ID, jsonString);
        // Salvo il nome del livello nella lista dei livelli
        LevelSaves levelSaves = new LevelSaves();
        levelSaves.LevelsID = LevelsID;
        string jsonStringForLevelID = JsonUtility.ToJson(levelSaves);
        //PlayerPrefs.SetString(LEVELSAVES, jsonStringForLevelID);
        //PlayerPrefs.Save();
        SaveItemInfo(jsonString, _levelID);
    }

    public void LoadLevel(string _levelID)
    {
        string path = null;
#if UNITY_EDITOR
        path = "Assets/Resources/GameJSONData/" + _levelID + ".json";
#endif
//#if UNITY_STANDALONE
//        // You cannot add a subfolder, at least it does not work for me
//        path = "MyGame_Data/Resources/" + _levelID + ".json";
//#endif
        //Load text from a JSON file (Assets/Resources/Text/jsonFile01.json)
        //var jsonTextFile = Resources.Load<TextAsset>(path);
        //string jsonLevelStringData = /*PlayerPrefs.GetString(_levelID)*/ jsonTextFile.text;
        string jsonLevelStringData = File.ReadAllText(path);

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

    int[] CellTypeIDCheck(CellData _cell)
    {
        if (_cell.TypeID != null) return _cell.TypeID;
        else return new int[1] { 0 };
    }

    public void SaveItemInfo(string _stringData, string _levelID)
    {
        string path = null;
#if UNITY_EDITOR
        path = "Assets/Resources/GameJSONData/" + _levelID + ".json";
#endif
//#if UNITY_STANDALONE
//        // You cannot add a subfolder, at least it does not work for me
//        path = "MyGame_Data/Resources/" + _levelID + ".json";
//#endif

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(_stringData);
            }
        }
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
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
    public int[] TypeID = new int[1] { 0 };
    public List<Vector3> SpotTransformsPosition = new List<Vector3>();
    public List<Quaternion> SpotTransformsRotation = new List<Quaternion>();
    public List<Vector3> SpotTransformsScale = new List<Vector3>();
    public List<ISpotType> CellSpotTypes = new List<ISpotType>();
}

public class LevelSaves
{
    public List<string> LevelsID;
}