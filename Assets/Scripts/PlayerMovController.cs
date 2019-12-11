using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [HideInInspector]
    public bool isCrouching = false;
    bool isRunning = false;
    [HideInInspector]
    public bool isHiding = false;

    Vector3 lastPosition;

    public NoiseController Noise;
    public float walkDimensionMod;
    public float runDimensionMod;
    public float walkDuration;
    public float runDuration;

    [HideInInspector]
    public float GraphSpeed;
    public CameraMovement m_camera;

    public Vector3 ResetPosition;

    bool InputActive = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputActive)
        {
            float translation = Input.GetAxis("Vertical") * currentSpeed;
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

            GraphSpeed = translation;

            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;

            MoveToCameraForward();

            if (translation > 0)
            {
                if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), transform.forward, 0.5f, WallMask)) transform.Translate(0, 0, translation);
            }
            else if (translation < 0) if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), -transform.forward, 0.5f, WallMask))
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
                Noise.MakeNoiseDelegate(walkDimensionMod, walkDuration, NoiseController.NoiseType.Walk);
            }
            if (currentSpeed == runningSpeed && Input.GetAxis("Vertical") != 0)
            {
                Noise.MakeNoiseDelegate(runDimensionMod, runDuration, NoiseController.NoiseType.Run);
            } 
        }

    }
    void Crouch()
    {
        if (Input.GetKeyDown(crouch) && isCrouching == false && isHiding == false)
        {
            currentSpeed = crouchingSpeed;
            isCrouching = true;
        }
        else if (Input.GetKeyDown(crouch) && isCrouching == true && isHiding == false)
        {
            currentSpeed = walkSpeed;
            isCrouching = false;
        }
    }

    void Run()
    {
        if (Input.GetKeyDown(run) && isRunning == false && isHiding == false)
        {
            currentSpeed = runningSpeed;
            isCrouching = false;
            isRunning = true;
        }
        else if (Input.GetKeyDown(run) && isRunning == true && isHiding == false)
        {
            currentSpeed = walkSpeed;
            isCrouching = false;
            isRunning = false;
        }
    }

    float pezzahidingSpeed = 0;
    public MenuSelector MenuSelector;
    private void DetectHidingPoint()
    {
        if (Input.GetKeyDown(interact) && !MenuSelector.InMapView)
        {
            if (isHiding == false)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 1.5f))
                {
                    if (hit.collider.gameObject.tag == "HidingSpot")
                    {
                        PezzaLampoHidingPoint(false);
                        lastPosition = transform.position;
                        transform.position = hit.transform.position;
                        isHiding = true;
                        pezzahidingSpeed = currentSpeed;
                        currentSpeed = 0;
                    }
                }
            }
            else if (isHiding == true)
            {
                transform.position = lastPosition;
                PezzaLampoHidingPoint(true);
                isHiding = false;
                currentSpeed = pezzahidingSpeed;
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

    public GameObject Graphics;
    public CapsuleCollider Collider;
    public NavMeshObstacle ObstacleNav;


    public void PezzaLampoHidingPoint(bool x)
    {
    //    rb.useGravity = x;
        Graphics.SetActive(x);
        Collider.enabled = x;
        if(GameManager.instance.OnExePhase) ObstacleNav.enabled = x;
    }

    public void TurnOnOffThePlayer(bool x)
    {
        Graphics.SetActive(x);
        InputActive = x;
    }

    public GameObject debugSphere;
    public void MoveToCameraForward()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            transform.LookAt(m_camera.point);
            var rot = transform.rotation;
            rot.x -= rot.x;
            rot.z -= rot.z;
            transform.rotation = rot;
        }
    }
}