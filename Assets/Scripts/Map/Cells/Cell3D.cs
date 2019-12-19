using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cell3D : MonoBehaviour
{
    public bool NotInEditor = false;
    public CellData data;
    public List<CellTypeBase> CellPsTypes = new List<CellTypeBase>();

    public void UpdateTypes()
    {
        if (data != null && data.TypeID != null)
        {
            for (int i = 0; i < data.TypeID.Length; i++)
            {
                if (data.TypeID[i] == 1)
                {
                    if (!NotInEditor)
                    {
                        CellPsTypes[i].enabled = true;
                    }
                        GetComponent<MeshRenderer>().enabled = true; 
                }
                else CellPsTypes[i].enabled = false;
            }
        }
    }

    void UpdateDataSpotTypes()
    {
        List<ISpotType> tempSpotTypes = GetComponents<ISpotType>().ToList();
        for (int i = 0; i < tempSpotTypes.Count; i++)
        {
            if(data.CellSpotTypes.Find(x => x == tempSpotTypes[i]) == null)
            {
                data.CellSpotTypes = tempSpotTypes;
                break;
            }
        }
        for (int i = 0; i < data.CellSpotTypes.Count; i++)
        {
            data.CellSpotTypes[i].SpotTransformDataIndex = i;
        }
    }
}
