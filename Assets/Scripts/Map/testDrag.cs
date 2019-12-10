using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class testDrag : MonoBehaviour, IDragHandler 
{
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
}
