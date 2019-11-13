using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrap : MonoBehaviour
{
    public EnemyAI m_EnemyAI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovController>())
            if (m_EnemyAI) m_EnemyAI.AI_FSM.SetTrigger("ToPatrol");
    }
}
