using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTestPad : MonoBehaviour
{
    public Grid grid;
    public GameObject LinerendPrefab;
    public string XButton, RTrigger, Vertical, Horizontal;
    LineRenderer lineRenderer;
    Vector3 prevCellPos = new Vector3();
    float currentX;
    float currentY;
    // Start is called before the first frame update
    void Start()
    {
        currentX = 0;
        currentY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis(XButton) > 0)
        {
            GameObject newLinerendererObj = GameObject.Instantiate(LinerendPrefab, Vector3.zero, Quaternion.identity);
            lineRenderer = newLinerendererObj.GetComponent<LineRenderer>();
            lineRenderer.positionCount = 0;
        }
        if (Input.GetAxis(RTrigger) > 0 && (Input.GetAxis(Vertical) != 0 || Input.GetAxis(Horizontal) != 0))
        {
            currentX += Input.GetAxis(Horizontal);
            if(currentX > grid.GridDim_X)
            {
                currentX = grid.GridDim_X;
            }
            else if (currentX < 0)
            {
                currentX = 0;
            }

            currentY += Input.GetAxis(Vertical);
            if (currentY > grid.GridDim_Y)
            {
                currentY = grid.GridDim_Y;
            }
            else if (currentY < 0)
            {
                currentY = 0;
            }
          
            Cell2D currentCell = grid.ReturnCell((int)currentX, (int)currentY);
            Vector3 CellPos = currentCell.data.AnchoredPosition;
            if (prevCellPos != CellPos)
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, CellPos);
                prevCellPos = CellPos;
            }
        }
    }
}