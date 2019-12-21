using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PlayerMovController>() != null)
        {
            other.transform.GetComponent<PlayerMovController>().haveTheKey = true;
            gameObject.SetActive(false);
        }
    }
}
