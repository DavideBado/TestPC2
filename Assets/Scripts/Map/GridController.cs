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


    // Start is called before the first frame update
    void Start()
    {
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
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
