using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TObject : MonoBehaviour
{
    public ParabolaController parabolaController;
    public NoiseController NoiseController;
    public Collider MyCollider;
    public float NoiseAreaMod, NoiseDuration;
    public GameObject Player;
    bool onUpgrade = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            parabolaController.FollowParabola();
            NoiseController.Reset();
            MyCollider.enabled = true;
            onUpgrade = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Player && other.transform.parent != Player.transform && !onUpgrade)
        {
            onUpgrade = true;
            NoiseController.MakeNoiseDelegate(NoiseAreaMod, NoiseDuration, NoiseController.NoiseType.Object);
        }
    }
}
