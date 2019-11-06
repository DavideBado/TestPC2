using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingController : MonoBehaviour
{
    public float HearingRadius;

    public LayerMask targetMask;

    public EnemyNavController navController;

    private void Start()
    {
        StartCoroutine("FindTargetsWithDElay", .2f);
    }

    IEnumerator FindTargetsWithDElay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        Collider[] targetsInHearingRadius = Physics.OverlapSphere(transform.position, HearingRadius, targetMask);
        if (targetsInHearingRadius.Length > 0) navController.SoundTarget = targetsInHearingRadius[0].transform;
        else navController.SoundTarget = null;
    }
}

