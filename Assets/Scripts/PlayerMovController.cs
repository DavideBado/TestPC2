using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Cinemachine;
using System.Linq;

public class PlayerMovController : MonoBehaviour
{
    int currentSpotCameraIndex = 0;
    public List<CamSpot> camSpots = new List<CamSpot>();
    public Camera SpotCamera;
    public RawImage SpotCameraScreen;
    public Rigidbody rb;
    public float currentSpeed;
    public float walkSpeed;
    public float crouchingSpeed;
    public float runningSpeed;
    public float rotationSpeed;

    public CinemachineFreeLook freeLookCamera;

    public KeyCode interact;
    public KeyCode crouch;
    public KeyCode run;

    public KeyCode NextSpotCam;
    public KeyCode PrevSpotCam;

    Collision Wall;
    public LayerMask WallMask;
    [HideInInspector]
    public bool isCrouching = false;
    bool isRunning = false;
    [HideInInspector]
    public bool isHiding = false;

    public bool haveTheKey = false;

    //List<Gate> gates = new List<Gate>();
    public string OpenTheGateTrigger;

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
        ChangeCamSpot(0);
        //gates = FindObjectsOfType<Gate>().ToList();
        if (camSpots.Count == 0) SpotCameraScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (camSpots.Count == 0) SpotCameraScreen.enabled = false;
        if (InputActive)
        {
            float translationVertical = Input.GetAxis("Vertical") * currentSpeed;
            float HorizontalTranslation = Input.GetAxis("Horizontal") * currentSpeed;
      
            GraphSpeed = translationVertical;

            translationVertical *= Time.deltaTime;
            HorizontalTranslation *= Time.deltaTime;

            MoveToCameraForward();

            if (translationVertical > 0)
            {
                if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), transform.forward, 0.5f, WallMask)) transform.Translate(0, 0, translationVertical);
            }
            else if (translationVertical < 0) if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), -transform.forward, 0.5f, WallMask))
                {
                    transform.Translate(0, 0, translationVertical);
                    Debug.DrawLine(transform.position, -transform.forward, Color.red, 1);
                }

            if (HorizontalTranslation > 0)
            {
                if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), transform.right, 0.5f, WallMask)) transform.Translate(HorizontalTranslation, 0, 0);
            }
            else if (HorizontalTranslation < 0) if (!Physics.Raycast(new Vector3(transform.position.x, 0.2f, transform.position.z), -transform.right, 0.5f, WallMask))
                {
                    transform.Translate(HorizontalTranslation, 0, 0);
                    Debug.DrawLine(transform.position, -transform.forward, Color.red, 1);
                }
            
            
            //transform.Rotate(0, rotation, 0);

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

            if(Input.GetKeyDown(interact) && haveTheKey)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 1.5f))
                {
                    if (hit.transform.GetComponent<Gate>() != null)
                    {
                        //for (int i = 0; i < gates.Count; i++)
                        //{
                        //    gates[i].GetComponent<Animator>().SetTrigger(OpenTheGateTrigger);
                        //}

                        hit.transform.GetComponent<Gate>().TheOtherHalf.GetComponent<Animator>().SetTrigger(OpenTheGateTrigger);
                        hit.transform.GetComponent<Animator>().SetTrigger(OpenTheGateTrigger);
                    }
                }
            }
        }


        if(Input.GetKeyDown(NextSpotCam))
        {
            currentSpotCameraIndex++;
            if (currentSpotCameraIndex >= camSpots.Count) currentSpotCameraIndex = 0;

            ChangeCamSpot(currentSpotCameraIndex);
        }
        if (Input.GetKeyDown(PrevSpotCam))
        {
            currentSpotCameraIndex--;
            if (currentSpotCameraIndex < 0) currentSpotCameraIndex = camSpots.Count-1;

            ChangeCamSpot(currentSpotCameraIndex);
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
        Graphics.SetActive(x);
        Collider.enabled = x;
        rb.useGravity = x;
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

    void ChangeCamSpot(int _index)
    {
        if (camSpots.Count > 0)
        {
            for (int i = 0; i < camSpots.Count; i++)
            {
                camSpots[i].GetComponent<CinemachineVirtualCamera>().Priority = 0;
            }
            if(camSpots.Count > _index) camSpots[_index].GetComponent<CinemachineVirtualCamera>().Priority = 50;

        }
    }
}