using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseController : MonoBehaviour
{
    public SphereCollider NoiseArea;
    public float Speed;
    float noiseOriginalRadius;
    #region DelegatesDef
    public delegate void NoiseDelegate(float dimensionMod, float duration);
    #endregion

    #region Delegates
    public NoiseDelegate WalkingNoiseDelegate;
    #endregion

    private void Start()
    {
        noiseOriginalRadius = NoiseArea.radius;
    }
    private void OnEnable()
    {
        WalkingNoiseDelegate += MakeNoise;
    }

    private void OnDisable()
    {
        WalkingNoiseDelegate -= MakeNoise;
    }

    private void MakeNoise(float dimensionMod, float duration)
    {
        NoiseArea.radius = noiseOriginalRadius;
        NoiseArea.enabled = true;
        NoiseArea.radius += dimensionMod * Speed * Time.deltaTime;
        if (NoiseArea.radius == noiseOriginalRadius * dimensionMod)
        {
            StartCoroutine("NoiseLife", duration);
        }
    }

    IEnumerator NoiseLife(float duration)
    {
        yield return new WaitForSeconds(duration);
        NoiseArea.enabled = false;
    }
}
