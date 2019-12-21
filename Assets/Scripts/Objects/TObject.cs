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
    public ParabolaGraphic Graphic;
    bool onUpgrade = false;
    bool onAir = false;
    public MeshRenderer MyRenderer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if(!onAir)
        {
            onAir = true;
            parabolaController.FollowParabola();
            NoiseController.Reset();
            MyCollider.enabled = true;
                MyRenderer.enabled = true;
            onUpgrade = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Player && other.transform.parent != Player.transform && !onUpgrade)
        {
            onUpgrade = true;
            onAir = false;
            Graphic.lineRenderer.enabled = false;
            MyRenderer.enabled = false;
            NoiseController.MakeNoiseDelegate(NoiseAreaMod, NoiseDuration, NoiseController.NoiseType.Object);
        }
    }
}
