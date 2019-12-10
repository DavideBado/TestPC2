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
            FindTargets();
        }
    }

    void FindTargets()
    {
        Collider[] targetsInHearingRadius = Physics.OverlapSphere(transform.position, HearingRadius, targetMask);
        if (targetsInHearingRadius.Length > 0) SaveNoise(targetsInHearingRadius[0].transform);
        else SaveNoise(null);
    }

    void SaveNoise(Transform _Target)
    {
        navController.NoiseTarget = _Target;
        navController.prevNoiseType = navController.currentNoiseType;
        if (_Target)
        {
            navController.currentNoiseType = _Target.GetComponent<NoiseController>().Type;
        }
        else
        {
            navController.currentNoiseType = 0;
        }
    }
}



