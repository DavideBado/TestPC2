using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphicsController : MonoBehaviour
{
    public PlayerMovController PlayerController;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", PlayerController.GraphSpeed);
        if(PlayerController.isCrouching) animator.SetFloat("Crouch", 5f);
        else animator.SetFloat("Crouch", 0);
    }
}
