using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<EnemyAI> EnemiesAI = new List<EnemyAI>();
    public GameObject Level;

    private void Start()
    {
        EnemiesAI = FindObjectsOfType<EnemyAI>().ToList();
    }
}
