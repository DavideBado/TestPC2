using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridData
{
    [CreateAssetMenu(fileName = "NewGameplayGrid", menuName = "GridData/GamePlayGrid", order = 1)]
    public class GameplayGrid_ConfigData : ScriptableObject
    {
        public CellData[][] Cells;
        public int X;
    }
}