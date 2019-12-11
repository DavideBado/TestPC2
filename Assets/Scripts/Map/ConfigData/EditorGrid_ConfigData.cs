using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridData
{
    [CreateAssetMenu(fileName = "NewEditorGrid", menuName = "GridData/EditorGrid", order = 0)]
    public class EditorGrid_ConfigData : ScriptableObject
    {
        public int HorizontalDim, VerticalDim;
    }
}