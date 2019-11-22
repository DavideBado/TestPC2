using System;
using System.Collections;
using UnityEngine;

public class NoiseController : MonoBehaviour
{
    public SphereCollider NoiseArea;
    public float Speed;
    float noiseOriginalRadius;
    [HideInInspector]
    public NoiseType Type;
    #region DelegatesDef
    public delegate void NoiseDelegate(float dimensionMod, float duration, NoiseType _type);
    #endregion

    #region Delegates
    public NoiseDelegate WalkingNoiseDelegate;
    #endregion

    #region Actions
    public Action Reset;
    #endregion

    private void Start()
    {
        noiseOriginalRadius = NoiseArea.radius;
    }
    private void OnEnable()
    {
        WalkingNoiseDelegate += MakeNoise;
        Reset += ResetNoise;
    }

    private void OnDisable()
    {
        WalkingNoiseDelegate -= MakeNoise;
        Reset -= ResetNoise;
    }

    private void MakeNoise(float dimensionMod, float duration, NoiseType _type)
    {
        StopCoroutine("NoiseLife");
        Type = _type;
        NoiseArea.radius = noiseOriginalRadius;
        NoiseArea.enabled = true;
        NoiseArea.radius += dimensionMod * Speed * Time.deltaTime;
        if (NoiseArea.radius >= noiseOriginalRadius * dimensionMod)
        {
            StartCoroutine("NoiseLife", duration);
        }
    }

    void ResetNoise()
    {
        StopCoroutine("NoiseLife");
        NoiseArea.radius = noiseOriginalRadius;
        Type = 0;
    }

    IEnumerator NoiseLife(float duration)
    {
        yield return new WaitForSeconds(duration);
        NoiseArea.enabled = false;
    }

    public enum NoiseType
    {
        Undefined,
        Walk,
        Run,
        Object
    }
}
