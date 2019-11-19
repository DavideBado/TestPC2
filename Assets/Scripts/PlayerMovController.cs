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
    public float rotationSpeed;

    public KeyCode interact;
    public KeyCode crouch;
    public KeyCode run;

    Collision Wall;
    public LayerMask WallMask;

    bool isCrouching = false;
    bool isRunning = false;
    [HideInInspector]
    public bool isHiding = false;

    Vector3 lastPosition;

    public NoiseController Noise;
    public float walkDimensionMod;
    public float runDimensionMod;
    public float walkDuration;
    public float runDuration;

    public CameraMovement m_camera;
    //public GameObject forwardPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * currentSpeed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        /*if (Input.GetAxis("Vertical") != 0 && m_camera.currentCameraPos != m_camera.originCameraPos && m_camera.currentCameraRot != m_camera.originCameraRot)
        {
            rb.velocity = m_camera.transform.forward * currentSpeed;
            transform.LookAt(m_camera.transform.forward);
            forwardPoint.transform.position = m_camera.transform.forward;

        }*/

        if (translation > 0)
        {
            if (!Physics.Raycast(transform.position, transform.forward, 0.5f, WallMask)) transform.Translate(0, 0, translation);
        }
        else if (translation < 0) if (!Physics.Raycast(transform.position, -transform.forward, 0.5f, WallMask))
            {
                transform.Translate(0, 0, translation);
                Debug.DrawLine(transform.position, -transform.forward, Color.red, 1);
            }

        transform.Rotate(0, rotation, 0);

        Crouch();
        Run();
        DetectHidingPoint();

        if (currentSpeed == walkSpeed && Input.GetAxis("Vertical") != 0)
        {
            Noise.WalkingNoiseDelegate(walkDimensionMod, walkDuration, NoiseController.NoiseType.Walk);
        }
        if (currentSpeed == runningSpeed && Input.GetAxis("Vertical") != 0)
        {
            Noise.WalkingNoiseDelegate(runDimensionMod, runDuration, NoiseController.NoiseType.Run);
        }

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
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
                {
                    if (hit.collider.gameObject.tag == "HidingSpot")
                    {
                        lastPosition = transform.position;
                        transform.position = hit.transform.position;
                        isHiding = true;
                        currentSpeed = 0;
                    }
                }
            }
            else if (isHiding == true)
            {
                transform.position = lastPosition;
                isHiding = false;
                currentSpeed = walkSpeed;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == WallMask)
        {
            Wall = collision;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (Wall == collision)
        {
            Wall = null;
        }
    }
}
