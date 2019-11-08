using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovController : MonoBehaviour
{
    public Rigidbody rb;
    public float currentSpeed;
    public float walkSpeed;
    public float crouchingSpeed;
    public float runningSpeed;

    public KeyCode interact;
    public KeyCode crouch;
    public KeyCode run;

    bool isCrouching = false;
    bool isRunning = false;
    bool isHiding = false;

    Vector3 lastPosition;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(moveHorizontal, 0.0f, moveVertical) * currentSpeed;

        Crouch();
        Run();
        DetectHidingPoint();
    }

    void Crouch()
    {
        if (Input.GetKeyDown(crouch) && isCrouching == false)
        {
            currentSpeed = crouchingSpeed;
            isCrouching = true;
        }
        else if (Input.GetKeyDown(crouch) && isCrouching == true)
        {
            currentSpeed = walkSpeed;
            isCrouching = false;
        }
    }

    void Run()
    {
        if (Input.GetKeyDown(run) && isRunning == false)
        {
            currentSpeed = runningSpeed;
            isRunning = true;
        }
        else if (Input.GetKeyDown(run) && isRunning == true)
        {
            currentSpeed = walkSpeed;
            isRunning = false;
        }
    }

    private void DetectHidingPoint()
    {
        if (Input.GetKeyDown(interact))
        {
            if (isHiding == false)
            {
                if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
                {
                    if (hit.collider.gameObject.tag == "HidingSpot")
                    {
                        lastPosition = transform.position;
                        transform.position = hit.transform.position;
                        isHiding = true;
                    }
                }
            }
            else if (isHiding == true)
            {
                transform.position = lastPosition;
                isHiding = false;
            }
        }
    }
}
