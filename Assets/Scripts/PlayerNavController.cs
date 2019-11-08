using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavController : MonoBehaviour
{
    public NavMeshAgent agent;

    private void UpdateTargets(Vector3 targetPosition) // Aggiorna la destinazione dei cultisti
    {
        agent.destination = targetPosition; // Imposta la destinazione
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Se è stato premuto il pulsante sinistro del mouse
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Fai partire un raggio dalla posizione del mouse
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit)) // Se entra in collisione con un oggetto
            {
                Vector3 targetPosition = hit.point; // La destinazione è il punto di collisione
                UpdateTargets(targetPosition); // Aggiorna la destinazione
            }
        }
    }
}
