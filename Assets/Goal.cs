using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(GameManager.instance.OnExePhase) if(collision.transform.GetComponent<PlayerMovController>()) GameManager.instance.PlayerGoal?.Invoke();
    }
}
