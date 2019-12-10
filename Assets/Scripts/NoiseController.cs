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
    public NoiseDelegate MakeNoiseDelegate;
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
        MakeNoiseDelegate += MakeNoise;
        Reset += ResetNoise;
    }

    private void OnDisable()
    {
        MakeNoiseDelegate -= MakeNoise;
        Reset -= ResetNoise;
    }

    private void MakeNoise(float dimensionMod, float duration, NoiseType _type)
    {
        StopCoroutine("NoiseLife");
        Type = _type;
        NoiseArea.radius = noiseOriginalRadius;
        NoiseArea.enabled = true;
        //NoiseArea.radius += dimensionMod * Speed * Time.deltaTime;
        //StartCoroutine(NoiseUp(dimensionMod, duration));
        m_dimensionMod = dimensionMod;
        m_duration = duration;
        test = true;
        //if (NoiseArea.radius >= noiseOriginalRadius * dimensionMod)
        //{
        //    StartCoroutine("NoiseLife", duration);
        //}
    }
    bool test = false;
    float m_dimensionMod, m_duration;
    private void Update()
    {
        if(test)
        {
            NoiseArea.radius += m_dimensionMod * Speed * Time.deltaTime;
            if (NoiseArea.radius >= noiseOriginalRadius * m_dimensionMod)
            {
                StartCoroutine("NoiseLife", m_duration);
                test = false;
            }
        }
    }
    void ResetNoise()
    {
        StopCoroutine("NoiseLife");
        NoiseArea.radius = noiseOriginalRadius;
        Type = 0;
    }

    IEnumerator NoiseUp(float dimensionMod, float duration)
    {
        while (NoiseArea.radius < noiseOriginalRadius * dimensionMod)
        {
            NoiseArea.radius += dimensionMod * Speed * Time.deltaTime;
        }
        StartCoroutine("NoiseLife", duration);
        yield return 0;
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
