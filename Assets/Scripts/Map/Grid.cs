using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject CellPref;
    public int GridDim_X, GridDim_Y;
    private float Snap_X, Snap_Y;
    Cell2D[,] Cells;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Init()
    {
        Cells = new Cell2D[GridDim_X, GridDim_Y];
        Snap_X = Screen.width / GridDim_X;
        Snap_Y = Screen.height / GridDim_Y;
        for (int i = 0; i < GridDim_Y; i++)
        {
            for (int j = 0; j < GridDim_X; j++)
            {
               GameObject _cell = Instantiate(CellPref, Camera.main.ScreenToWorldPoint(new Vector3(i * Snap_X, j * Snap_Y, 10)), Quaternion.identity);
                Cells[i,j] = _cell.GetComponent<Cell2D>();
                Cells[i, j].data.AnchoredPosition = _cell.transform.position;
            }
        }
    }

    public Cell2D ReturnCell(int _x, int _y)
    {
        return Cells[_x, _y];
    }
}
